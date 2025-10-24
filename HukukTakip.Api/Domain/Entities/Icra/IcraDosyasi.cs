using HukukTakip.Api.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HukukTakip.Api.Domain.Entities.Icra;

public class IcraDosyasi
{
    public Guid Id { get; set; }

    public string DosyaNo { get; set; } = null!;

    public Guid MusteriId { get; set; }
    public Musteri Musteri { get; set; } = null!;

    public Guid? AvukatId { get; set; }
    public Avukat? Avukat { get; set; }

    public string? AvukatTevziNo { get; set; }
    public DateOnly? TakipTarihi { get; set; }
    public TakipTipi? TakipTipi { get; set; }

    public string? IhtarBorclulari { get; set; }
    public string? IhtarKonusuUrunler { get; set; }

    public string? IcraMudurlugu { get; set; }
    public MahiyetKoduTipi? MahiyetKodu { get; set; }

    public IcraDurumu Durum { get; set; } = IcraDurumu.Acik;

    public DateTime OlusturmaTarihiUtc { get; set; } = DateTime.UtcNow;

}

