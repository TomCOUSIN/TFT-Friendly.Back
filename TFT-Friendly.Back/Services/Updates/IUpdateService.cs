using System.Collections.Generic;
using TFT_Friendly.Back.Models.Updates;

namespace TFT_Friendly.Back.Services.Updates
{
    /// <summary>
    /// IUpdateService interface
    /// </summary>
    public interface IUpdateService
    {
        public long RegisterUpdate(List<string> updates);
        
        public long GetLastUpdateIdentifier();

        public List<Update> GetLastUpdates(long from);

        public Update GetUpdateByIdentifier(long identifier);
    }
}