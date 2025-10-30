using HukukTakip.Api.Application.DTOs;
using HukukTakip.Api.Domain.Entities;
using HukukTakip.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HukukTakip.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IhtarController : ControllerBase
{
    private readonly AppDb _db;
    public IhtarController(AppDb db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IhtarListItemDto>>> List([FromQuery] string? q, [FromQuery] int take = 50)
    {
        var query = _db.Ihtarlar
            .AsNoTracking()
            .Include(x => x.Musteri)
            .OrderByDescending(x => x.Id)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();
            query = query.Where(x =>
                (x.YevmiyeNo != null && x.YevmiyeNo.Contains(q)) ||
                (x.IhtarNo != null && x.IhtarNo.Contains(q)) ||
                (x.Musteri.AdiUnvani != null && x.Musteri.AdiUnvani.Contains(q)));
        }

        var list = await query
            .Take(Math.Clamp(take, 1, 200))
            .Select(x => new IhtarListItemDto(
                x.Id, x.MusteriId, x.Musteri.AdiUnvani ?? string.Empty,
                x.NoterAdi, x.YevmiyeNo, x.IhtarTarihi, x.IhtarNo,
                x.IhtarnameNakitTutar, x.IhtarnameGayriNakitTutar))
            .ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Ihtar>> Get(int id)
    {
        var e = await _db.Ihtarlar.Include(x => x.Musteri).FirstOrDefaultAsync(x => x.Id == id);
        return e is null ? NotFound() : Ok(e);
    }

    [HttpGet("musteri-ihtarlari")]
    public async Task<ActionResult> GetMusteriIhtarlari([FromQuery] Guid musteriId, CancellationToken ct)
    {
        if (musteriId == Guid.Empty) return BadRequest("MusteriId gereklidir.");

        var ihtarlar = await _db.Ihtarlar
            .Where(i => i.MusteriId == musteriId)
            .OrderByDescending(i => i.IhtarTarihi)
            .Select(i => new {
             
                id = i.Id,
                ihtarNo = i.IhtarNo,
                yevmiyeNo = i.YevmiyeNo,
                ihtarTarihi = i.IhtarTarihi,
                musteriUrunId = i.MusteriUrunleri 
            })
            .ToListAsync(ct);

        return Ok(ihtarlar);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] IhtarCreateDto dto)
    {
        if (dto.MusteriId == Guid.Empty) return BadRequest("MusteriId zorunlu.");
        if (dto.IhtarnameNakitTutar < 0 || dto.IhtarnameGayriNakitTutar < 0)
            return BadRequest("Tutarlar negatif olamaz.");
        if (dto.TebligTarihi is { } t1 && t1 < dto.IhtarTarihi)
            return BadRequest("Tebliğ tarihi İhtar tarihinden önce olamaz.");
        if (dto.IhtarTebligGirisTarihi is { } t2 && dto.TebligTarihi is { } tt && t2 < tt)
            return BadRequest("İhtar tebliğ giriş tarihi tebliğ tarihinden önce olamaz.");
        if (dto.KatTarihi is { } kt && dto.TebligTarihi is { } tt2 && kt > tt2)
            return BadRequest("Kat tarihi tebliğ tarihinden sonra olamaz.");
        ;

        if (!string.IsNullOrWhiteSpace(dto.YevmiyeNo) && !string.IsNullOrWhiteSpace(dto.NoterAdi))
        {
            var existsY = await _db.Ihtarlar.AnyAsync(x =>
                x.YevmiyeNo == dto.YevmiyeNo && x.NoterAdi == dto.NoterAdi);
            if (existsY) return Conflict("Aynı noter ve yevmiye numarası zaten mevcut.");
        }

        var userId = int.Parse(User.FindFirst("sub")?.Value ?? "0");

        var e = new Ihtar
        {
            MusteriId = dto.MusteriId,
            MusteriUrunleri = dto.MusteriUrunleri?.Trim(),
            NoterAdi = dto.NoterAdi?.Trim(),
            YevmiyeNo = dto.YevmiyeNo?.Trim(),
            IhtarTarihi = dto.IhtarTarihi,
            IhtarnameSuresiGun = dto.IhtarnameSuresiGun,
            TebligTarihi = dto.TebligTarihi,
            IhtarTebligGirisTarihi = dto.IhtarTebligGirisTarihi,
            KatTarihi = dto.KatTarihi,
            IhtarnameNakitTutar = dto.IhtarnameNakitTutar,
            IhtarnameGayriNakitTutar = dto.IhtarnameGayriNakitTutar,
            IhtarNo = dto.IhtarNo?.Trim(),
            Aciklama = dto.Aciklama,
            OlusturanUserId = userId,
            OlusturmaTarihiUtc = DateTime.UtcNow
        };

        _db.Ihtarlar.Add(e);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = e.Id }, new { e.Id });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] IhtarUpdateDto dto)
    {
        var e = await _db.Ihtarlar.FirstOrDefaultAsync(x => x.Id == id);
        if (e is null) return NotFound();

        if (dto.IhtarnameNakitTutar < 0 || dto.IhtarnameGayriNakitTutar < 0)
            return BadRequest("Tutarlar negatif olamaz.");
        if (dto.TebligTarihi is { } t1 && t1 < dto.IhtarTarihi)
            return BadRequest("Tebliğ tarihi İhtar tarihinden önce olamaz.");
        if (dto.IhtarTebligGirisTarihi is { } t2 && dto.TebligTarihi is { } tt && t2 < tt)
            return BadRequest("İhtar tebliğ giriş tarihi tebliğ tarihinden önce olamaz.");
        if (dto.KatTarihi is { } kt && dto.TebligTarihi is { } tt2 && kt > tt2)
            return BadRequest("Kat tarihi tebliğ tarihinden sonra olamaz.");

        if (!string.IsNullOrWhiteSpace(dto.YevmiyeNo) && !string.IsNullOrWhiteSpace(dto.NoterAdi))
        {
            var existsY = await _db.Ihtarlar.AnyAsync(x =>
                x.YevmiyeNo == dto.YevmiyeNo && x.NoterAdi == dto.NoterAdi && x.Id != id);
            if (existsY) return Conflict("Aynı noter ve yevmiye numarası zaten mevcut.");
        }


        e.MusteriUrunleri = dto.MusteriUrunleri?.Trim();
        e.NoterAdi = dto.NoterAdi?.Trim();
        e.YevmiyeNo = dto.YevmiyeNo?.Trim();
        e.IhtarTarihi = dto.IhtarTarihi;
        e.IhtarnameSuresiGun = dto.IhtarnameSuresiGun;
        e.TebligTarihi = dto.TebligTarihi;
        e.IhtarTebligGirisTarihi = dto.IhtarTebligGirisTarihi;
        e.KatTarihi = dto.KatTarihi;
        e.IhtarnameNakitTutar = dto.IhtarnameNakitTutar;
        e.IhtarnameGayriNakitTutar = dto.IhtarnameGayriNakitTutar;
        e.IhtarNo = dto.IhtarNo?.Trim();
        e.Aciklama = dto.Aciklama;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var e = await _db.Ihtarlar.FirstOrDefaultAsync(x => x.Id == id);
        if (e is null) return NotFound();

        _db.Remove(e);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}

