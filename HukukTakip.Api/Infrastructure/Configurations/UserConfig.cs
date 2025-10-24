using HukukTakip.Api.Domain.Entities;
using HukukTakip.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HukukTakip.Api.Infrastructure.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.ToTable("Users");
            b.HasKey(x => x.Id);

            b.Property(x => x.KullaniciAdi).IsRequired().HasMaxLength(100);
            b.Property(x => x.Email).IsRequired().HasMaxLength(150);
            b.Property(x => x.SifreHash).IsRequired();
            b.Property(x => x.Rol).HasMaxLength(20);
            b.Property(x => x.KayitTarihi).HasDefaultValueSql("GETUTCDATE()");

            b.HasIndex(x => x.Email).IsUnique();
            b.HasIndex(x => x.KullaniciAdi).IsUnique();

            // --- SABİT SEED DEĞERLERİ ---
            var adminCreatedAtUtc = new DateTime(2025, 09, 30, 00, 00, 00, DateTimeKind.Utc);
            var fixedHash = "$2a$11$DJUpOEmwBejBEoH.cGQQGueUPIhIWYHbpXkiTK7edw2fL.r9pC472"; // Admin123!

            b.HasData(new User
            {
                Id = 1,
                KullaniciAdi = "admin",
                Email = "admin@demo.com",
                SifreHash = fixedHash,
                Rol = "admin",
                KayitTarihi = adminCreatedAtUtc
            });
        }
    }
}
