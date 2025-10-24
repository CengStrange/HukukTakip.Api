using System.Text.RegularExpressions;
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
[Produces("application/json")]
public class MusterilerController : ControllerBase
{
    private readonly AppDb _db;
    public MusterilerController(AppDb db) => _db = db;

    // ------------------------- Helpers -------------------------
    static bool IsDigits(string s, int len) => Regex.IsMatch(s, $"^\\d{{{len}}}$");

    // Normalize + iş kuralları (artık TCKN/Vergi kuralları MusteriMusteriTipi'ne göre)
    private (bool ok, string? error) ValidateAndNormalizeForType(ref MusteriCreateDto dto)
    {
        // Genel trim (null güvenli)
        string? T(string? v) => string.IsNullOrWhiteSpace(v) ? null : v!.Trim();

        dto = dto with
        {
            MusteriNo = dto.MusteriNo.Trim(),
            AdiUnvani = T(dto.AdiUnvani),
            BorcluSoyadi = T(dto.BorcluSoyadi),
            TCKN = T(dto.TCKN),
            DogumYeri = T(dto.DogumYeri),
            BabaAdi = T(dto.BabaAdi),
            AnneAdi = T(dto.AnneAdi),
            PasaportNumarasi = T(dto.PasaportNumarasi),
            NufusaKayitliOlduguIl = T(dto.NufusaKayitliOlduguIl),
            CiltNo = T(dto.CiltNo),
            AileSiraNo = T(dto.AileSiraNo),
            KutukSiraNo = T(dto.KutukSiraNo),
            SurucuBelgesiNumarasi = T(dto.SurucuBelgesiNumarasi),
            Semt = T(dto.Semt),
            VergiDairesi = T(dto.VergiDairesi),
            VergiNo = T(dto.VergiNo),
            SSKNo = T(dto.SSKNo),
            SSKIsyeriNumarasi = T(dto.SSKIsyeriNumarasi),
            TicaretSicilNo = T(dto.TicaretSicilNo),
            BorcuTipi = T(dto.BorcuTipi)
            // BorcluTuru kaldırıldı (istemiştin)
        };

        // Artık Bireysel / Kurumsal üzerinden kontrol:
        if (dto.MusteriMusteriTipi == MistiralMusteriTuru.Bireysel)
        {
            if (string.IsNullOrWhiteSpace(dto.TCKN))
                return (false, "Bireysel müşteri için TCKN zorunlu.");
            if (!IsDigits(dto.TCKN, 11))
                return (false, "TCKN 11 haneli rakamlardan oluşmalıdır.");
            if (!string.IsNullOrWhiteSpace(dto.VergiNo))
                return (false, "Bireysel müşteri için Vergi No girilmemelidir.");
        }
        else // Kurumsal
        {
            if (string.IsNullOrWhiteSpace(dto.VergiNo))
                return (false, "Kurumsal müşteri için Vergi No zorunlu.");
            if (!IsDigits(dto.VergiNo, 10))
                return (false, "Vergi No 10 haneli rakamlardan oluşmalıdır.");
            if (!string.IsNullOrWhiteSpace(dto.TCKN))
                return (false, "Kurumsal müşteri için TCKN girilmemelidir.");
            // Kurumsal için bireysel alanları sıfırlayalım:
            dto = dto with
            {
                Cinsiyet = null,
                MedeniDurum = null,
                BabaAdi = null,
                AnneAdi = null,
                DogumTarihi = null
            };
        }

        return (true, null);
    }

    // ------------------------- CREATE -------------------------
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MusteriCreateDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.MusteriNo))
            return BadRequest("Müşteri No zorunlu.");

        var (ok, error) = ValidateAndNormalizeForType(ref dto);
        if (!ok) return BadRequest(error);

        // Unique checks
        if (await _db.Musteriler.AnyAsync(x => x.MusteriNo == dto.MusteriNo, ct))
            return Conflict("Aynı Müşteri No mevcut.");

        if (!string.IsNullOrWhiteSpace(dto.TCKN) &&
            await _db.Musteriler.AnyAsync(x => x.TCKN == dto.TCKN, ct))
            return Conflict("Aynı TCKN mevcut.");

        if (!string.IsNullOrWhiteSpace(dto.VergiNo) &&
            await _db.Musteriler.AnyAsync(x => x.VergiNo == dto.VergiNo, ct))
            return Conflict("Aynı Vergi No mevcut.");

        var e = new Musteri
        {
            Id = Guid.NewGuid(),
            MusteriNo = dto.MusteriNo,
            AdiUnvani = dto.AdiUnvani,
            BorcluTipi = dto.BorcluTipi, // AsilKrediBorclusu/Kefil/.. gibi roller
            BorcluSoyadi = dto.BorcluSoyadi,
            TCKN = dto.TCKN,
            DogumTarihi = dto.DogumTarihi,
            DogumYeri = dto.DogumYeri,
            Cinsiyet = dto.Cinsiyet,
            MedeniDurum = dto.MedeniDurum,
            BabaAdi = dto.BabaAdi,
            AnneAdi = dto.AnneAdi,
            PasaportNumarasi = dto.PasaportNumarasi,
            KimlikVerilisTarihi = dto.KimlikVerilisTarihi,
            NufusaKayitliOlduguIl = dto.NufusaKayitliOlduguIl,
            CiltNo = dto.CiltNo,
            AileSiraNo = dto.AileSiraNo,
            KutukSiraNo = dto.KutukSiraNo,
            SurucuBelgesiNumarasi = dto.SurucuBelgesiNumarasi,
            //SehirId = dto.SehirId,
            IlceId = dto.IlceId,
            Semt = dto.Semt,
            VergiDairesi = dto.VergiDairesi,
            VergiNo = dto.VergiNo,
            SSKNo = dto.SSKNo,
            SSKIsyeriNumarasi = dto.SSKIsyeriNumarasi,
            TicaretSicilNo = dto.TicaretSicilNo,
            // BorcluTuru alanı 
            BorcuTipi = dto.BorcuTipi,
            MusteriMusteriTipi = dto.MusteriMusteriTipi,
            HayattaMi = dto.HayattaMi,
            OlusturanUserId = int.TryParse(User.FindFirst("sub")?.Value, out var uid) ? uid : 0,
            OlusturmaTarihiUtc = DateTime.UtcNow
        };

        _db.Musteriler.Add(e);
        await _db.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(Get), new { id = e.Id }, new { e.Id });
    }

    // ------------------------- DETAIL -------------------------
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MusteriDetailDto>> Get(Guid id, CancellationToken ct)
    {
        var x = await _db.Musteriler.AsNoTracking()
            .FirstOrDefaultAsync(y => y.Id == id, ct);

        if (x is null) return NotFound();

        var dto = new MusteriDetailDto(
            x.Id, x.MusteriNo, x.AdiUnvani, x.BorcluTipi, x.BorcluSoyadi, x.TCKN,
            x.DogumTarihi, x.DogumYeri, x.Cinsiyet, x.MedeniDurum, x.BabaAdi, x.AnneAdi,
            x.PasaportNumarasi, x.KimlikVerilisTarihi, x.NufusaKayitliOlduguIl, x.CiltNo,
            x.AileSiraNo, x.KutukSiraNo, x.SurucuBelgesiNumarasi,
            x.SehirId, x.IlceId, x.Semt, x.VergiDairesi, x.VergiNo, x.SSKNo,
            x.SSKIsyeriNumarasi, x.TicaretSicilNo,
            // BorcluTuru kaldırıldığı için buraya dahil değil
            x.BorcuTipi, x.MusteriMusteriTipi, x.HayattaMi
        );

        return Ok(dto);
    }

    // ------------------------- LIST (paging + basit arama) -------------------------
    [HttpGet]
    public async Task<ActionResult<object>> List(
        [FromQuery] string? q,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] BorcluTipi? borcluTipi = null,
        [FromQuery] MistiralMusteriTuru? musteriTipi = null,
        CancellationToken ct = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var qry = _db.Musteriler.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();
            qry = qry.Where(x =>
                x.MusteriNo.Contains(q) ||
                (x.AdiUnvani != null && x.AdiUnvani.Contains(q)));
        }

        if (borcluTipi.HasValue)
            qry = qry.Where(x => x.BorcluTipi == borcluTipi.Value);

        if (musteriTipi.HasValue)
            qry = qry.Where(x => x.MusteriMusteriTipi == musteriTipi.Value);

        var total = await qry.CountAsync(ct);

        var items = await qry
            .OrderByDescending(x => x.OlusturmaTarihiUtc)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new MusteriListDto(x.Id, x.MusteriNo, x.AdiUnvani, x.BorcluTipi))
            .ToListAsync(ct);

        return Ok(new { page, pageSize, total, items });
    }

    // ------------------------- UPDATE -------------------------
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] MusteriUpdateDto dto, CancellationToken ct)
    {
        var e = await _db.Musteriler.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (e is null) return NotFound();

        // Map UpdateDto -> Create-like for same validation
        var createLike = new MusteriCreateDto(
            dto.MusteriNo, dto.AdiUnvani, dto.BorcluTipi, dto.BorcluSoyadi, dto.TCKN,
            dto.DogumTarihi, dto.DogumYeri, dto.Cinsiyet, dto.MedeniDurum, dto.BabaAdi, dto.AnneAdi,
            dto.PasaportNumarasi, dto.KimlikVerilisTarihi, dto.NufusaKayitliOlduguIl, dto.CiltNo,
            dto.AileSiraNo, dto.KutukSiraNo, dto.SurucuBelgesiNumarasi, dto.SehirId, dto.IlceId,
            dto.Semt, dto.VergiDairesi, dto.VergiNo, dto.SSKNo, dto.SSKIsyeriNumarasi,
            dto.TicaretSicilNo,
            // Burada BorcluTuru yok
            dto.BorcuTipi, dto.MusteriMusteriTipi, dto.HayattaMi);

        var (ok, error) = ValidateAndNormalizeForType(ref createLike);
        if (!ok) return BadRequest(error);

        // Unique checks (kendi kaydı hariç)
        if (await _db.Musteriler.AnyAsync(x => x.MusteriNo == createLike.MusteriNo && x.Id != id, ct))
            return Conflict("Aynı Müşteri No zaten mevcut.");
        if (!string.IsNullOrWhiteSpace(createLike.TCKN) &&
            await _db.Musteriler.AnyAsync(x => x.TCKN == createLike.TCKN && x.Id != id, ct))
            return Conflict("Aynı TCKN zaten mevcut.");
        if (!string.IsNullOrWhiteSpace(createLike.VergiNo) &&
            await _db.Musteriler.AnyAsync(x => x.VergiNo == createLike.VergiNo && x.Id != id, ct))
            return Conflict("Aynı Vergi No zaten mevcut.");

        // Güncelle
        e.MusteriNo = createLike.MusteriNo;
        e.AdiUnvani = createLike.AdiUnvani;
        e.BorcluTipi = createLike.BorcluTipi;
        e.BorcluSoyadi = createLike.BorcluSoyadi;
        e.TCKN = createLike.TCKN;
        e.DogumTarihi = createLike.DogumTarihi;
        e.DogumYeri = createLike.DogumYeri;
        e.Cinsiyet = createLike.Cinsiyet;
        e.MedeniDurum = createLike.MedeniDurum;
        e.BabaAdi = createLike.BabaAdi;
        e.AnneAdi = createLike.AnneAdi;
        e.PasaportNumarasi = createLike.PasaportNumarasi;
        e.KimlikVerilisTarihi = createLike.KimlikVerilisTarihi;
        e.NufusaKayitliOlduguIl = createLike.NufusaKayitliOlduguIl;
        e.CiltNo = createLike.CiltNo;
        e.AileSiraNo = createLike.AileSiraNo;
        e.KutukSiraNo = createLike.KutukSiraNo;
        e.SurucuBelgesiNumarasi = createLike.SurucuBelgesiNumarasi;
        e.SehirId = createLike.SehirId;
        e.IlceId = createLike.IlceId;
        e.Semt = createLike.Semt;
        e.VergiDairesi = createLike.VergiDairesi;
        e.VergiNo = createLike.VergiNo;
        e.SSKNo = createLike.SSKNo;
        e.SSKIsyeriNumarasi = createLike.SSKIsyeriNumarasi;
        e.TicaretSicilNo = createLike.TicaretSicilNo;
        e.BorcuTipi = createLike.BorcuTipi;
        e.MusteriMusteriTipi = createLike.MusteriMusteriTipi;
        e.HayattaMi = createLike.HayattaMi;

        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    // ------------------------- DELETE -------------------------
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var e = await _db.Musteriler.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (e is null) return NotFound();

        var ihtarVarMi = await _db.Ihtarlar.AnyAsync(i => i.MusteriId == id, ct);
        if (ihtarVarMi)
            return Conflict("Bu müşteriye bağlı ihtar kayıtları var. Önce onları silin ya da devredin.");

        _db.Musteriler.Remove(e);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    // ------------------------- QUICK CHECK -------------------------
    [HttpGet("check")]
    [AllowAnonymous]
    public async Task<ActionResult<object>> Check(
        [FromQuery] string? musteriNo,
        [FromQuery] string? tckn,
        [FromQuery] string? vergiNo,
        CancellationToken ct)
    {
        musteriNo = string.IsNullOrWhiteSpace(musteriNo) ? null : musteriNo.Trim();
        tckn = string.IsNullOrWhiteSpace(tckn) ? null : tckn.Trim();
        vergiNo = string.IsNullOrWhiteSpace(vergiNo) ? null : vergiNo.Trim();

        var existsMusteriNo = musteriNo != null &&
            await _db.Musteriler.AnyAsync(x => x.MusteriNo == musteriNo, ct);

        var existsTckn = tckn != null &&
            await _db.Musteriler.AnyAsync(x => x.TCKN == tckn, ct);

        var existsVergi = vergiNo != null &&
            await _db.Musteriler.AnyAsync(x => x.VergiNo == vergiNo, ct);

        return Ok(new
        {
            musteriNoTaken = existsMusteriNo,
            tcknTaken = existsTckn,
            vergiNoTaken = existsVergi
        });
    }
}
