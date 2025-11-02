namespace ReservationSys.Domain.Entities;

public class Menu:BaseEntity
{
    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public Guid DetailsRestaurantId { get; set; }
    public DetailsRestaurant DetailsRestaurant { get; set; } = null!;
}
