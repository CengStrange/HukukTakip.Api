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
public class UrunlerController : ControllerBase
{
    private readonly AppDb _db;
    public UrunlerController(AppDb db) => _db = db;



    [HttpGet]
    public async Task<ActionResult> List(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] Guid? musteriId = null,
        CancellationToken ct = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = _db.Urunler.AsNoTracking();


        if (musteriId.HasValue)
        {
            query = query.Where(u => u.MusteriId == musteriId.Value);
        }

        var total = await query.CountAsync(ct);

        var items = await query
            .Include(u => u.Musteri)
            .OrderByDescending(x => x.OlusturmaTarihiUtc)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(u => new UrunListDto(
                u.Id,
                u.MusteriId,
                u.Musteri.AdiUnvani ?? "",
                u.UrunTipi,
                u.TakipMiktari,
                u.DovizTipi
            ))
            .ToListAsync(ct);

        return Ok(new { total, items });
    }

    // ---------- YENİ EKLENEN ENDPOINT (BİR SONRAKİ 404 HATASINI ÖNLEYECEK) ----------
    [HttpGet("ihtarli-urunler")]
    public async Task<ActionResult> GetIhtarliUrunler([FromQuery] Guid musteriId, CancellationToken ct)
    {
        // 1. Bu müşteriye ait ihtarlardaki ürün ID'lerini (string) al
        var ihtarliUrunIdStrings = await _db.Ihtarlar
            .Where(i => i.MusteriId == musteriId && i.MusteriUrunleri != null)
            .Select(i => i.MusteriUrunleri)
            .Distinct()
            .ToListAsync(ct);

        // 2. String ID'leri Guid'e çevir
        var urunGuidList = ihtarliUrunIdStrings
            .Select(idStr => Guid.TryParse(idStr, out var guid) ? guid : Guid.Empty)
            .Where(g => g != Guid.Empty)
            .ToList();

        // 3. Urunler tablosundan bu ID'lere sahip ürünlerin detaylarını getir
        // (Frontend'deki TS UrunListDto arayüzüyle eşleşen anonim bir tip kullanıyoruz)
        var urunler = await _db.Urunler
            .Include(u => u.Musteri) // musteriAdiUnvani için
            .Where(u => urunGuidList.Contains(u.Id))
            .Select(u => new {
                id = u.Id.ToString(), // Frontend UrunListDto 'id: string' bekliyor
                urunTipi = u.UrunTipi,
                musteriAdiUnvani = u.Musteri.AdiUnvani ?? "",
                takipMiktari = u.TakipMiktari,
                dovizTipi = u.DovizTipi
            })
            .ToListAsync(ct);

        return Ok(urunler);
    }
    // ---------- BİTTİ ----------

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UrunDetailDto>> Get(Guid id, CancellationToken ct)
    {
        var urun = await _db.Urunler.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, ct);

        if (urun is null) return NotFound();

        var dto = new UrunDetailDto(
            urun.Id,
            urun.MusteriId,
            urun.AvukatId,
            urun.KrediBirimKoduSubeId,
            urun.TakipBirimKoduSubeId,
            urun.UrunTipi,
            urun.KrediMudiNo,
            urun.TakipMiktari,
            urun.DovizTipi,
            urun.AylikFaizOrani,
            urun.TakipTarihi,
            urun.MasrafMudiNo,
            urun.MasrafBakiyesi,
            urun.FaizMudiNo,
            urun.FaizBakiyesi,
            urun.TakipMudiNo,
            urun.Aciklama
        );

        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UrunCreateDto dto, CancellationToken ct)
    {
        var urun = new Urun
        {
            Id = Guid.NewGuid(),
            MusteriId = dto.MusteriId,
            AvukatId = dto.AvukatId,
            KrediBirimKoduSubeId = dto.KrediBirimKoduSubeId,
            TakipBirimKoduSubeId = dto.TakipBirimKoduSubeId,
            UrunTipi = dto.UrunTipi,
            KrediMudiNo = dto.KrediMudiNo,
            TakipMiktari = dto.TakipMiktari,
            DovizTipi = dto.DovizTipi,
            AylikFaizOrani = dto.AylikFaizOrani,
            TakipTarihi = dto.TakipTarihi,
            MasrafMudiNo = dto.MasrafMudiNo,
            MasrafBakiyesi = dto.MasrafBakiyesi,
            FaizMudiNo = dto.FaizMudiNo,
            FaizBakiyesi = dto.FaizBakiyesi,
            TakipMudiNo = dto.TakipMudiNo,
            Aciklama = dto.Aciklama,
            OlusturmaTarihiUtc = DateTime.UtcNow
        };

        _db.Urunler.Add(urun);
        await _db.SaveChangesAsync(ct);

        // ----- HATA DÜZELTMESİ BURADA (e.Id -> urun.Id) -----
        return CreatedAtAction(nameof(Get), new { id = urun.Id }, new { urun.Id });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UrunUpdateDto dto, CancellationToken ct)
    {
        var urun = await _db.Urunler.FindAsync(new object[] { id }, ct);
        if (urun is null) return NotFound();

        urun.MusteriId = dto.MusteriId;
        urun.AvukatId = dto.AvukatId;
        urun.KrediBirimKoduSubeId = dto.KrediBirimKoduSubeId;
        urun.TakipBirimKoduSubeId = dto.TakipBirimKoduSubeId;
        urun.UrunTipi = dto.UrunTipi;
        urun.KrediMudiNo = dto.KrediMudiNo;
        urun.TakipMiktari = dto.TakipMiktari;
        urun.DovizTipi = dto.DovizTipi;
        urun.AylikFaizOrani = dto.AylikFaizOrani;
        urun.TakipTarihi = dto.TakipTarihi;
        urun.MasrafMudiNo = dto.MasrafMudiNo;
        urun.MasrafBakiyesi = dto.MasrafBakiyesi;
        urun.FaizMudiNo = dto.FaizMudiNo;
        urun.FaizBakiyesi = dto.FaizBakiyesi;
        urun.TakipMudiNo = dto.TakipMudiNo;
        urun.Aciklama = dto.Aciklama;

        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var urun = await _db.Urunler.FindAsync(new object[] { id }, ct);
        if (urun is null) return NotFound();

        _db.Urunler.Remove(urun);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}

