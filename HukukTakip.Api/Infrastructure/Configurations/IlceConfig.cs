using HukukTakip.Api.Domain.Entities.Sozluk;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HukukTakip.Api.Infrastructure.Configurations;

public class IlceConfig : IEntityTypeConfiguration<Ilce>
{
    public void Configure(EntityTypeBuilder<Ilce> b)
    {
        b.ToTable("Ilceler");
        b.HasKey(x => x.Id);
        b.Property(x => x.Ad).IsRequired().HasMaxLength(100);

        // Bir ilçe bir şehire aittir.
        b.HasOne(i => i.Sehir)
            .WithMany() // Bir şehrin birden çok ilçesi olabilir (bu ilişkiyi Sehir entity'sinde tanımlamaya gerek yok)
            .HasForeignKey(i => i.SehirId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); // İlişkili ilçesi olan bir şehir silinemez.
    }
}
