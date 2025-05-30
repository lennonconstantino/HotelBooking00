using Application.Guest.DTO;
using Application.Guest.Ports;
using Application.Guest.Request;
using Application.Guest.Responses;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class GuestManager : IGuestManager
    {
        IGuestRepository _guestRepository;
        public GuestManager(IGuestRepository guestRepository) 
        { 
            _guestRepository = guestRepository;
        }
        public async Task<GuestResponse> CreateGuest(CreateGuestRequest request)
        {
            try
            {
                // preciso criar um repository
                var guest = GuestDTO.MapToEntity(request.Data);

                request.Data.Id = await _guestRepository.Create(guest);

                return new GuestResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (Exception)
            {
                // Trabalhar outros exceptions
                return new GuestResponse
                {
                    Success = false,
                    ErrorCodes = ErrorCodes.COULD_NOT_STORE_DATA,
                    Message = "There was an error when saving to db"
                };
            }
        }
    }
}
