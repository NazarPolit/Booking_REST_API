using Microsoft.EntityFrameworkCore;
using Booking_REST_API.Models;

namespace Booking_REST_API.DataAccess
{
    public class BookingContext: DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> contextOptions): base(contextOptions) 
        { }
        public DbSet<Booking> Bookings { get; set; }
        
    }
}
