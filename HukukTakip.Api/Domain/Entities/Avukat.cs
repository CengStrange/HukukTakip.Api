using HukukTakip.Api.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HukukTakip.Api.Domain.Entities;

public class Avukat
{
    public Guid Id { get; set; }

    public string Adi { get; set; } = null!;
    public string Soyadi { get; set; } = null!;

    [NotMapped]
    public string AdSoyad => $"{Adi} {Soyadi}";

    // TEMEL BİLGİLER
    public string? TCKN { get; set; }
    public string? VergiDairesi { get; set; }
    public string? VergiNo { get; set; }
    public string? Email { get; set; }
    public DateOnly? DogumTarihi { get; set; }

    // İLETİŞİM
    public string? CepTelefonu { get; set; }
    public string? IsTelefonu { get; set; }
    public string? IsFaxNo { get; set; }

    // ADRES
    public int? SehirId { get; set; }
    public int? IlceId { get; set; }
    public string? TamAdres { get; set; }

    // HESAP BİLGİLERİ
    public int? AvansHesapSubeId { get; set; }
    public Sube? AvansHesapSube { get; set; }
    public string? AvansHesapNo { get; set; }

    public int? VadesizHesapSubeId { get; set; }
    public Sube? VadesizHesapSube { get; set; }
    public string? VadesizHesapNo { get; set; }
    public string? HalkbankVadesizIbanNo { get; set; }
    public string? DigerBankaIbanNo { get; set; }

    // DİĞER BİLGİLER
    public decimal AvansLimiti { get; set; }
    public AvukatTipi? AvukatTipi { get; set; }
    public bool IletisimVeribanMi { get; set; }
    public bool Dialogdan { get; set; }
    public bool DialogYasal { get; set; }
    public bool Normal { get; set; }
    public bool HesapAktifMi { get; set; } = true;

    // AUDIT
    public DateTime OlusturmaTarihiUtc { get; set; } = DateTime.UtcNow;
}

