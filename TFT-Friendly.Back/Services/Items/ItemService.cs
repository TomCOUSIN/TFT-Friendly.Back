using System;
using System.Collections.Generic;
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

        public List<Item> GetItemList()
        {
            return _items.GetItems();
        }

        public Item GetItem(int itemId)
        {
            return _items.GetItem(itemId);
        }

        public Item UpdateItem(Item item)
        {
            return _items.UpdateItem(item);
        }

        public void DeleteItem(int itemId)
        {
            _items.DeleteItem(itemId);
        }

        #endregion METHODS
    }
}