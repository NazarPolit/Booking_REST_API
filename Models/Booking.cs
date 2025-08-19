namespace Booking_REST_API.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string? GuestName { get; set; }
        public int? RoomNumber { get; set; } 
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public bool? IsPaid { get; set; }
    }
}
