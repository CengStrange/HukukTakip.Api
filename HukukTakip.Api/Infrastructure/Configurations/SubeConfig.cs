using HukukTakip.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HukukTakip.Api.Infrastructure.Configurations;

public class SubeConfig : IEntityTypeConfiguration<Sube>
{
    public void Configure(EntityTypeBuilder<Sube> builder)
    {
        builder.ToTable("Subeler");

        builder.HasKey(s => s.BrmKod);

        builder.Property(s => s.BrmKod).ValueGeneratedNever();


        builder.Property(s => s.BrmAd)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(s => s.IlKod)
            .IsRequired();
    }
}
