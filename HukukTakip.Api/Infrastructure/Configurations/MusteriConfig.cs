using HukukTakip.Api.Domain.Entities;
using HukukTakip.Api.Domain.Entities.Sozluk;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HukukTakip.Api.Infrastructure.Configurations;

public class MusteriConfig : IEntityTypeConfiguration<Musteri>
{
    public void Configure(EntityTypeBuilder<Musteri> b)
    {
        b.ToTable("Musteriler");
        b.HasKey(x => x.Id);

        b.Property(x => x.MusteriNo).HasMaxLength(50).IsRequired();
        b.Property(x => x.AdiUnvani).HasMaxLength(200);
        b.Property(x => x.BorcluSoyadi).HasMaxLength(100);

        b.Property(x => x.TCKN).HasMaxLength(11).IsFixedLength();
        b.HasIndex(x => x.TCKN).IsUnique();
        b.Property(x => x.DogumYeri).HasMaxLength(100);
        b.Property(x => x.BabaAdi).HasMaxLength(100);
        b.Property(x => x.AnneAdi).HasMaxLength(100);

        b.Property(x => x.PasaportNumarasi).HasMaxLength(30);
        b.Property(x => x.NufusaKayitliOlduguIl).HasMaxLength(100);

        b.Property(x => x.Semt).HasMaxLength(150);

        b.Property(x => x.VergiDairesi).HasMaxLength(120);
        b.Property(x => x.VergiNo).HasMaxLength(20);
        b.Property(x => x.SSKNo).HasMaxLength(30);
        b.Property(x => x.SSKIsyeriNumarasi).HasMaxLength(30);
        b.Property(x => x.TicaretSicilNo).HasMaxLength(30);
        b.Property(x => x.BorcuTipi).HasMaxLength(50);

        b.HasIndex(x => x.MusteriNo).IsUnique();
        b.HasIndex(x => x.TCKN).IsUnique().HasFilter("[TCKN] IS NOT NULL");
        b.HasIndex(x => x.VergiNo).IsUnique().HasFilter("[VergiNo] IS NOT NULL");

        b.HasOne<Sehir>()
         .WithMany()
         .HasForeignKey(x => x.SehirId)
         .OnDelete(DeleteBehavior.Restrict);
    }
}
