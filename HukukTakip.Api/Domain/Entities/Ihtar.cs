namespace HukukTakip.Api.Domain.Entities;

public class Ihtar
{
    public int Id { get; set; }

    public Guid MusteriId { get; set; }           // 🔴 Guid (Musteri.Id ile uyumlu)
    public Musteri Musteri { get; set; } = null!;

    public string? MusteriUrunleri { get; set; }
    public string? NoterAdi { get; set; }
    public string? YevmiyeNo { get; set; }        // UNIQUE (nullable)
    public DateOnly IhtarTarihi { get; set; }
    public int? IhtarnameSuresiGun { get; set; }
    public DateOnly? TebligTarihi { get; set; }
    public DateOnly? IhtarTebligGirisTarihi { get; set; }
    public DateOnly? KatTarihi { get; set; }
    public decimal IhtarnameNakitTutar { get; set; }
    public decimal IhtarnameGayriNakitTutar { get; set; }
    public string? IhtarNo { get; set; }          // UNIQUE (nullable)
    public string? Aciklama { get; set; }

    public int OlusturanUserId { get; set; }
    public DateTime OlusturmaTarihiUtc { get; set; }
}
