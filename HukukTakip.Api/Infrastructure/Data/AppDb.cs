using HukukTakip.Api.Domain.Entities;
using HukukTakip.Api.Domain.Entities.Icra;
using HukukTakip.Api.Domain.Entities.Sozluk;
using HukukTakip.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HukukTakip.Api.Infrastructure.Data
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options) { }

    
        public DbSet<Musteri> Musteriler => Set<Musteri>();
        public DbSet<Ihtar> Ihtarlar => Set<Ihtar>();
        public DbSet<Avukat> Avukatlar => Set<Avukat>();
        public DbSet<Urun> Urunler => Set<Urun>();
        public DbSet<User> Users => Set<User>();


        public DbSet<Ilce> Ilceler => Set<Ilce>();
        public DbSet<Sehir> Sehirler => Set<Sehir>();
        public DbSet<DavaTuru> DavaTurleri => Set<DavaTuru>();
        public DbSet<IcraDairesi> IcraDaireleri => Set<IcraDairesi>();

        
        public DbSet<IcraDosyasi> IcraDosyalari => Set<IcraDosyasi>();  
        public DbSet<Sube> Subeler => Set<Sube>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDb).Assembly);

            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                d => d.ToDateTime(TimeOnly.MinValue),
                d => DateOnly.FromDateTime(d)
            );

            
     
          
          
       

            
        }
    }
}
