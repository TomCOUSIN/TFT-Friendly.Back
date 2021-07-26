using System.Collections.Generic;
using TFT_Friendly.Back.Models.Sets;

namespace TFT_Friendly.Back.Services.Sets
{
    /// <summary>
    /// ISetService interface
    /// </summary>
    public interface ISetService
    {
        List<Set> GetSetList();
        
        Set GetSet(string key);

        Set AddSet(Set set);
        
        Set UpdateSet(Set set);
        
        Set UpdateSet(string key, Set set);

        void DeleteSet(string key);
    }
}