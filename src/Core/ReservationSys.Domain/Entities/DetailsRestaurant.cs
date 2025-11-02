namespace ReservationSys.Domain.Entities;

public class DetailsRestaurant:BaseEntity
{
    public string Adress { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Guid RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;

    public Menu Menu { get; set; } = null!;
}
