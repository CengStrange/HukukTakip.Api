namespace HukukTakip.Api.Application.DTOs;

public record IhtarListItemDto(
    int Id,
    Guid MusteriId,
    string MusteriAd,
    string? NoterAdi,
    string? YevmiyeNo,
    DateOnly IhtarTarihi,
    string? IhtarNo,
    decimal IhtarnameNakitTutar,
    decimal IhtarnameGayriNakitTutar
    
);

public record IhtarCreateDto(
    Guid MusteriId,
    string? MusteriUrunleri,
    string? NoterAdi,
    string? YevmiyeNo,
    DateOnly IhtarTarihi,
    int? IhtarnameSuresiGun,            
    DateOnly? TebligTarihi,
    DateOnly? IhtarTebligGirisTarihi,
    DateOnly? KatTarihi,
    decimal IhtarnameNakitTutar,
    decimal IhtarnameGayriNakitTutar,
    string? IhtarNo,
    string? Aciklama
);

public record IhtarUpdateDto(
    string? MusteriUrunleri,
    string? NoterAdi,
    string? YevmiyeNo,
    DateOnly IhtarTarihi,
    int? IhtarnameSuresiGun,
    DateOnly? TebligTarihi,
    DateOnly? IhtarTebligGirisTarihi,
    DateOnly? KatTarihi,
    decimal IhtarnameNakitTutar,
    decimal IhtarnameGayriNakitTutar,
    string? IhtarNo,
    string? Aciklama
);
