using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Errors;
using TFT_Friendly.Back.Models.Users;
using TFT_Friendly.Back.Services.Users;

namespace TFT_Friendly.Back.Controllers
{
    /// <summary>
    /// AuthController class
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region MEMBERS

        private readonly IUserService _userService;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="AuthController"/> class
        /// </summary>
        /// <param name="userService">The userService to use</param>
        public AuthController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        #endregion CONSTRUCTOR

        #region ROUTES

        /// <summary>
        /// Login a user
        /// </summary>
        /// <param name="user">The user to login</param>
        /// <returns>The token of the user</returns>
        /// <response code="200">Everything worked well.</response>
        /// <response code="400">Wrong password for this user.</response>
        /// <response code="404">The user was not found.</response>
        [HttpPost("/login")]
        [ProducesResponseType(typeof(UserToken), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        public IActionResult Login(User user)
        {
            try
            {
                var token = _userService.AuthenticateUser(user);
                return Ok(new UserToken{Token = token});
            }
            catch (UserNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
            catch (InvalidPasswordException exception)
            {
                return BadRequest(new HttpError(StatusCodes.Status400BadRequest, exception.Message));
            }
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="user">The user to register</param>
        /// <returns>The token of the user</returns>
        /// <response code="201">User registered successfully.</response>
        /// <response code="400">Wrong information format.</response>
        /// <response code="409">A user with this username already exist.</response> 
        [HttpPost("/register")]
        [ProducesResponseType(typeof(UserToken), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status409Conflict)]
        public IActionResult Register(User user)
        {
            try
            {
                var token = _userService.RegisterUser(user);
                return CreatedAtAction(nameof(Login), new UserToken{Token =  token});
            }
            catch (UserConflictException exception)
            {
                return Conflict(new HttpError(StatusCodes.Status409Conflict, exception.Message));
            }
            catch (PasswordFormatException exception)
            {
                return BadRequest(new HttpError(StatusCodes.Status400BadRequest, exception.Message));
            }
        }

        #endregion ROUTES
    }
}