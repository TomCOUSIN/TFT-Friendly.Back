using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Database;
using TFT_Friendly.Back.Models.Items;
using TFT_Friendly.Back.Services.Mongo;

namespace TFT_Friendly.Back.Services.Items
{
    /// <summary>
    /// ItemService class
    /// </summary>
    public class ItemService : IItemService
    {
        #region MEMBERS

        private readonly EntityContext<Item> _items;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="ItemService"/> class
        /// </summary>
        /// <param name="configuration">The database configuration to use</param>
        public ItemService(IOptions<DatabaseConfiguration> configuration)
        {
            _items = new EntityContext<Item>(Currentdb.Items, configuration);
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
        /// <exception cref="ItemNotFoundException">Throw an exception if item doesn't exist</exception>
        public Item GetItem(string key)
        {
            if (!_items.IsEntityExists(key))
                throw new ItemNotFoundException("Item not found");
            return _items.GetEntity(key);
        }

        /// <summary>
        /// Add a new item
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <returns>The newly added item</returns>
        /// <exception cref="ItemConflictException">Throw an exception if an item with the same id already exist</exception>
        public Item AddItem(Item item)
        {
            if (_items.IsEntityExists(item.Key))
                throw new ItemConflictException("An item with this ItemId already exist");
            return _items.AddEntity(item);
        }

        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="item">The item to update</param>
        /// <returns>The updated item</returns>
        /// <exception cref="ItemNotFoundException">Throw an exception if the item doesn't exist</exception>
        public Item UpdateItem(Item item)
        {
            if (!_items.IsEntityExists(item.Key))
                throw new ItemNotFoundException("Item not found");
            return _items.UpdateEntity(item.Key, item);
        }
        
        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="key">The key of the item to update</param>
        /// <param name="item">The item to update</param>
        /// <returns>The updated item</returns>
        /// <exception cref="ItemNotFoundException">Throw an exception if the item doesn't exist</exception>
        public Item UpdateItem(string key, Item item)
        {
            if (!_items.IsEntityExists(key))
                throw new ItemNotFoundException("Item not found");
            return _items.UpdateEntity(key, item);
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="key">The key of item to delete</param>
        /// <exception cref="ItemNotFoundException">Throw an exception if the item doesn't exist</exception>
        public void DeleteItem(string key)
        {
            if (!_items.IsEntityExists(key))
                throw new ItemNotFoundException("Item not found");
            _items.DeleteEntity(key);
        }

        #endregion METHODS
    }
}