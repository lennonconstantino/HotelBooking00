using Domain.Booking.Ports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Data.Booking
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDbContext _hotelDbContext;
        public BookingRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }
        public async Task<Domain.Entities.Booking> CreateBooking(Domain.Entities.Booking booking)
        {
            _hotelDbContext.Bookings.Add(booking);
            await _hotelDbContext.SaveChangesAsync();
            return booking;
        }

        public Task<Domain.Entities.Booking> GetBooking(int id)
        {
            return _hotelDbContext.Bookings.Where(x => x.Id == id).FirstAsync();
        }
    }
}
