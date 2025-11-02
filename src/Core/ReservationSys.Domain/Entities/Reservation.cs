using ReservationSys.Domain.Enums;

namespace ReservationSys.Domain.Entities;

public class Reservation:BaseEntity
{
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public ReservationStatus ReservationStatus { get; set; } =ReservationStatus.Pending;

    public string AppUserId { get; set; }=null!;
    public AppUser AppUser { get; set; } = null!;

    public Guid RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }= null!;

    public Guid TableId { get; set; }
    public Table Table { get; set; } = null!;
}
