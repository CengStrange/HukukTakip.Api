using HukukTakip.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HukukTakip.Api.Infrastructure.Configurations;

public class IhtarConfig : IEntityTypeConfiguration<Ihtar>
{
    public void Configure(EntityTypeBuilder<Ihtar> b)
    {
        b.ToTable("Ihtarlar");
        b.HasKey(x => x.Id);

        b.Property(x => x.NoterAdi).HasMaxLength(150);
        b.Property(x => x.IhtarNo).HasMaxLength(50);

        b.Property(x => x.IhtarnameNakitTutar).HasColumnType("decimal(18,2)");
        b.Property(x => x.IhtarnameGayriNakitTutar).HasColumnType("decimal(18,2)");

        b.HasIndex(x => x.YevmiyeNo).IsUnique().HasFilter("[YevmiyeNo] IS NOT NULL");
        b.HasIndex(x => x.IhtarNo).IsUnique().HasFilter("[IhtarNo] IS NOT NULL");

        b.HasOne(x => x.Musteri)
         .WithMany()
         .HasForeignKey(x => x.MusteriId)
         .OnDelete(DeleteBehavior.Restrict);

        b.HasIndex(x => new { x.NoterAdi, x.YevmiyeNo })
         .IsUnique()
         .HasFilter("[NoterAdi] IS NOT NULL AND [YevmiyeNo] IS NOT NULL");

    }
}
