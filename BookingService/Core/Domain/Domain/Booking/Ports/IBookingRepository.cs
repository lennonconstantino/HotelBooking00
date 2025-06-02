namespace Domain.Booking.Ports
{
    public interface IBookingRepository
    {
        Task<Entities.Booking> GetBooking(int id);
        Task<Entities.Booking> CreateBooking(Entities.Booking booking);

    }
}
