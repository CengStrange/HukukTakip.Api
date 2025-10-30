using HukukTakip.Api.Domain.Enums;

namespace HukukTakip.Api.Application.DTOs;

public record UrunListDto(
    Guid Id,
    Guid MusteriId,
    string MusteriAdiUnvani,
    UrunTipi UrunTipi,
    decimal TakipMiktari,
    string? DovizTipi
);

public record UrunDetailDto(
    Guid Id,
    Guid MusteriId,
    Guid? AvukatId,
    int? KrediBirimKoduSubeId,
    int? TakipBirimKoduSubeId,
    UrunTipi UrunTipi,
    string? KrediMudiNo,
    decimal TakipMiktari,
    string? DovizTipi,
    decimal? AylikFaizOrani,
    DateOnly? TakipTarihi,
    string? MasrafMudiNo,
    decimal? MasrafBakiyesi,
    string? FaizMudiNo,
    decimal? FaizBakiyesi,
    string? TakipMudiNo,
    string? Aciklama
);

public record UrunCreateDto(
    Guid MusteriId,
    Guid? AvukatId,
    int? KrediBirimKoduSubeId,
    int? TakipBirimKoduSubeId,
    UrunTipi UrunTipi,
    string? KrediMudiNo,
    decimal TakipMiktari,
    string? DovizTipi,
    decimal? AylikFaizOrani,
    DateOnly? TakipTarihi,
    string? MasrafMudiNo,
    decimal? MasrafBakiyesi,
    string? FaizMudiNo,
    decimal? FaizBakiyesi,
    string? TakipMudiNo,
    string? Aciklama
);

public record UrunUpdateDto(
    Guid MusteriId,
    Guid? AvukatId,
    int? KrediBirimKoduSubeId,
    int? TakipBirimKoduSubeId,
    UrunTipi UrunTipi,
    string? KrediMudiNo,
    decimal TakipMiktari,
    string? DovizTipi,
    decimal? AylikFaizOrani,
    DateOnly? TakipTarihi,
    string? MasrafMudiNo,
    decimal? MasrafBakiyesi,
    string? FaizMudiNo,
    decimal? FaizBakiyesi,
    string? TakipMudiNo,
    string? Aciklama
);
