using Application.Guest.Dtos;
using Application.Guest.Ports;
using Application.Guest.Request;
using Application.Guest.Responses;
using Domain.Guest.Exceptions;
using Domain.Guest.Ports;

namespace Application.Guest
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

                await guest.Save(_guestRepository);

                request.Data.Id = guest.Id;

                return new GuestResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (InvalidPersonDocumentIdException)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCodes = ErrorCodes.INVALID_PERSON_ID,
                    Message = "The ID passed is not valid"
                };
            }
            catch (MissingRequiredInformationExcpetion)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCodes = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                    Message = "Missing Required Information passed is not valid"
                };
            }
            catch (InvalidEmailException)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCodes = ErrorCodes.INVALID_EMAIL,
                    Message = "The given email is not valid"
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
        public async Task<GuestResponse> GetGuest(int guestId)
        {
            var guest = await _guestRepository.Get(guestId);
            if (guest == null) 
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCodes = ErrorCodes.GUEST_NOT_FOUND,
                    Message = "No Guest record was found with the given Id",
                };
            }

            return new GuestResponse
            {
                Data = GuestDTO.MapToDto(guest),
                Success = true,
            };

        }
    }
}
