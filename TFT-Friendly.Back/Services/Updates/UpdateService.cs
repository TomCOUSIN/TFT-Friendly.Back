using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Database;
using TFT_Friendly.Back.Models.Updates;
using TFT_Friendly.Back.Services.Mongo;

namespace TFT_Friendly.Back.Services.Updates
{
    /// <summary>
    /// UpdateService class
    /// </summary>
    public class UpdateService : IUpdateService
    {
        #region MEMBERS

        private readonly EntityContext<Update> _context;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new <see cref="UpdateService"/> class
        /// </summary>
        /// <param name="configuration">The database configuration to use</param>
        public UpdateService(IOptions<DatabaseConfiguration> configuration)
        {
            _context = new EntityContext<Update>(CurrentDb.Updates, configuration);
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Register a new update
        /// </summary>
        /// <param name="updates">The updates to register</param>
        /// <returns>The new update's identifier</returns>
        public long RegisterUpdate(List<string> updates)
        {
            var currentUpdates = _context.GetEntities();
            var newUpdate = _context.AddEntity(new Update
            {
                Updates = updates,
                Identifier = currentUpdates.Last().Identifier + 1
            });
            return newUpdate.Identifier;
        }

        /// <summary>
        /// Get the last update's identifier
        /// </summary>
        /// <returns>The lest update's identifier</returns>
        public long GetLastUpdateIdentifier()
        {
            var updates = _context.GetEntities();
            return updates.Last().Identifier;
        }

        /// <summary>
        /// Get the last updates according to the identifier
        /// </summary>
        /// <param name="from">The identifier to start with</param>
        /// <returns>All the last updates according to the given identifier</returns>
        public List<Update> GetLastUpdates(long from)
        {
            var updates = _context.GetEntities();
            var fromUpdates = new List<Update>();
            var index = 0;
            
            while (updates[index].Identifier != from)
            {
                ++index;
            }

            while (index < updates.Count)
            {
                fromUpdates.Add(updates[index]);
                ++index;
            }
            return fromUpdates;
        }

        #endregion METHODS
    }
}