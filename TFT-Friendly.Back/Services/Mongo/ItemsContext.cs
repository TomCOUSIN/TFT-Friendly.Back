using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Items;

namespace TFT_Friendly.Back.Services.Mongo
{
    /// <summary>
    /// ItemsContext class
    /// </summary>
    public class ItemsContext : MongoContext
    {
        #region MEMBERS

        private readonly IMongoCollection<Item> _items;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="ItemsContext"/> class
        /// </summary>
        /// <param name="configuration">The database configuration to use</param>
        public ItemsContext(IOptions<DatabaseConfiguration> configuration) : base(configuration)
        {
            _items = Database.GetCollection<Item>(Configuration.ItemsCollectionName);
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Verify if an item exist
        /// </summary>
        /// <param name="itemId">The id of the item to verify</param>
        /// <returns>True if exist, false otherwise</returns>
        public bool IsItemExist(int itemId)
        {
            var item = _items.Find(i => i.ItemId == itemId).FirstOrDefault();
            return item != null;
        }

        /// <summary>
        /// Get all the items
        /// </summary>
        /// <returns></returns>
        public List<Item> GetItems()
        {
            return _items.Find(user => true).ToList();
        }

        /// <summary>
        /// Get a specific item
        /// </summary>
        /// <param name="itemId">The id of the item</param>
        /// <returns>The item related to the id</returns>
        public Item GetItem(int itemId)
        {
            return _items.Find(i => i.ItemId == itemId).FirstOrDefault();
        }

        /// <summary>
        /// Add an item
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <returns>The newly added item</returns>
        public Item AddItem(Item item)
        {
            _items.InsertOne(item);
            return item;
        }

        /// <summary>
        /// Add multiple items
        /// </summary>
        /// <param name="items">The item list to add</param>
        /// <returns>The newly added items</returns>
        public List<Item> AddItems(List<Item> items)
        {
            _items.InsertMany(items);
            return items;
        }

        /// <summary>
        /// Update a specific item
        /// </summary>
        /// <param name="item">The item to update</param>
        /// <returns>The newly updated item</returns>
        public Item UpdateItem(Item item)
        {
            var filter = Builders<Item>.Filter.Eq("ItemId", item.ItemId);
            _items.ReplaceOne(filter, item);
            return item;
        }
        
        /// <summary>
        /// Update a specific item
        /// </summary>
        /// <param name="itemId">The id of the item to update</param>
        /// <param name="item">The item to update</param>
        /// <returns>The newly updated item</returns>
        public Item UpdateItem(int itemId, Item item)
        {
            var filter = Builders<Item>.Filter.Eq("ItemId", itemId);
            _items.ReplaceOne(filter, item);
            return item;
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="itemId">The id of the item to delete</param>
        public void DeleteItem(int itemId)
        {
            _items.DeleteOne(i => i.ItemId == itemId);
        }

        #endregion METHODS
    }
}