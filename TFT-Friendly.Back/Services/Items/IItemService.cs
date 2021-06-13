using System.Collections.Generic;
using TFT_Friendly.Back.Models.Items;

namespace TFT_Friendly.Back.Services.Items
{
    /// <summary>
    /// IItemService interface
    /// </summary>
    public interface IItemService
    {
        List<Item> GetItemList();
        
        Item GetItem(int itemId);

        Item AddItem(Item item);
        
        Item UpdateItem(Item item);
        
        Item UpdateItem(int itemId, Item item);

        void DeleteItem(int itemId);
    }
}