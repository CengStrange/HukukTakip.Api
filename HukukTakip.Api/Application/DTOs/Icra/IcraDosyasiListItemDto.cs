using System;
using HukukTakip.Api.Domain.Enums;

namespace HukukTakip.Api.Application.DTOs.Icra
{
    public record IcraDosyasiListItemDto(
       Guid Id,
        string DosyaNo,
        string MusteriAdi, 
        string? AvukatAdiSoyadi, 
        IcraDurumu Durum
);
}
