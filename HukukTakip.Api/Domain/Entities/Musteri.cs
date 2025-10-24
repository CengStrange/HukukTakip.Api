namespace HukukTakip.Api.Domain.Entities;

public enum Cinsiyet { Erkek = 1, Kadin = 2 }
public enum MedeniDurum { Bekar = 1, Evli = 2 }
public enum BorcluTipi
{
    AsilKrediBorclusu = 1,
    Kefil = 2,
    Mirasci = 3,
    Ciranta = 4,
    Kesideci = 5
}
public enum MistiralMusteriTuru { Bireysel = 1, Kurumsal = 2 }

public class Musteri
{
    public Guid Id { get; set; }

    // Temel
    public string MusteriNo { get; set; } = default!;
    public string? AdiUnvani { get; set; }
    public BorcluTipi BorcluTipi { get; set; }

    // Kimlik / Demografik
    public string? BorcluSoyadi { get; set; }
    public string? TCKN { get; set; }                 // UNIQUE (nullable)
    public DateOnly? DogumTarihi { get; set; }
    public string? DogumYeri { get; set; }
    public Cinsiyet? Cinsiyet { get; set; }           // nullable
    public MedeniDurum? MedeniDurum { get; set; }     // nullable
    public string? BabaAdi { get; set; }
    public string? AnneAdi { get; set; }

    // Kimlik (nüfus/cüzdan)
    public string? PasaportNumarasi { get; set; }
    public DateOnly? KimlikVerilisTarihi { get; set; }
    public string? NufusaKayitliOlduguIl { get; set; }
    public string? CiltNo { get; set; }
    public string? AileSiraNo { get; set; }
    public string? KutukSiraNo { get; set; }
    public string? SurucuBelgesiNumarasi { get; set; }

    // Adres
    public int? SehirId { get; set; }
    public int? IlceId { get; set; }
    public string? Semt { get; set; }

    // Ek Bilgiler
    public string? VergiDairesi { get; set; }
    public string? VergiNo { get; set; }              // UNIQUE (nullable)
    public string? SSKNo { get; set; }
    public string? SSKIsyeriNumarasi { get; set; }
    public string? TicaretSicilNo { get; set; }
    public string? BorcuTipi { get; set; }            
    public MistiralMusteriTuru MusteriMusteriTipi { get; set; } = MistiralMusteriTuru.Bireysel;
    public bool HayattaMi { get; set; } = true;

    // Audit
    public int OlusturanUserId { get; set; }
    public DateTime OlusturmaTarihiUtc { get; set; } = DateTime.UtcNow;
}
