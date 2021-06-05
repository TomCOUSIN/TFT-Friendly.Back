using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TFT_Friendly.Back.Models.Users;
using TFT_Friendly.Back.Services.Mongo;

namespace TFT_Friendly.Back.Controllers
{
    /// <summary>
    /// UserController class
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region MEMBERS
        
        private readonly UsersMongoService _usersMongoService;
        private readonly ILogger<UsersController> _logger;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="UsersController"/> class
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="usersMongoService">The users mongo database</param>
        /// <exception cref="ArgumentNullException">Throw an exception if one of the parameter is null</exception>
        public UsersController(ILogger<UsersController> logger, UsersMongoService usersMongoService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _usersMongoService = usersMongoService ?? throw new ArgumentNullException(nameof(usersMongoService));
        }

        #endregion CONSTRUCTOR
        
        #region ROUTES

        /// <summary>
        /// Get all the users
        /// </summary>
        /// <returns>The list of users</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _usersMongoService.FindAsync());
        }

        /// <summary>
        /// Post a new user
        /// </summary>
        /// <param name="user">The user to add</param>
        /// <returns>The newly created user</returns>
        /// <response code="201">The user as been created successfully</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> PostUser(User user)
        {
            await _usersMongoService.InsertOneAsync(user);
            return CreatedAtAction(nameof(GetUsers), new {id = user.Id}, user);
        }

        /// <summary>
        /// Update a specific user
        /// </summary>
        /// <param name="user">The updated user</param>
        /// <returns>The updated user</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPatch]
        public IActionResult UpdateUser(User user)
        {
            _usersMongoService.ReplaceOneAsync(user);
            return Ok(user);
        }

        /// <summary>
        /// Delete a specific user
        /// </summary>
        /// <param name="user">The user to delete</param>
        /// <returns>The deleted user</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        public IActionResult RemoveUser(User user)
        {
            _usersMongoService.DeleteOneAsync(user);
            return Ok(user);
        }

        #endregion ROUTES
    }
}