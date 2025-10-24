namespace HukukTakip.Api.Application.DTOs;

public record SehirDto(int Id, string Ad, string? PlakaKodu);
public record DavaTuruDto(int Id, string Ad);
public record IcraDairesiDto(int Id, string Ad, int SehirId, string SehirAd);
public record IlceDto(int Id, string Ad, int SehirId, string SehirAd);
