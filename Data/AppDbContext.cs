namespace BookingSystem.Data;

using Microsoft.EntityFrameworkCore;
using BookingSystem.Models;

public class AppDbContext : DbContext
{
    public DbSet<Booking> Bookings { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
