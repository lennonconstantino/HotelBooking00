namespace Domain.Booking.Ports
{
    public interface IBookingRepository
    {
        Task<Guest.Entities.Booking> GetBooking(int id);
        Task<Guest.Entities.Booking> CreateBooking(Guest.Entities.Booking booking);

    }
}
