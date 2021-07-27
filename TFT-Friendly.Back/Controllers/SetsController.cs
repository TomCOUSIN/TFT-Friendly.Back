using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Errors;
using TFT_Friendly.Back.Models.Sets;
using TFT_Friendly.Back.Services.Sets;

namespace TFT_Friendly.Back.Controllers
{
    /// <summary>
    /// SetsController class
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class SetsController : ControllerBase
    {
        #region MEMBERS

        private readonly ISetService _setsService;
        private readonly ILogger<ItemsController> _logger;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="SetsController"/> class
        /// </summary>
        /// <param name="setService">The set service to use</param>
        /// <param name="logger">The logger to use</param>
        /// <exception cref="ArgumentNullException">Throw an exception if on parameter is null</exception>
        public SetsController(ISetService setService, ILogger<ItemsController> logger)
        {
            _setsService = setService ?? throw new ArgumentNullException(nameof(setService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion CONSTRUCTOR

        #region ROUTES

        /// <summary>
        /// Get the list of sets
        /// </summary>
        /// <returns>The list of sets</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(typeof(List<Set>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetSetList()
        {
            return Ok(_setsService.GetSetList());
        }

        /// <summary>
        /// Add a list of new set
        /// </summary>
        /// <returns>The newly added sets</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(typeof(List<Set>), StatusCodes.Status200OK)]
        [HttpPost]
        public IActionResult AddNewSets(List<Set> sets)
        {
            var addedSets = new List<Set>();

            foreach (var set in sets)
            {
                try
                {
                    var addedSet = _setsService.AddSet(set);
                    addedSets.Add(addedSet);
                }
                catch (EntityConflictException exception)
                {
                    _logger.LogError(exception.Message);
                }
            }

            return Ok(addedSets);
        }

        /// <summary>
        /// Get a specific set
        /// </summary>
        /// <param name="key">The key of the set to get</param>
        /// <returns>The requested set</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Item not found</response>
        [ProducesResponseType(typeof(Set), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpGet("{key}")]
        public IActionResult GetSet(string key)
        {
            try
            {
                return Ok(_setsService.GetSet(key));
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }

        /// <summary>
        /// Update a set
        /// </summary>
        /// <param name="key">The key of the set to update</param>
        /// <param name="item">The set to update</param>
        /// <returns>The updated set</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Item not found</response>
        [ProducesResponseType(typeof(Set), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpPatch("{key}")]
        public IActionResult UpdateSet(string key, Set set)
        {
            try
            {
                return Ok(_setsService.UpdateSet(key, set));
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }

        /// <summary>
        /// Delete a set
        /// </summary>
        /// <param name="key">The key of the set to delete</param>
        /// <returns>Nothing</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Item not found</response>
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpDelete("{key}")]
        public IActionResult DeleteSet(string key)
        {
            try
            {
                _setsService.DeleteSet(key);
                return Ok();
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }

        #endregion ROUTES
    }
}