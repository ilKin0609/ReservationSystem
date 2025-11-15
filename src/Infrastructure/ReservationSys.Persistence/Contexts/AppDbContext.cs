using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservationSys.Domain.Entities;

namespace ReservationSys.Persistence.Context;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Restaurant> Restaurants { get; set; }=null!;
    public DbSet<Category> Categories { get; set; }=null!;
   
    public DbSet<Reservation> Reservations { get; set; }=null!;
    
    public DbSet<Favourite> Favourites { get; set; }=null!;
    
    public DbSet<Table> Tables { get; set; }=null!;
    
    public DbSet<DetailsRestaurant> DetailsRestaurants { get; set; }=null!;
    
    public DbSet<Menu> Menus { get; set; }=null!;
}
