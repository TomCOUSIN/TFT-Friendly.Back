using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Errors;
using TFT_Friendly.Back.Models.Items;
using TFT_Friendly.Back.Services.Items;

namespace TFT_Friendly.Back.Controllers
{
    /// <summary>
    /// ItemsController class
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        #region MEMBERS

        private readonly IItemService _itemService;
        private readonly ILogger<ItemsController> _logger;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="ItemsController"/> class
        /// </summary>
        /// <param name="itemService">The item service to use</param>
        /// <param name="logger">The logger to use</param>
        /// <exception cref="ArgumentNullException">Throw an exception if on parameter is null</exception>
        public ItemsController(IItemService itemService, ILogger<ItemsController> logger)
        {
            _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion CONSTRUCTOR

        #region ROUTES

        /// <summary>
        /// Get the list of items
        /// </summary>
        /// <returns>The list of items</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(typeof(List<Item>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetItemList()
        {
            return Ok(_itemService.GetItemList());
        }

        /// <summary>
        /// Add a list of new item
        /// </summary>
        /// <returns>The newly added items</returns>
        /// <response code="200">Everything worked well</response>
        [ProducesResponseType(typeof(List<Item>), StatusCodes.Status200OK)]
        [HttpPost]
        public IActionResult AddNewItems(List<Item> items)
        {
            var addedItems = new List<Item>();
                
            foreach (var item in items)
            {
                try
                {
                    var addedItem = _itemService.AddItem(item);
                    addedItems.Add(addedItem);
                }
                catch (EntityConflictException exception)
                {
                    _logger.LogError(exception.Message);
                }
            }
            return Ok(addedItems);
        }

        /// <summary>
        /// Get a specific item
        /// </summary>
        /// <param name="key">The key of the item to get</param>
        /// <returns>The requested item</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Item not found</response>
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpGet("{key}")]
        public IActionResult GetItem(string key)
        {
            try
            {
                return Ok(_itemService.GetItem(key));
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }

        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="key">The key of the item to update</param>
        /// <param name="item">The item to update</param>
        /// <returns>The updated item</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Item not found</response>
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpPatch("{key}")]
        public IActionResult UpdateItem(string key, Item item)
        {
            try
            {
                return Ok(_itemService.UpdateItem(key, item));
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }
        
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="key">The key of the item to delete</param>
        /// <returns>Nothing</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Item not found</response>
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpDelete("{key}")]
        public IActionResult DeleteItem(string key)
        {
            try
            {
                _itemService.DeleteItem(key);
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