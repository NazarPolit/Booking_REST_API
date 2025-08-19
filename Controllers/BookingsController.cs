using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Booking_REST_API.Models;
using Booking_REST_API.DataAccess;

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
            if(_context.Bookings == null)
            {
                return NotFound(new {message = "Not Found"});
            }
            return await _context.Bookings.ToListAsync();
        }
    }
}
