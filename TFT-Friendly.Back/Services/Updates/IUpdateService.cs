using System.Collections.Generic;
using TFT_Friendly.Back.Models.Champions;
using TFT_Friendly.Back.Models.Updates;

namespace TFT_Friendly.Back.Services.Updates
{
    /// <summary>
    /// IUpdateService interface
    /// </summary>
    public interface IUpdateService
    {
        public long RegisterUpdate(List<string> updates);

        public long RegisterChampion(Champion champion);

        public long UpdateChampion(Champion champion);
        
        public long DeleteChampion(Champion champion);
        
        public long GetLastUpdateIdentifier();

        public List<Update> GetLastUpdates(long from);

        public Update GetUpdateByIdentifier(long identifier);

        public void DeleteUpdate(long identifier);
    }
}