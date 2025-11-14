namespace ReservationSys.Domain.Entities;

public class Category:BaseEntity
{
    public string Name { get; set; } = null!;

    public string? Slug { get; set; }

    public string Description { get; set; }=null!;

    public Guid? ParentCategoryId { get; set; }

    public Category? ParentCategory { get; set; }

    public ICollection<Category> SubCategories { get; set; }=new List<Category>();

    public ICollection<Restaurant> Restaurants { get; set; }=new List<Restaurant>();
}
