using HukukTakip.Api.Domain.Enums;

public record IcraDosyasiCreateDto(
    string DosyaNo,
    Guid MusteriId,
    Guid? AvukatId,
    string? AvukatTevziNo,
    DateOnly? TakipTarihi,
    TakipTipi? TakipTipi,
    string? IhtarBorclulari,
    string? IhtarKonusuUrunler,
    string? IcraMudurlugu,
    MahiyetKoduTipi? MahiyetKodu,
    IcraDurumu Durum = IcraDurumu.Acik 
);
