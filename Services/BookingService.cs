using BookingSystem.Data;
using BookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Services;

public class BookingService
{
    private readonly AppDbContext _context;

    public BookingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Booking>> GetAllAsync()
    {
        return await _context.Bookings.ToListAsync();
    }

    public async Task AddAsync(Booking booking)
    {
        var exists = await _context.Bookings
            .AnyAsync(b => b.Date == booking.Date);

        if (exists)
            throw new Exception("Time already booked");

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Booking booking)
    {
        var existing = await _context.Bookings.FindAsync(booking.Id);

        if (existing == null)
            throw new Exception("Booking not found");

        var exists = await _context.Bookings
            .AnyAsync(b => b.Id != booking.Id && b.Date == booking.Date);

        if (exists)
            throw new Exception("Time already booked");

        existing.Name = booking.Name;
        existing.Date = booking.Date;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking != null)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
}
