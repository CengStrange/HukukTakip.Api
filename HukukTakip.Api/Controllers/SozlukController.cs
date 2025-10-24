using ExcelDataReader;
using HukukTakip.Api.Domain.Entities.Sozluk;
using HukukTakip.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

namespace HukukTakip.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SozlukController : ControllerBase
    {
        private readonly AppDb _db;
        public SozlukController(AppDb db) => _db = db;

        [HttpGet("icra-daireleri")]
        public async Task<IActionResult> IcraDaireleri(
            [FromQuery] int? sehirId,
            [FromQuery] string? q,
            [FromQuery] bool onlyIcra = false,
            [FromQuery] int take = 100)
        {
            var qry = _db.IcraDaireleri.AsNoTracking().AsQueryable();

            if (sehirId.HasValue && sehirId.Value > 0)
                qry = qry.Where(x => x.SehirId == sehirId.Value);

            if (onlyIcra)
                qry = qry.Where(x => EF.Functions.Collate(x.Ad, "Turkish_CI_AI").Contains("icra"));

          
            if (!string.IsNullOrWhiteSpace(q))
                qry = qry.Where(x => EF.Functions.Collate(x.Ad, "Turkish_CI_AI").Contains(q));

            var list = await qry
                .OrderBy(x => x.Ad)
                .Take(Math.Clamp(take, 1, 2000))
                .Select(x => new { x.Id, x.Ad, x.SehirId })
                .ToListAsync();

            return Ok(list);
        }

        [HttpGet("sehirler")]
        public async Task<IActionResult> Sehirler([FromQuery] string? q, [FromQuery] int take = 81)
        {
            var qry = _db.Sehirler.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
                qry = qry.Where(x => EF.Functions.Collate(x.Ad, "Turkish_CI_AI").Contains(q));

            var list = await qry
                .OrderBy(x => x.Ad)
                .Take(Math.Clamp(take, 1, 81))
                .Select(x => new { x.Id, x.Ad, x.PlakaKodu })
                .ToListAsync();

            return Ok(list);
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("import-icra-daireleri")]
        public async Task<IActionResult> ImportIcraDaireleri([FromForm] IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("Excel dosyası boş veya yüklenemedi.");

            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var stream = file.OpenReadStream();
            using var reader = ExcelReaderFactory.CreateReader(stream);
            var ds = reader.AsDataSet(new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = true }
            });

            if (ds.Tables.Count == 0)
                return BadRequest("Çalışılacak sayfa bulunamadı.");

            var tbl = ds.Tables[0];

            string colAd = tbl.Columns.Contains("UYAP_BRM_TUR_ACKLM") ? "UYAP_BRM_TUR_ACKLM"
                          : tbl.Columns.Contains("Ad") ? "Ad" : null;

            string colSehirId = tbl.Columns.Contains("UYAP_IL_KOD") ? "UYAP_IL_KOD"
                              : tbl.Columns.Contains("SehirId") ? "SehirId" : null;

            if (colAd is null || colSehirId is null)
                return BadRequest("Excel başlıkları hatalı. Beklenen: UYAP_BRM_TUR_ACKLM/Ad ve UYAP_IL_KOD/SehirId.");

            var added = 0;
            var skipped = 0;

            
            var batchSize = 500;
            var toAdd = new List<IcraDairesi>(batchSize);

            foreach (DataRow row in tbl.Rows)
            {
                try
                {
                    var adRaw = row[colAd]?.ToString()?.Trim();
                    if (string.IsNullOrWhiteSpace(adRaw)) { skipped++; continue; }

                    if (!int.TryParse(row[colSehirId]?.ToString(), out var sehirId) || sehirId <= 0)
                    { skipped++; continue; }

                    var exists = await _db.IcraDaireleri
                        .AnyAsync(x => x.SehirId == sehirId &&
                                       EF.Functions.Collate(x.Ad, "Turkish_CI_AI") ==
                                       EF.Functions.Collate(adRaw, "Turkish_CI_AI"));

                    if (exists) { skipped++; continue; }

                    toAdd.Add(new IcraDairesi
                    {
                        Id = Guid.NewGuid(),   
                        SehirId = sehirId,
                        Ad = adRaw
                    });

                    if (toAdd.Count >= batchSize)
                    {
                        _db.IcraDaireleri.AddRange(toAdd);
                        added += toAdd.Count;
                        toAdd.Clear();
                        await _db.SaveChangesAsync();
                    }
                }
                catch
                {
                    skipped++;
                }
            }

            if (toAdd.Count > 0)
            {
                _db.IcraDaireleri.AddRange(toAdd);
                added += toAdd.Count;
                await _db.SaveChangesAsync();
            }

            return Ok(new { added, skipped });
        }
    }
}
