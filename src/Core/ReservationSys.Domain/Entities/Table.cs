namespace ReservationSys.Domain.Entities;

public class Table:BaseEntity
{
    public int TableNumber { get; set; }

    public int Capacity { get; set; }

    public bool IsAvailable { get; set; } = true;

    public Guid RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
