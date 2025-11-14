namespace ReservationSys.Domain.Entities;

public class Restaurant:BaseEntity
{
    public string Name { get; set; } = null!;

    public decimal Latitude { get; set; }

    public string? Slug { get; set; }

    public decimal Longitude { get; set; }

    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public DetailsRestaurant DetailsRestaurant { get; set; } = null!;

    public ICollection<Table> Tables { get; set; } = new List<Table>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
}
