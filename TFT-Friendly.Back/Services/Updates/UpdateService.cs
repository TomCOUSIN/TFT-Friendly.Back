using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Champions;
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

        #region CHAMPIONS

        /// <summary>
        /// Register a new update associated with a champion creation
        /// </summary>
        /// <param name="champion">The champion to register</param>
        /// <returns>The identifier of the new update</returns>
        public long RegisterChampion(Champion champion)
        {
            var currentUpdates = _context.GetEntities();

            var updates = new List<string>
            {
                $"CREATE;CHAMPION;{champion.Key}",
                $"SET;CHAMPION;{champion.Key};Name;{champion.Name};",
                $"SET;CHAMPION;{champion.Key};Cost;{champion.Cost};",
                $"SET;CHAMPION;{champion.Key};Armor;{champion.Armor};",
                $"SET;CHAMPION;{champion.Key};MagicResist;{champion.MagicResist};",
                $"SET;CHAMPION;{champion.Key};Speed;{champion.Speed};",
                $"SET;CHAMPION;{champion.Key};Range;{champion.Range};",
                $"SET;CHAMPION;{champion.Key};ManaMax;{champion.ManaMax};",
                $"SET;CHAMPION;{champion.Key};AbilityKey;{champion.AbilityKey};",
            };
            updates.AddRange(champion.Traits.Select(trait => $"APPEND;CHAMPION;{champion.Key};Traits;{trait}"));
            updates.AddRange(champion.Origins.Select(origin => $"APPEND;CHAMPION;{champion.Key};Origins;{origin}"));
            updates.AddRange(champion.Health.Select(health => $"APPEND;CHAMPION;{champion.Key};Health;{health}"));
            updates.AddRange(champion.Damage.Select(damage => $"APPEND;CHAMPION;{champion.Key};Damage;{damage}"));
            updates.AddRange(champion.Dps.Select(dps => $"APPEND;CHAMPION;{champion.Key};Dps;{dps}"));

            var update = new Update
            {
                Updates = updates,
                Identifier = currentUpdates.Count > 0 ? currentUpdates.Last().Identifier + 1 : 0,
                Key = currentUpdates.Count > 0 ? (currentUpdates.Last().Identifier + 1).ToString() : "0"
            };
            _context.AddEntity(update);
            return update.Identifier;
        }

        /// <summary>
        /// Register a new update associated with a champion update
        /// </summary>
        /// <param name="champion">The updated champion</param>
        /// <returns>The identifier of the new update</returns>
        public long UpdateChampion(Champion champion)
        {
            var currentUpdates = _context.GetEntities();
            
            var updates = new List<string>
            {
                $"UPDATE;CHAMPION;{champion.Key};Name;{champion.Name};",
                $"UPDATE;CHAMPION;{champion.Key};Cost;{champion.Cost};",
                $"UPDATE;CHAMPION;{champion.Key};Armor;{champion.Armor};",
                $"UPDATE;CHAMPION;{champion.Key};MagicResist;{champion.MagicResist};",
                $"UPDATE;CHAMPION;{champion.Key};Speed;{champion.Speed};",
                $"UPDATE;CHAMPION;{champion.Key};Range;{champion.Range};",
                $"UPDATE;CHAMPION;{champion.Key};ManaMax;{champion.ManaMax};",
                $"UPDATE;CHAMPION;{champion.Key};AbilityKey;{champion.AbilityKey};",
                $"REMOVE;CHAMPION;{champion.Key};Traits",
                $"REMOVE;CHAMPION;{champion.Key};Origins",
                $"REMOVE;CHAMPION;{champion.Key};Health",
                $"REMOVE;CHAMPION;{champion.Key};Damage",
                $"REMOVE;CHAMPION;{champion.Key};Dps",
            };
            updates.AddRange(champion.Traits.Select(trait => $"APPEND;CHAMPION;{champion.Key};Traits;{trait}"));
            updates.AddRange(champion.Origins.Select(origin => $"APPEND;CHAMPION;{champion.Key};Origins;{origin}"));
            updates.AddRange(champion.Health.Select(health => $"APPEND;CHAMPION;{champion.Key};Health;{health}"));
            updates.AddRange(champion.Damage.Select(damage => $"APPEND;CHAMPION;{champion.Key};Damage;{damage}"));
            updates.AddRange(champion.Dps.Select(dps => $"APPEND;CHAMPION;{champion.Key};Dps;{dps}"));
            
            var update = new Update
            {
                Updates = updates,
                Identifier = currentUpdates.Count > 0 ? currentUpdates.Last().Identifier + 1 : 0,
                Key = currentUpdates.Count > 0 ? (currentUpdates.Last().Identifier + 1).ToString() : "0"
            };
            _context.AddEntity(update);
            return update.Identifier;
        }

        /// <summary>
        /// Register a new update associated with a champion deletion
        /// </summary>
        /// <param name="champion">The champion to delete</param>
        /// <returns>The identifier of the new update</returns>
        public long DeleteChampion(Champion champion)
        {
            var currentUpdates = _context.GetEntities();
            
            var update = new Update
            {
                Updates = new List<string>
                {
                    $"DELETE;CHAMPION;{champion.Key}"
                },
                Identifier = currentUpdates.Count > 0 ? currentUpdates.Last().Identifier + 1 : 0,
                Key = currentUpdates.Count > 0 ? (currentUpdates.Last().Identifier + 1).ToString() : "0"
            };
            _context.AddEntity(update);
            return update.Identifier;
        }

        #endregion CHAMPIONS

        /// <summary>
        /// Register a new update
        /// </summary>
        /// <param name="updates">The updates to register</param>
        /// <returns>The new update's identifier</returns>
        public long RegisterUpdate(List<string> updates)
        {
            var currentUpdates = _context.GetEntities();
            if (currentUpdates.Count > 0)
            {
                return _context.AddEntity(new Update
                {
                    Updates = updates,
                    Identifier = currentUpdates.Count > 0 ? currentUpdates.Last().Identifier + 1 : 0,
                    Key = currentUpdates.Count > 0 ? (currentUpdates.Last().Identifier + 1).ToString() : "0"
                }).Identifier;
            }
            return _context.AddEntity(new Update
            {
                Updates = updates,
                Identifier = 0,
                Key = "0"
            }).Identifier;
        }

        /// <summary>
        /// Get the last update's identifier
        /// </summary>
        /// <returns>The lest update's identifier</returns>
        public long GetLastUpdateIdentifier()
        {
            var updates = _context.GetEntities();
            return updates.Count > 0 ? updates.Last().Identifier : 0;
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
            
            while (updates[index].Identifier <= from)
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

        /// <summary>
        /// Get a specific update by this identifier
        /// </summary>
        /// <param name="identifier">The identifier of the update</param>
        /// <returns>The requested update</returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if the update don't exists</exception>
        public Update GetUpdateByIdentifier(long identifier)
        {
            if (!_context.IsEntityExists(identifier.ToString()))
                throw new EntityNotFoundException("This Updates doesn't exists.");
            return _context.GetEntity(identifier.ToString());
        }

        /// <summary>
        /// Delete an update
        /// </summary>
        /// <param name="identifier">The identifier of the update to delete</param>
        /// <exception cref="EntityNotFoundException">Throw an exception if the update doesn't exists</exception>
        public void DeleteUpdate(long identifier)
        {
            if (!_context.IsEntityExists(identifier.ToString()))
                throw new EntityNotFoundException("This Updates doesn't exists.");
            _context.DeleteEntity(identifier.ToString());
        }

        #endregion METHODS
    }
}