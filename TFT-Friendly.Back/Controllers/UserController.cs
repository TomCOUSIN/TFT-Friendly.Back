using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TFT_Friendly.Back.Controllers
{
    /// <summary>
    /// UserController class
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Get a user
        /// </summary>
        /// <returns>An empty string</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetEmptyUserName()
        {
            return Ok("");
        }
    }
}