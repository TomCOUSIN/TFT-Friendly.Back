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
        /// <param name="itemId">The id of the item</param>
        /// <returns>The requested item</returns>
        /// <exception cref="ItemNotFoundException">Throw an exception if item doesn't exist</exception>
        public Item GetItem(int itemId)
        {
            if (!_items.IsItemExist(itemId))
                throw new ItemNotFoundException("Item not found");
            return _items.GetItem(itemId);
        }

        /// <summary>
        /// Add a new item
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <returns>The newly added item</returns>
        /// <exception cref="ItemConflictException">Throw an exception if an item with the same id already exist</exception>
        public Item AddItem(Item item)
        {
            if (_items.IsItemExist(item.ItemId))
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
            if (!_items.IsItemExist(item.ItemId))
                throw new ItemNotFoundException("Item not found");
            return _items.UpdateItem(item);
        }
        
        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="itemId">The id of the item to update</param>
        /// <param name="item">The item to update</param>
        /// <returns>The updated item</returns>
        /// <exception cref="ItemNotFoundException">Throw an exception if the item doesn't exist</exception>
        public Item UpdateItem(int itemId, Item item)
        {
            if (!_items.IsItemExist(itemId))
                throw new ItemNotFoundException("Item not found");
            return _items.UpdateItem(itemId, item);
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="itemId">The id of item to delete</param>
        /// <exception cref="ItemNotFoundException">Throw an exception if the item doesn't exist</exception>
        public void DeleteItem(int itemId)
        {
            if (!_items.IsItemExist(itemId))
                throw new ItemNotFoundException("Item not found");
            _items.DeleteItem(itemId);
        }

        #endregion METHODS
    }
}