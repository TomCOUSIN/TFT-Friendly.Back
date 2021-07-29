using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Database;
using TFT_Friendly.Back.Models.Items;
using TFT_Friendly.Back.Services.Mongo;
using TFT_Friendly.Back.Services.Updates;

namespace TFT_Friendly.Back.Services.Items
{
    /// <summary>
    /// ItemService class
    /// </summary>
    public class ItemService : IItemService
    {
        #region MEMBERS

        private readonly EntityContext<Item> _items;
        private readonly IUpdateService _updateService;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="ItemService"/> class
        /// </summary>
        /// <param name="configuration">The database configuration to use</param>
        /// <param name="updateService">The update service to use</param>
        public ItemService(IOptions<DatabaseConfiguration> configuration, IUpdateService updateService)
        {
            _items = new EntityContext<Item>(CurrentDb.Items, configuration);
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Get a list of items
        /// </summary>
        /// <returns>The list of items</returns>
        public List<Item> GetItemList()
        {
            return _items.GetEntities();
        }

        /// <summary>
        /// Get a specific item
        /// </summary>
        /// <param name="key">The key of the item</param>
        /// <returns>The requested item</returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if item doesn't exist</exception>
        public Item GetItem(string key)
        {
            if (!_items.IsEntityExists(key))
                throw new EntityNotFoundException("Item not found");
            return _items.GetEntity(key);
        }

        /// <summary>
        /// Add a new item
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <returns>The newly added item</returns>
        /// <exception cref="EntityConflictException">Throw an exception if an item with the same id already exist</exception>
        public Item AddItem(Item item)
        {
            if (_items.IsEntityExists(item.Key))
                throw new EntityConflictException("An item with this ItemId already exist");
            _items.AddEntity(item);
            _updateService.RegisterItem(item);
            return item;
        }

        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="item">The item to update</param>
        /// <returns>The updated item</returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if the item doesn't exist</exception>
        public Item UpdateItem(Item item)
        {
            if (!_items.IsEntityExists(item.Key))
                throw new EntityNotFoundException("Item not found");
            _items.UpdateEntity(item.Key, item);
            _updateService.UpdateItem(item);
            return item;
        }
        
        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="key">The key of the item to update</param>
        /// <param name="item">The item to update</param>
        /// <returns>The updated item</returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if the item doesn't exist</exception>
        public Item UpdateItem(string key, Item item)
        {
            if (!_items.IsEntityExists(key))
                throw new EntityNotFoundException("Item not found");
            _items.UpdateEntity(key, item);
            _updateService.UpdateItem(item);
            return item;
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="key">The key of item to delete</param>
        /// <exception cref="EntityNotFoundException">Throw an exception if the item doesn't exist</exception>
        public void DeleteItem(string key)
        {
            if (!_items.IsEntityExists(key))
                throw new EntityNotFoundException("Item not found");
            var item = _items.GetEntity(key);
            _items.DeleteEntity(key);
            _updateService.DeleteItem(item);
        }

        #endregion METHODS
    }
}