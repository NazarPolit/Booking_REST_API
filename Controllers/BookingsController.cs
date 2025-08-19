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
                return NotFound(new { message = "Not Found" });

            return await _context.Bookings.ToListAsync();
        }

        //GET: api/Bookings/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            if (_context.Bookings == null)
                return NotFound(new { message = "Not Found" });

            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound(new { message = "Not Found" });

            return booking;
        }

        //POST: api/Bookings
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(Booking booking)
        {
            if (_context.Bookings == null)
                return Problem("Entity set 'BookingsContext.Booking' is null.");

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        //DELETE: api/Bookings/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            if (_context.Bookings == null)
                return NotFound(new { message = "Not Found" });

            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound(new { message = "Not Found" });

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking deleted." });
        }

        //PUT: api/Bookings/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, Booking booking)
        {
            if (id != booking.Id)
                return BadRequest();

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(booking.Id))
                    return NotFound(new { message = "Not Found" });

                else throw;
            }

            return Ok(new { message = "Booking updated successfully." });

        }

        //PATCH: api/Bookings/1
        [HttpPatch("{id}")]
        public async Task<ActionResult<Booking>> PatchBooking(int id, Booking booking)
        {
            var existingBooking = await _context.Bookings.FindAsync(id);

            if (existingBooking == null)
                return NotFound(new { message = "Not Found" });
            
            if (booking.GuestName != null)
                existingBooking.GuestName = booking.GuestName;

            if (booking.RoomNumber != null)
                existingBooking.RoomNumber = booking.RoomNumber;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(booking.Id))
                    return NotFound(new { message = "Not Found" });

                else throw;
            }

            return Ok(new { message = "Booking updated successfully." });

        }
        private bool BookingExists(int id)
        {
            return (_context.Bookings?.Any(b => b.Id == id)).GetValueOrDefault();
        }
        
    }
}
