using HukukTakip.Api.Domain.Entities;
using HukukTakip.Api.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HukukTakip.Api.Infrastructure.Configurations;

public class UrunConfig : IEntityTypeConfiguration<Urun>
{
    public void Configure(EntityTypeBuilder<Urun> b)
    {
        b.ToTable("Urunler");
        b.HasKey(x => x.Id);

        // İlişkiler
        b.HasOne(u => u.Musteri)
            .WithMany()
            .HasForeignKey(u => u.MusteriId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(u => u.Avukat)
            .WithMany()
            .HasForeignKey(u => u.AvukatId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        b.HasOne(u => u.KrediBirimSube)
            .WithMany()
            .HasForeignKey(u => u.KrediBirimKoduSubeId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(u => u.TakipBirimSube)
            .WithMany()
            .HasForeignKey(u => u.TakipBirimKoduSubeId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        b.Property(x => x.UrunTipi).HasConversion<string>().HasMaxLength(50);
        b.Property(x => x.TakipMiktari).HasColumnType("decimal(18,2)");
        b.Property(x => x.DovizTipi).HasMaxLength(10);
        b.Property(x => x.AylikFaizOrani).HasColumnType("decimal(18,4)");
        b.Property(x => x.FaizBakiyesi).HasColumnType("decimal(18,2)");
        b.Property(x => x.MasrafBakiyesi).HasColumnType("decimal(18,2)");
    
        b.Property(x => x.KrediMudiNo).HasMaxLength(50);
        b.Property(x => x.MasrafMudiNo).HasMaxLength(50);
        b.Property(x => x.FaizMudiNo).HasMaxLength(50);
        b.Property(x => x.TakipMudiNo).HasMaxLength(50);

        b.Property(x => x.Aciklama).HasMaxLength(500);
    }
}

