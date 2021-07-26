using System.Collections.Generic;
using TFT_Friendly.Back.Models.Champions;

namespace TFT_Friendly.Back.Services.Champions
{
    /// <summary>
    /// IChampionService interface
    /// </summary>
    public interface IChampionService
    {
        List<Champion> GetChampionList();
        
        Champion GetChampion(string key);

        Champion AddChampion(Champion champion);
        
        Champion UpdateChampion(Champion champion);
        
        Champion UpdateChampion(string key, Champion champion);

        void DeleteChampion(string key);
    }
}