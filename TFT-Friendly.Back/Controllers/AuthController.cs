using System;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost("/login")]
        public IActionResult Login(User user)
        {
            return Ok(_userService.AuthenticateUser(user));
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="user">The user to register</param>
        /// <returns>The token of the user</returns>
        [HttpPost("/register")]
        public IActionResult Register(User user)
        {
            return Ok(_userService.RegisterUser(user));
        }

        #endregion ROUTES
    }
}