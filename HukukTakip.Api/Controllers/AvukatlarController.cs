using HukukTakip.Api.Application.DTOs;
using HukukTakip.Api.Domain.Entities;
using HukukTakip.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HukukTakip.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AvukatlarController : ControllerBase
{
    private readonly AppDb _db;
    public AvukatlarController(AppDb db) => _db = db;

    // GET /api/avukatlar?q=&page=1&pageSize=10
    [HttpGet]
    public async Task<ActionResult<PagedResult<AvukatListDto>>> GetAll(
        [FromQuery] string? q,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = _db.Set<Avukat>().AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();
            query = query.Where(x =>
                x.Adi.Contains(q) ||
                x.Soyadi.Contains(q) ||
                (x.TCKN != null && x.TCKN.Contains(q)) ||
                (x.VergiNo != null && x.VergiNo.Contains(q))
            );
        }

        var total = await query.CountAsync(ct);

        var items = await query
            .OrderBy(x => x.Adi).ThenBy(x => x.Soyadi)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new AvukatListDto(x.Id, x.Adi, x.Soyadi))
            .ToListAsync(ct);

        return Ok(new PagedResult<AvukatListDto>(page, pageSize, total, items));
    }

    // GET /api/avukatlar/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AvukatDetailDto>> GetById(Guid id, CancellationToken ct)
    {
        var a = await _db.Set<Avukat>().AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new AvukatDetailDto(
                x.Id, x.Adi, x.Soyadi, x.TCKN, x.VergiDairesi, x.VergiNo, x.Email, x.DogumTarihi,
                x.CepTelefonu, x.IsTelefonu, x.IsFaxNo, x.SehirId, x.IlceId, x.TamAdres,
                x.AvansHesapSubeId, x.AvansHesapNo, x.VadesizHesapSubeId, x.VadesizHesapNo,
                x.HalkbankVadesizIbanNo, x.DigerBankaIbanNo, x.AvansLimiti, x.AvukatTipi,
                x.IletisimVeribanMi, x.Dialogdan, x.DialogYasal, x.Normal, x.HesapAktifMi
            ))
            .FirstOrDefaultAsync(ct);

        return a is null ? NotFound() : Ok(a);
    }

    [HttpPost]
    public async Task<ActionResult<AvukatDetailDto>> Create([FromBody] AvukatCreateDto dto, CancellationToken ct)
    {
        var a = new Avukat
        {
            Id = Guid.NewGuid(),
            Adi = dto.Adi,
            Soyadi = dto.Soyadi,
            TCKN = dto.TCKN,
            VergiDairesi = dto.VergiDairesi,
            VergiNo = dto.VergiNo,
            Email = dto.Email,
            DogumTarihi = dto.DogumTarihi,
            CepTelefonu = dto.CepTelefonu,
            IsTelefonu = dto.IsTelefonu,
            IsFaxNo = dto.IsFaxNo,
            SehirId = dto.SehirId,
            IlceId = dto.IlceId,
            TamAdres = dto.TamAdres,
            AvansHesapSubeId = dto.AvansHesapSubeId,
            AvansHesapNo = dto.AvansHesapNo,
            VadesizHesapSubeId = dto.VadesizHesapSubeId,
            VadesizHesapNo = dto.VadesizHesapNo,
            HalkbankVadesizIbanNo = dto.HalkbankVadesizIbanNo,
            DigerBankaIbanNo = dto.DigerBankaIbanNo,
            AvansLimiti = dto.AvansLimiti,
            AvukatTipi = dto.AvukatTipi,
            IletisimVeribanMi = dto.IletisimVeribanMi,
            Dialogdan = dto.Dialogdan,
            DialogYasal = dto.DialogYasal,
            Normal = dto.Normal,
            HesapAktifMi = dto.HesapAktifMi
        };

        _db.Add(a);
        await _db.SaveChangesAsync(ct);

        var result = await GetById(a.Id, ct);
        return CreatedAtAction(nameof(GetById), new { id = a.Id }, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] AvukatUpdateDto dto, CancellationToken ct)
    {
        var a = await _db.Set<Avukat>().FindAsync(new object[] { id }, ct);
        if (a is null) return NotFound();

        a.Adi = dto.Adi;
        a.Soyadi = dto.Soyadi;
        a.TCKN = dto.TCKN;
        a.VergiDairesi = dto.VergiDairesi;
        a.VergiNo = dto.VergiNo;
        a.Email = dto.Email;
        a.DogumTarihi = dto.DogumTarihi;
        a.CepTelefonu = dto.CepTelefonu;
        a.IsTelefonu = dto.IsTelefonu;
        a.IsFaxNo = dto.IsFaxNo;
        a.SehirId = dto.SehirId;
        a.IlceId = dto.IlceId;
        a.TamAdres = dto.TamAdres;
        a.AvansHesapSubeId = dto.AvansHesapSubeId;
        a.AvansHesapNo = dto.AvansHesapNo;
        a.VadesizHesapSubeId = dto.VadesizHesapSubeId;
        a.VadesizHesapNo = dto.VadesizHesapNo;
        a.HalkbankVadesizIbanNo = dto.HalkbankVadesizIbanNo;
        a.DigerBankaIbanNo = dto.DigerBankaIbanNo;
        a.AvansLimiti = dto.AvansLimiti;
        a.AvukatTipi = dto.AvukatTipi;
        a.IletisimVeribanMi = dto.IletisimVeribanMi;
        a.Dialogdan = dto.Dialogdan;
        a.DialogYasal = dto.DialogYasal;
        a.Normal = dto.Normal;
        a.HesapAktifMi = dto.HesapAktifMi;

        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var a = await _db.Set<Avukat>().FindAsync(new object[] { id }, ct);
        if (a is null) return NotFound();

        _db.Remove(a);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}
