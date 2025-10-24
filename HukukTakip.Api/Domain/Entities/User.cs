namespace HukukTakip.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SifreHash { get; set; } = string.Empty;
        public string Rol { get; set; } = "user"; // default: normal kullanıcı
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
    }
}
