namespace HukukTakip.Api.Application.DTOs;

using HukukTakip.Api.Domain.Entities;

public record MusteriListDto(
    Guid Id,
    string MusteriNo,
    string? AdiUnvani,
    BorcluTipi BorcluTipi
);

public record MusteriDetailDto(
    Guid Id,
    string MusteriNo,
    string? AdiUnvani,
    BorcluTipi BorcluTipi,
    string? BorcluSoyadi,
    string? TCKN,
    DateOnly? DogumTarihi,
    Cinsiyet? Cinsiyet,
    MedeniDurum? MedeniDurum,
    string? BabaAdi,
    string? AnneAdi,
    string? PasaportNumarasi,
    DateOnly? KimlikVerilisTarihi,
    int? NufusaKayitliOlduguIl,
    int? SehirId,
    int? IlceId,
    string? Semt,
    string? VergiDairesi,
    string? VergiNo,
    string? SSKNo,
    string? SSKIsyeriNumarasi,
    string? TicaretSicilNo,
    MistiralMusteriTuru MusteriMusteriTipi,
    bool HayattaMi
);

public record MusteriCreateDto(
    string MusteriNo,
    string? AdiUnvani,
    BorcluTipi BorcluTipi,
    string? BorcluSoyadi,
    string? TCKN,
    DateOnly? DogumTarihi,
    Cinsiyet? Cinsiyet,
    MedeniDurum? MedeniDurum,
    string? BabaAdi,
    string? AnneAdi,
    string? PasaportNumarasi,
    DateOnly? KimlikVerilisTarihi,
    int? NufusaKayitliOlduguIl,
    int? SehirId,
    int? IlceId,
    string? Semt,
    string? VergiDairesi,
    string? VergiNo,
    string? SSKNo,
    string? SSKIsyeriNumarasi,
    string? TicaretSicilNo,
    MistiralMusteriTuru MusteriMusteriTipi,
    bool HayattaMi
);

public record MusteriUpdateDto(
    string MusteriNo,
    string? AdiUnvani,
    BorcluTipi BorcluTipi,
    string? BorcluSoyadi,
    string? TCKN,
    DateOnly? DogumTarihi,
    Cinsiyet? Cinsiyet,
    MedeniDurum? MedeniDurum,
    string? BabaAdi,
    string? AnneAdi,
    string? PasaportNumarasi,
    DateOnly? KimlikVerilisTarihi,
    int? NufusaKayitliOlduguIl,
    int? SehirId,
    int? IlceId,
    string? Semt,
    string? VergiDairesi,
    string? VergiNo,
    string? SSKNo,
    string? SSKIsyeriNumarasi,
    string? TicaretSicilNo,

    MistiralMusteriTuru MusteriMusteriTipi,
    bool HayattaMi
);
