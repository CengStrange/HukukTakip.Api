using HukukTakip.Api.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HukukTakip.Api.Domain.Entities;

public class Urun
{
    public Guid Id { get; set; }

    // İlişkiler
    public Guid MusteriId { get; set; }
    public Musteri Musteri { get; set; } = null!;

    public Guid? AvukatId { get; set; }
    public Avukat? Avukat { get; set; }

    [ForeignKey("KrediBirimSube")]
    public int? KrediBirimKoduSubeId { get; set; }
    public Sube? KrediBirimSube { get; set; }

    [ForeignKey("TakipBirimSube")]
    public int? TakipBirimKoduSubeId { get; set; }
    public Sube? TakipBirimSube { get; set; }


    public UrunTipi UrunTipi { get; set; }
    public string? KrediMudiNo { get; set; }
    public decimal TakipMiktari { get; set; }
    public string? DovizTipi { get; set; }
    public decimal? AylikFaizOrani { get; set; }
    public DateOnly? TakipTarihi { get; set; }
    public string? MasrafMudiNo { get; set; }
    public decimal? MasrafBakiyesi { get; set; }
    public string? FaizMudiNo { get; set; }
    public decimal? FaizBakiyesi { get; set; }
    public string? TakipMudiNo { get; set; }
    public string? Aciklama { get; set; }
    public DateTime OlusturmaTarihiUtc { get; set; } = DateTime.UtcNow;
}
