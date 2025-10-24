using HukukTakip.Api.Domain.Entities.Icra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HukukTakip.Api.Infrastructure.Configurations;


public class IcraDosyasiConfig : IEntityTypeConfiguration<IcraDosyasi>
{
    public void Configure(EntityTypeBuilder<IcraDosyasi> b)
    {
        b.ToTable("IcraDosyalari");
        b.HasKey(x => x.Id);


        b.HasOne(icra => icra.Musteri)
            .WithMany()
            .HasForeignKey(icra => icra.MusteriId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(icra => icra.Avukat)
            .WithMany()
            .HasForeignKey(icra => icra.AvukatId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);


        b.Property(x => x.DosyaNo)
            .IsRequired()
            .HasMaxLength(50);
        b.HasIndex(x => x.DosyaNo).IsUnique();

        b.Property(x => x.AvukatTevziNo).HasMaxLength(50);


        b.Property(x => x.TakipTarihi);

        b.Property(x => x.TakipTipi)
            .HasConversion<string>()
            .HasMaxLength(50);

        b.Property(x => x.IhtarBorclulari).HasMaxLength(500);
        b.Property(x => x.IhtarKonusuUrunler).HasMaxLength(500);
        b.Property(x => x.IcraMudurlugu).HasMaxLength(150);
        b.Property(x => x.MahiyetKodu).HasConversion<string>()
            .HasMaxLength(50);

        b.Property(x => x.Durum)
            .HasConversion<int>();

        b.Property(x => x.OlusturmaTarihiUtc)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
