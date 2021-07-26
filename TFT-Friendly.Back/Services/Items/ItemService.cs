using System;
using System.Collections.Generic;
using TFT_Friendly.Back.Exceptions;
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

        private readonly ItemsContext _items;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="ItemService"/> class
        /// </summary>
        /// <param name="context">The context of the item database</param>
        /// <exception cref="ArgumentNullException">Throw an exception if one parameter is null</exception>
        public ItemService(ItemsContext context)
        {
            _items = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Get a list of items
        /// </summary>
        /// <returns>The list of items</returns>
        public List<Item> GetItemList()
        {
            return _items.GetItems();
        }

        /// <summary>
        /// Get a specific item
        /// </summary>
        /// <param name="key">The key of the item</param>
        /// <returns>The requested item</returns>
        /// <exception cref="ItemNotFoundException">Throw an exception if item doesn't exist</exception>
        public Item GetItem(string key)
        {
            if (!_items.IsItemExist(key))
                throw new ItemNotFoundException("Item not found");
            return _items.GetItem(key);
        }

        /// <summary>
        /// Add a new item
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <returns>The newly added item</returns>
        /// <exception cref="ItemConflictException">Throw an exception if an item with the same id already exist</exception>
        public Item AddItem(Item item)
        {
            if (_items.IsItemExist(item.Key))
                throw new ItemConflictException("An item with this ItemId already exist");
            return _items.AddItem(item);
        }

        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="item">The item to update</param>
        /// <returns>The updated item</returns>
        /// <exception cref="ItemNotFoundException">Throw an exception if the item doesn't exist</exception>
        public Item UpdateItem(Item item)
        {
            if (!_items.IsItemExist(item.Key))
                throw new ItemNotFoundException("Item not found");
            return _items.UpdateItem(item);
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
            if (!_items.IsItemExist(key))
                throw new ItemNotFoundException("Item not found");
            return _items.UpdateItem(key, item);
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="key">The key of item to delete</param>
        /// <exception cref="ItemNotFoundException">Throw an exception if the item doesn't exist</exception>
        public void DeleteItem(string key)
        {
            if (!_items.IsItemExist(key))
                throw new ItemNotFoundException("Item not found");
            _items.DeleteItem(key);
        }

        #endregion METHODS
    }
}