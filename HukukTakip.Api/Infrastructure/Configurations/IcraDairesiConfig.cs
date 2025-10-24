using HukukTakip.Api.Domain.Entities.Sozluk;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class IcraDairesiConfig : IEntityTypeConfiguration<IcraDairesi>
{
    public void Configure(EntityTypeBuilder<IcraDairesi> b)
    {
        b.ToTable("IcraDaireleri");
        b.HasKey(x => x.Id);

        b.Property(x => x.Id).ValueGeneratedNever(); 
        b.Property(x => x.Ad)
          .HasColumnName("UYAP_BRM_TUR_ACKLM")
          .HasMaxLength(500).IsRequired();

        b.Property(x => x.SehirId)
          .HasColumnName("UYAP_IL_KOD")
          .IsRequired();

        b.HasOne(x => x.Sehir)
         .WithMany(s => s.IcraDaireleri)
         .HasForeignKey(x => x.SehirId)
         .OnDelete(DeleteBehavior.Restrict);
    }
}
