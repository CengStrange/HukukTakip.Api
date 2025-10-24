using HukukTakip.Api.Domain.Enums;
using System;

namespace HukukTakip.Api.Application.DTOs.Icra
{
    public record IcraDosyasiDetailDto(
    Guid Id,
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
    IcraDurumu Durum
);

}
