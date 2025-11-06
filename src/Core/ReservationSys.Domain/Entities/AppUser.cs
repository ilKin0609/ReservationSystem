using Microsoft.AspNetCore.Identity;

namespace ReservationSys.Domain.Entities;

public class AppUser:IdentityUser
{
    public string FullName { get; set; }=null!;

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime LastLoginDate { get; set; } = DateTime.UtcNow;

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public ICollection<Reservation> Reservations { get; set; }=new List<Reservation>();  
    public ICollection<Restaurant> Restaurants { get; set; }=new List<Restaurant>();
    public ICollection<Favourite> Favourites { get; set; }=new List<Favourite>();
}
