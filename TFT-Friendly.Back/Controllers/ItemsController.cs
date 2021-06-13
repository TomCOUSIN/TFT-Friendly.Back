using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="ItemsController"/> class
        /// </summary>
        /// <param name="itemService">The item service to use</param>
        /// <exception cref="ArgumentNullException">Throw an exception if on parameter is null</exception>
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
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
        /// Add a new item
        /// </summary>
        /// <returns>The newly added item</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="409">An item with this ItemId already exist</response>
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Item), StatusCodes.Status409Conflict)]
        [HttpPost]
        public IActionResult AddNewItem(Item item)
        {
            try
            {
                return Ok(_itemService.AddItem(item));
            }
            catch (ItemConflictException exception)
            {
                return Conflict(new HttpError(StatusCodes.Status409Conflict, exception.Message));
            }
        }

        /// <summary>
        /// Get a specific item
        /// </summary>
        /// <returns>The requested item</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Item not found</response>
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpGet("{itemId:int}")]
        public IActionResult GetItem(int itemId)
        {
            try
            {
                return Ok(_itemService.GetItem(itemId));
            }
            catch (ItemNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }

        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="itemId">The id of the item to update</param>
        /// <param name="item">The item to update</param>
        /// <returns>The updated item</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Item not found</response>
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpPatch("{itemId:int}")]
        public IActionResult UpdateItem(int itemId, Item item)
        {
            try
            {
                return Ok(_itemService.UpdateItem(itemId, item));
            }
            catch (ItemNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }
        
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="itemId">The if of the item to delete</param>
        /// <returns>Nothing</returns>
        /// <response code="200">Everything worked well</response>
        /// <response code="404">Item not found</response>
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status404NotFound)]
        [HttpDelete("{itemId:int}")]
        public IActionResult DeleteItem(int itemId)
        {
            try
            {
                _itemService.DeleteItem(itemId);
                return Ok();
            }
            catch (ItemNotFoundException exception)
            {
                return NotFound(new HttpError(StatusCodes.Status404NotFound, exception.Message));
            }
        }
        
        #endregion ROUTES
    }
}