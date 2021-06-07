using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TFT_Friendly.Back.Attributes;
using TFT_Friendly.Back.Models.Users;
using TFT_Friendly.Back.Services.Mongo;
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
        private readonly ILogger<UsersController> _logger;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="UsersController"/> class
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="userService">The user service to manage users</param>
        /// <exception cref="ArgumentNullException">Throw an exception if one of the parameter is null</exception>
        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        #endregion CONSTRUCTOR
        
        #region ROUTES

        [HttpGet("/me")]
        public IActionResult GetMe()
        {
            return Ok(_userService.GetMe((string)HttpContext.Items["UserId"]));
        }

        #endregion ROUTES
    }
}