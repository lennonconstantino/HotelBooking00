
using Application;
using Application.Guest.Dtos;
using Application.Guest.Ports;
using Application.Guest.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IGuestManager _guestManager;

        public GuestController(ILogger<GuestController> logger, IGuestManager guestManager)
        {
            _logger = logger;
            _guestManager = guestManager;
        }

        [HttpPost]
        public async Task<ActionResult<GuestDTO>> Post(GuestDTO guest)
        {
            var request = new CreateGuestRequest
            {
                Data = guest
            };

            var res = await _guestManager.CreateGuest(request);

            if (res.Success) return Created("", res.Data);

            if (res.ErrorCodes == ErrorCodes.NOT_FOUND) return NotFound(res);

            if (res.ErrorCodes == ErrorCodes.INVALID_PERSON_ID
                || res.ErrorCodes == ErrorCodes.INVALID_EMAIL
                || res.ErrorCodes == ErrorCodes.MISSING_REQUIRED_INFORMATION
                || res.ErrorCodes == ErrorCodes.COULD_NOT_STORE_DATA)
            {
                return BadRequest(res);
            }

            _logger.LogError("Reponse with unknown ErrorCode Returned", res);
            return BadRequest(500);
        }
    
        [HttpGet]
        public async Task<ActionResult<GuestDTO>> Get(int guestId)
        {
            var res = await _guestManager.GetGuest(guestId);
            if (res.Success) return Created("", res.Data);
            return NotFound(res);
        }
    }
}
