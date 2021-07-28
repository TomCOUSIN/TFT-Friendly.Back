using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Abilities;
using TFT_Friendly.Back.Models.Errors;
using TFT_Friendly.Back.Services.Ability;

namespace TFT_Friendly.Back.Controllers
{
    /// <summary>
    /// AbilityController class
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class AbilityController : ControllerBase
    {
        #region MEMBERS
        
        private readonly IAbilityService _abilityService;
        
        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="AbilityController"/> class
        /// </summary>
        /// <param name="abilityService">The ability service to use</param>
        /// <exception cref="ArgumentNullException">Throw an exception if one parameter is null</exception>
        public AbilityController(IAbilityService abilityService)
        {
            _abilityService = abilityService ?? throw new ArgumentNullException(nameof(abilityService));
        }

        #endregion CONSTRUCTOR

        #region ROUTES

        #region ABILITY

        /// <summary>
        /// Get the list of ability
        /// </summary>
        /// <returns>The list of ability</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(typeof(List<Ability>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetAllAbility()
        {
            return Ok(_abilityService.GetAllAbilities());
        }

        /// <summary>
        /// Get an ability
        /// </summary>
        /// <returns>The ability</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Ability not found</response>
        [ProducesResponseType(typeof(Ability), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpGet("{key}")]
        public IActionResult GetAbility(string key)
        {
            try
            {
                return Ok(_abilityService.GetAbility(key));
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }

        /// <summary>
        /// Add an ability
        /// </summary>
        /// <returns>The ability created</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="409">Ability with same key already exists</response>
        [ProducesResponseType(typeof(Ability), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status409Conflict)]
        [HttpPost]
        public IActionResult AddAbility(Ability ability)
        {
            try
            {
                return Ok(_abilityService.AddAbility(ability));
            }
            catch (EntityConflictException exception)
            {
                return Conflict(new HttpError(StatusCodes.Status409Conflict, exception.Message));
            }
        }

        /// <summary>
        /// Update an ability
        /// </summary>
        /// <returns>The updated ability</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Ability not found</response>
        [ProducesResponseType(typeof(Ability), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpPatch("{key}")]
        public IActionResult UpdateAbility(string key, Ability ability)
        {
            try
            {
                return Ok(_abilityService.UpdateAbility(key, ability));
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }

        /// <summary>
        /// Update an ability
        /// </summary>
        /// <returns>Nothing</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Ability not found</response>
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpDelete("{key}")]
        public IActionResult DeleteAbility(string key)
        {
            try
            {
                _abilityService.DeleteAbility(key);
                return Ok();
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }

        #endregion ABILITY

        #region ABILITY_EFFECT

        /// <summary>
        /// Get the list of ability effect
        /// </summary>
        /// <returns>The list of ability effect</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(typeof(List<AbilityEffect>), StatusCodes.Status200OK)]
        [HttpGet("/effect")]
        public IActionResult GetAllAbilityEffect()
        {
            return Ok(_abilityService.GetAllAbilityEffects());
        }
        
        /// <summary>
        /// Get an ability effect
        /// </summary>
        /// <returns>The ability effect</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">AbilityEffect not found</response>
        [ProducesResponseType(typeof(AbilityEffect), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpGet("/effect/{key}")]
        public IActionResult GetAbilityEffect(string key)
        {
            try
            {
                return Ok(_abilityService.GetAbilityEffect(key));
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }

        /// <summary>
        /// Add an ability effect
        /// </summary>
        /// <param name="effect">The ability effect to add</param>
        /// <returns>The ability effect created</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="409">AbilityEffect with same key already exists</response>
        [ProducesResponseType(typeof(AbilityEffect), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status409Conflict)]
        [HttpPost("/effect")]
        public IActionResult AddAbilityEffect(AbilityEffect effect)
        {
            try
            {
                return Ok(_abilityService.AddAbilityEffect(effect));
            }
            catch (EntityConflictException exception)
            {
                return Conflict(new HttpError(StatusCodes.Status409Conflict, exception.Message));
            }
        }

        /// <summary>
        /// Update an ability effect
        /// </summary>
        /// <returns>The updated ability effect</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">AbilityEffect not found</response>
        [ProducesResponseType(typeof(AbilityEffect), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpPatch("/effect/{key}")]
        public IActionResult UpdateAbilityEffect(string key, AbilityEffect effect)
        {
            try
            {
                return Ok(_abilityService.UpdateAbilityEffect(key, effect));
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }

        /// <summary>
        /// Update an ability effect
        /// </summary>
        /// <returns>Nothing</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">AbilityEffect not found</response>
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpDelete("/effect/{key}")]
        public IActionResult DeleteAbilityEffect(string key)
        {
            try
            {
                _abilityService.DeleteAbilityEffect(key);
                return Ok();
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }

        #endregion ABILITY_EFFECT

        #endregion ROUTES
    }
}