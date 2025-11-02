namespace ReservationSys.Domain.Entities;

public class Favourite:BaseEntity
{
    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;

    public Guid RestuarantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;
}
