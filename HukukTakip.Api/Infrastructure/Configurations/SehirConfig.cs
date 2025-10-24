namespace HukukTakip.Api.Infrastructure.Configurations;

using HukukTakip.Api.Domain.Entities.Sozluk;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SehirConfig : IEntityTypeConfiguration<Sehir>
{
    public void Configure(EntityTypeBuilder<Sehir> b)
    {
        b.ToTable("Sehirler");
        b.Property(x => x.Ad).HasMaxLength(100).IsRequired();
        b.Property(x => x.PlakaKodu).HasMaxLength(2);
        b.HasIndex(x => x.Ad);
    }
}

public class DavaTuruConfig : IEntityTypeConfiguration<DavaTuru>
{
    public void Configure(EntityTypeBuilder<DavaTuru> b)
    {
        b.ToTable("DavaTurleri");
        b.Property(x => x.Ad).HasMaxLength(100).IsRequired();
        b.HasIndex(x => x.Ad).IsUnique();
    }
}
