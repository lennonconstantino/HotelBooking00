using Domain.Guest.Ports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Guest
{
    public class GuestRepository : IGuestRepository
    {
        private readonly HotelDbContext _hotelDbContext;
        public GuestRepository(HotelDbContext hotelDbContext)
        { 
            _hotelDbContext = hotelDbContext;
        }
        public async Task<int> Create(Domain.Guest.Entities.Guest guest)
        {
            _hotelDbContext.Guests.Add(guest);
            await _hotelDbContext.SaveChangesAsync();
            return guest.Id;
        }

        public Task<Domain.Guest.Entities.Guest> Get(int Id)
        {
            return _hotelDbContext.Guests.Where(g => g.Id == Id).FirstOrDefaultAsync();
        }
    }
}
