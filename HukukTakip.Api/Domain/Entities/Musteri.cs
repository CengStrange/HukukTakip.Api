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

    public string MusteriNo { get; set; } = default!;
    public string? AdiUnvani { get; set; }
    public BorcluTipi BorcluTipi { get; set; }
    public string? BorcluSoyadi { get; set; }
    public string? TCKN { get; set; }                 
    public DateOnly? DogumTarihi { get; set; }
    public Cinsiyet? Cinsiyet { get; set; }           
    public MedeniDurum? MedeniDurum { get; set; }     
    public string? BabaAdi { get; set; }
    public string? AnneAdi { get; set; }

    public string? PasaportNumarasi { get; set; }
    public DateOnly? KimlikVerilisTarihi { get; set; }
    public int? NufusaKayitliOlduguIl { get; set; }


    public int? SehirId { get; set; }
    public int? IlceId { get; set; }
    public string? Semt { get; set; }

    public string? VergiDairesi { get; set; }
    public string? VergiNo { get; set; }             
    public string? SSKNo { get; set; }
    public string? SSKIsyeriNumarasi { get; set; }
    public string? TicaretSicilNo { get; set; }
    public MistiralMusteriTuru MusteriMusteriTipi { get; set; } = MistiralMusteriTuru.Bireysel;
    public bool HayattaMi { get; set; } = true;

    public DateTime OlusturmaTarihiUtc { get; set; } = DateTime.UtcNow;
}
