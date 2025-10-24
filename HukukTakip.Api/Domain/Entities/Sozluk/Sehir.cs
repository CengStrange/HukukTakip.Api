namespace HukukTakip.Api.Domain.Entities.Sozluk;

public class Sehir
{
    public int Id { get; set; }
    public string Ad { get; set; } = null!;
    public string? PlakaKodu { get; set; }

    public ICollection<IcraDairesi> IcraDaireleri { get; set; } = new List<IcraDairesi>();
}

public class DavaTuru
{
    public int Id { get; set; }
    public string Ad { get; set; } = null!;
}

public class IcraDairesi
{
    public Guid Id { get; set; }           
    public string Ad { get; set; } = null!; 
    public int SehirId { get; set; }        
    public Sehir Sehir { get; set; } = null!;
}
public class Ilce
{
    public int Id { get; set; }
    public string Ad { get; set; } = null!;
    public int SehirId { get; set; }
    public Sehir Sehir { get; set; } = null!;
}

