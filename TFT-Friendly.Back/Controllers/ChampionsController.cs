using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TFT_Friendly.Back.Models.Champions;
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
        
        #endregion ROUTES
    }
}