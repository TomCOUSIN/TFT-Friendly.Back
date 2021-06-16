using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Errors;
using TFT_Friendly.Back.Models.Items;
using TFT_Friendly.Back.Models.Traits;
using TFT_Friendly.Back.Services.Traits;

namespace TFT_Friendly.Back.Controllers
{
    /// <summary>
    /// TraitsController class
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class TraitsController : ControllerBase
    {
        #region MEMBERS
        
        private readonly ITraitService _traitService;
        private readonly ILogger<TraitsController> _logger;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="TraitsController"/> class
        /// </summary>
        /// <param name="traitService">The trait service to use</param>
        /// <param name="logger">The logger to use</param>
        /// <exception cref="ArgumentNullException">Throw an exception if on parameter is null</exception>
        public TraitsController(ITraitService traitService, ILogger<TraitsController> logger)
        {
            _traitService = traitService ?? throw new ArgumentNullException(nameof(traitService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion CONSTRUCTOR

        #region ROUTES

        /// <summary>
        /// Get the list of traits
        /// </summary>
        /// <returns>The list of traits</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(typeof(List<Trait>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetTraitList()
        {
            return Ok(_traitService.GetTraitList());
        }

        /// <summary>
        /// Add a list of new traits
        /// </summary>
        /// <returns>The newly added traits</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(typeof(List<Trait>), StatusCodes.Status200OK)]
        [HttpPost]
        public IActionResult AddNewTraits(List<Trait> traits)
        {
            var addedTraits = new List<Trait>();
                
            foreach (var trait in traits)
            {
                try
                {
                    var addedTrait = _traitService.AddTrait(trait);
                    addedTraits.Add(addedTrait);
                }
                catch (TraitConflictException exception)
                {
                    _logger.LogError(exception.Message);
                }
            }
            return Ok(addedTraits);
        }

        /// <summary>
        /// Get a specific trait
        /// </summary>
        /// <returns>The requested trait</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Trait not found</response>
        [ProducesResponseType(typeof(Trait), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpGet("{key}")]
        public IActionResult GetTrait(string key)
        {
            try
            {
                return Ok(_traitService.GetTrait(key));
            }
            catch (TraitNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }

        /// <summary>
        /// Update a trait
        /// </summary>
        /// <param name="key">The key of the trait to update</param>
        /// <param name="trait">The trait to update</param>
        /// <returns>The updated trait</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Trait not found</response>
        [ProducesResponseType(typeof(Trait), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpPatch("{key}")]
        public IActionResult UpdateItem(string key, Trait trait)
        {
            try
            {
                return Ok(_traitService.UpdateTrait(key, trait));
            }
            catch (TraitNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }
        
        /// <summary>
        /// Delete a trait
        /// </summary>
        /// <param name="key">The key of the trait to delete</param>
        /// <returns>Nothing</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Trait not found</response>
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpDelete("{key}")]
        public IActionResult DeleteItem(string key)
        {
            try
            {
                _traitService.DeleteTrait(key);
                return Ok();
            }
            catch (TraitNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }

        #endregion ROUTES
    }
}