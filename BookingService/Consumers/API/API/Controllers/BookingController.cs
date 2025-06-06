using Application;
using Application.Booking.Dtos;
using Application.Booking.Ports;
using Application.Booking.Requests;
using Application.Room.Request;
using Domain.Room.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingManager _bookingManager;

        public BookingController(
            ILogger<BookingController> logger, 
            IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Post(BookingDto dto)
        {

            var request = new CreateBookingRequest
            {
                Data = dto,
            };

            var res = await _bookingManager.CreateBooking(request);

            if (res.Success) return Created("", res.Data);
            else if (res.ErrorCodes == ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION)
            {
                // Na vida real o consumidor costuma mudar a mensagem
                // motor de traducao ou outros dominios...
                return BadRequest(res);
            }
            else if (res.ErrorCodes == ErrorCodes.BOOKING_COULD_NOT_STORE_DATA)
            {
                return BadRequest(res);
            }

            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest(500);
        }
    }
}
