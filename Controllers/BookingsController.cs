using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Booking_REST_API.Models;
using Booking_REST_API.DataAccess;
using System.Runtime.InteropServices;

namespace Booking_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly BookingContext _context;

        public BookingsController(BookingContext context)
        {
            _context = context;
        }

        //GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            if (_context.Bookings == null)
            {
                return NotFound(new { message = "Not Found" });
            }
            return await _context.Bookings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            if(_context.Bookings == null)
            {
                return NotFound(new { message = "Not Found" });
            }

            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null) 
            {
                return NotFound(new { message = "Not Found" });
            }

            return booking;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Booking>> CreateBooking(Booking booking)
        {
            if (_context.Bookings == null)
            {
                return Problem("Entity set 'BookingsContext.Booking' is null.");
            }
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

    }
}
