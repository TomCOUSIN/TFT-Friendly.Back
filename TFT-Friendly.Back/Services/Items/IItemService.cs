using System.Collections.Generic;
using System.Threading.Tasks;
using TFT_Friendly.Back.Models.Items;
using TFT_Friendly.Back.Models.Users;

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