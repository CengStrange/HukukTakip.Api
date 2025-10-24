using HukukTakip.Api.Application.DTOs;
using HukukTakip.Api.Application.DTOs.Icra;
using HukukTakip.Api.Domain.Entities.Icra;
using HukukTakip.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HukukTakip.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IcraDosyalariController : ControllerBase
{
    private readonly AppDb _db;
    public IcraDosyalariController(AppDb db) => _db = db;


    [HttpGet]
    public async Task<ActionResult<PagedResult<IcraDosyasiListItemDto>>> GetAll(
        [FromQuery] string? q,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = _db.IcraDosyalari
            .Include(icra => icra.Musteri)
            .Include(icra => icra.Avukat)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();

            query = query.Where(x =>
                x.DosyaNo.Contains(q) ||
                (x.Musteri.AdiUnvani != null && x.Musteri.AdiUnvani.Contains(q)) ||
                (x.Avukat != null && (x.Avukat.Adi.Contains(q) || x.Avukat.Soyadi.Contains(q)))
            );
        }

        var total = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(x => x.OlusturmaTarihiUtc)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new IcraDosyasiListItemDto(
                x.Id,
                x.DosyaNo,
                x.Musteri.AdiUnvani ?? "-",
                x.Avukat != null ? $"{x.Avukat.Adi} {x.Avukat.Soyadi}" : null,
                x.Durum
            ))
            .ToListAsync(ct);

        return Ok(new PagedResult<IcraDosyasiListItemDto>(page, pageSize, total, items));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<IcraDosyasiDetailDto>> GetById(Guid id, CancellationToken ct)
    {
        var icra = await _db.IcraDosyalari.AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new IcraDosyasiDetailDto(
                x.Id, x.DosyaNo, x.MusteriId, x.AvukatId, x.AvukatTevziNo, x.TakipTarihi,
                x.TakipTipi, x.IhtarBorclulari, x.IhtarKonusuUrunler, x.IcraMudurlugu,
                x.MahiyetKodu, x.Durum
            ))
            .FirstOrDefaultAsync(ct);

        return icra is null ? NotFound() : Ok(icra);
    }

    [HttpPost]
    public async Task<ActionResult<IcraDosyasiDetailDto>> Create([FromBody] IcraDosyasiCreateDto dto, CancellationToken ct)
    {

        if (!await _db.Musteriler.AnyAsync(m => m.Id == dto.MusteriId, ct))
        {
            return BadRequest("Geçersiz Müşteri ID.");
        }
        if (dto.AvukatId.HasValue && !await _db.Avukatlar.AnyAsync(a => a.Id == dto.AvukatId.Value, ct))
        {
            return BadRequest("Geçersiz Avukat ID.");
        }

        var icra = new IcraDosyasi
        {
            Id = Guid.NewGuid(),
            DosyaNo = dto.DosyaNo,
            MusteriId = dto.MusteriId,
            AvukatId = dto.AvukatId,
            AvukatTevziNo = dto.AvukatTevziNo,
            TakipTarihi = dto.TakipTarihi,
            TakipTipi = dto.TakipTipi,
            IhtarBorclulari = dto.IhtarBorclulari,
            IhtarKonusuUrunler = dto.IhtarKonusuUrunler,
            IcraMudurlugu = dto.IcraMudurlugu,
            MahiyetKodu = dto.MahiyetKodu,
            Durum = dto.Durum
        };

        _db.Add(icra);
        await _db.SaveChangesAsync(ct);

        var result = await GetById(icra.Id, ct);
        return CreatedAtAction(nameof(GetById), new { id = icra.Id }, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] IcraDosyasiUpdateDto dto, CancellationToken ct)
    {
        var icra = await _db.IcraDosyalari.FindAsync(new object[] { id }, ct);
        if (icra is null) return NotFound();

        if (icra.MusteriId != dto.MusteriId && !await _db.Musteriler.AnyAsync(m => m.Id == dto.MusteriId, ct))
        {
            return BadRequest("Geçersiz Müşteri ID.");
        }
        if (dto.AvukatId.HasValue && icra.AvukatId != dto.AvukatId && !await _db.Avukatlar.AnyAsync(a => a.Id == dto.AvukatId.Value, ct))
        {
            return BadRequest("Geçersiz Avukat ID.");
        }


        icra.DosyaNo = dto.DosyaNo;
        icra.MusteriId = dto.MusteriId;
        icra.AvukatId = dto.AvukatId;
        icra.AvukatTevziNo = dto.AvukatTevziNo;
        icra.TakipTarihi = dto.TakipTarihi;
        icra.TakipTipi = dto.TakipTipi;
        icra.IhtarBorclulari = dto.IhtarBorclulari;
        icra.IhtarKonusuUrunler = dto.IhtarKonusuUrunler;
        icra.IcraMudurlugu = dto.IcraMudurlugu;
        icra.MahiyetKodu = dto.MahiyetKodu;
        icra.Durum = dto.Durum;

        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var icra = await _db.IcraDosyalari.FindAsync(new object[] { id }, ct);
        if (icra is null) return NotFound();

        _db.Remove(icra);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}
