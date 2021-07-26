using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Champions;
using TFT_Friendly.Back.Models.Errors;
using TFT_Friendly.Back.Services.Champions;

namespace TFT_Friendly.Back.Controllers
{
    /// <summary>
    /// ChampionsController class
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ChampionsController : ControllerBase
    {
        #region MEMBERS

        private readonly IChampionService _championService;
        private readonly ILogger<ItemsController> _logger;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="ChampionsController"/> class
        /// </summary>
        /// <param name="championService">The champion service to use</param>
        /// <param name="logger">The logger to use</param>
        /// <exception cref="ArgumentNullException">Throw an exception if on parameter is null</exception>
        public ChampionsController(IChampionService championService, ILogger<ItemsController> logger)
        {
            _championService = championService ?? throw new ArgumentNullException(nameof(championService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        #endregion CONSTRUCTOR

        #region ROUTES

        /// <summary>
        /// Get the list of Champions
        /// </summary>
        /// <returns>The list of champions</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(typeof(List<Champion>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetChampionList()
        {
            return Ok(_championService.GetChampionList());
        }
        
        /// <summary>
        /// Add a list of new champion
        /// </summary>
        /// <returns>The newly added champions</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(typeof(List<Champion>), StatusCodes.Status200OK)]
        [HttpPost]
        public IActionResult AddNewChampions(List<Champion> champions)
        {
            var addedChampions = new List<Champion>();
                
            foreach (var champion in champions)
            {
                try
                {
                    var addedChampion = _championService.AddChampion(champion);
                    addedChampions.Add(addedChampion);
                }
                catch (ChampionConflictException exception)
                {
                    _logger.LogError(exception.Message);
                }
            }
            return Ok(addedChampions);
        }

        /// <summary>
        /// Get a specific champion
        /// </summary>
        /// <returns>The requested champion</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Champion not found</response>
        [ProducesResponseType(typeof(Champion), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpGet("{key}")]
        public IActionResult GetChampion(string key)
        {
            try
            {
                return Ok(_championService.GetChampion(key));
            }
            catch (ChampionNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }

        /// <summary>
        /// Update a champion
        /// </summary>
        /// <param name="key">The key of the champion to update</param>
        /// <param name="champion">The champion to update</param>
        /// <returns>The updated champion</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Champion not found</response>
        [ProducesResponseType(typeof(Champion), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpPatch("{key}")]
        public IActionResult UpdateItem(string key, Champion champion)
        {
            try
            {
                return Ok(_championService.UpdateChampion(key, champion));
            }
            catch (ChampionNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }
        
        /// <summary>
        /// Delete a champion
        /// </summary>
        /// <param name="key">The key of the champion to delete</param>
        /// <returns>Nothing</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Champion not found</response>
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpDelete("{key}")]
        public IActionResult DeleteItem(string key)
        {
            try
            {
                _championService.DeleteChampion(key);
                return Ok();
            }
            catch (ChampionNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }
        
        #endregion ROUTES
    }
}