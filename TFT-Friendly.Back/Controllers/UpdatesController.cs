using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Champions;
using TFT_Friendly.Back.Models.Errors;
using TFT_Friendly.Back.Models.Updates;
using TFT_Friendly.Back.Services.Updates;

namespace TFT_Friendly.Back.Controllers
{
    /// <summary>
    /// UpdatesController class
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class UpdatesController : ControllerBase
    {
        #region MEMBERS

        private readonly IUpdateService _service;
        private readonly ILogger<ItemsController> _logger;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="UpdatesController"/> class
        /// </summary>
        /// <param name="service">The update service to use</param>
        /// <param name="logger">The logger to use</param>
        /// <exception cref="ArgumentNullException">Throw an exception if on parameter is null</exception>
        public UpdatesController(IUpdateService service, ILogger<ItemsController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        #endregion CONSTRUCTOR

        #region ROUTES
        
        /// <summary>
        /// Get a specific update
        /// </summary>
        /// <returns>The requested update</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(typeof(Update), StatusCodes.Status200OK)]
        [HttpGet("{identifier:long}")]
        public IActionResult GetUpdate(long identifier = 0)
        {
            return Ok(_service.GetUpdateByIdentifier(identifier));
        }

        /// <summary>
        /// Get the list of Updates
        /// </summary>
        /// <returns>The list of updates</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(typeof(List<Update>), StatusCodes.Status200OK)]
        [HttpGet("{from:long}")]
        public IActionResult GetUpdateList(long from = 0)
        {
            return Ok(_service.GetLastUpdates(from));
        }

        /// <summary>
        /// Get the last update identifier
        /// </summary>
        /// <returns>The last identifier</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetLastUpdateIdentifier()
        {
            return Ok(_service.GetLastUpdateIdentifier());
        }

        /// <summary>
        /// Register a new update
        /// </summary>
        /// <param name="updates">The updates to add</param>
        /// <returns>The newly create update</returns>
        /// <response code="200">Everything worked well</response>
        [HttpPost]
        public IActionResult PostNewUpdate(List<string> updates)
        {
            return Ok(_service.RegisterUpdate(updates));
        }
        
        #endregion ROUTES
    }
}