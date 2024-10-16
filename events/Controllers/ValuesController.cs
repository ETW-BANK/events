using events.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IEvents _events;

        public ValuesController(IEvents events)
        {
            _events = events;
        }

        [HttpGet]
        [Route("events")]
        [ProducesResponseType(typeof(Dto.Root), 200)]

        public async Task<IActionResult> GetEventsByCity(string type, string city)
        {
            try
            {
                var events = await _events  .GetEvents(type, city);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving Events: {ex.Message}");
            }
        }
    }
}
