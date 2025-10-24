using HukukTakip.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HukukTakip.Api.Infrastructure.Configurations;

public class AvukatConfig : IEntityTypeConfiguration<Avukat>
{
    public void Configure(EntityTypeBuilder<Avukat> b)
    {
        b.ToTable("Avukatlar");
        b.HasKey(x => x.Id);

        // İlişkiler
        b.HasOne(a => a.AvansHesapSube)
            .WithMany()
            .HasForeignKey(a => a.AvansHesapSubeId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(a => a.VadesizHesapSube)
            .WithMany()
            .HasForeignKey(a => a.VadesizHesapSubeId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        b.Property(x => x.Adi).IsRequired().HasMaxLength(100);
        b.Property(x => x.Soyadi).IsRequired().HasMaxLength(100);
        b.Property(x => x.TCKN).HasMaxLength(11).IsFixedLength();
        b.HasIndex(x => x.TCKN).IsUnique(); 
        b.Property(x => x.VergiDairesi).HasMaxLength(150);
        b.Property(x => x.VergiNo).HasMaxLength(10);
        b.HasIndex(x => x.VergiNo).IsUnique();

        b.Property(x => x.Email).HasMaxLength(200);
        b.Property(x => x.CepTelefonu).HasMaxLength(20);
        b.Property(x => x.IsTelefonu).HasMaxLength(20);
        b.Property(x => x.IsFaxNo).HasMaxLength(20);
        b.Property(x => x.TamAdres).HasMaxLength(500);

        b.Property(x => x.AvansHesapNo).HasMaxLength(50);
        b.Property(x => x.VadesizHesapNo).HasMaxLength(50);
        b.Property(x => x.HalkbankVadesizIbanNo).HasMaxLength(34); // TR dahil 26 + 8 = 34
        b.Property(x => x.DigerBankaIbanNo).HasMaxLength(34);

        b.Property(x => x.AvansLimiti).HasColumnType("decimal(18,2)");

        b.Property(x => x.AvukatTipi)
            .HasConversion<string>()
            .HasMaxLength(50);
    }
}

