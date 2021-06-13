using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFT_Friendly.Back.Attributes;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Errors;
using TFT_Friendly.Back.Models.Users;
using TFT_Friendly.Back.Services.Users;

namespace TFT_Friendly.Back.Controllers
{
    /// <summary>
    /// UserController class
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        #region MEMBERS

        private readonly IUserService _userService;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="UsersController"/> class
        /// </summary>
        /// <param name="userService">The user service to manage users</param>
        /// <exception cref="ArgumentNullException">Throw an exception if one of the parameter is null</exception>
        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        #endregion CONSTRUCTOR
        
        #region ROUTES

        /// <summary>
        /// Get user information
        /// </summary>
        /// <returns>The information of the user</returns>
        /// <response code="200">Everything worked well.</response>
        /// <response code="401">Unauthorized request for this user.</response>
        [HttpGet("/me")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status401Unauthorized)]
        public IActionResult GetMe()
        {
            return Ok(_userService.GetMe((string)HttpContext.Items["UserId"]));
        }

        /// <summary>
        /// Patch the user information
        /// </summary>
        /// <param name="user">The new information of the user</param>
        /// <returns>The user with new information</returns>
        /// <response code="200">Everything worked well.</response>
        /// <response code="400">Wrong information format.</response>
        /// <response code="401">Unauthorized request for this user.</response>
        [HttpPatch("/me")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status401Unauthorized)]
        public IActionResult PatchMe(User user)
        {
            try
            {
                _userService.PatchMe((string) HttpContext.Items["UserId"], user);
                return Ok(user);
            }
            catch (PasswordFormatException exception)
            {
                return BadRequest(new HttpError(StatusCodes.Status400BadRequest, exception.Message));
            }
        }

        /// <summary>
        /// Delete the user
        /// </summary>
        /// <returns>Nothing is return</returns>
        /// <response code="200">Everything worked well.</response>
        /// <response code="401">Unauthorized request for this user.</response>
        [HttpDelete("/me")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteMe()
        {
            _userService.DeleteMe((string) HttpContext.Items["UserId"]);
            return Ok();
        }

        #endregion ROUTES
    }
}