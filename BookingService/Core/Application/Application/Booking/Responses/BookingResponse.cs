using Application.Booking.Dtos;

namespace Application.Booking.responses
{
    public class BookingResponse : Response
    {
        public BookingDto? Data { get; set; }
    }
}
