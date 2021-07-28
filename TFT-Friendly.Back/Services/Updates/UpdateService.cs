using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Abilities;
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
        
        #region ABILITY
        
        /// <summary>
        /// Register a new update associated with an ability creation
        /// </summary>
        /// <param name="ability">The ability to register</param>
        /// <returns>The identifier of the new update</returns>
        public long RegisterAbility(Models.Abilities.Ability ability)
        {
            var currentUpdates = _context.GetEntities();

            var updates = new List<string>
            {
                $"CREATE;ABILITY;{ability.Key}",
                $"SET;ABILITY;{ability.Key};Name;{ability.Name};",
                $"SET;ABILITY;{ability.Key};Active;{ability.Active};",
                $"SET;ABILITY;{ability.Key};Passive;{ability.Passive};",
                $"SET;ABILITY;{ability.Key};EffectKey;{ability.EffectKey};",
            };

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
        /// Register a new update associated with an ability update
        /// </summary>
        /// <param name="ability">The updated ability</param>
        /// <returns>The identifier of the new update</returns>
        public long UpdateAbility(Models.Abilities.Ability ability)
        {
            var currentUpdates = _context.GetEntities();
            
            var updates = new List<string>
            {
                $"UPDATE;ABILITY;{ability.Key};Name;{ability.Name};",
                $"UPDATE;ABILITY;{ability.Key};Active;{ability.Active};",
                $"UPDATE;ABILITY;{ability.Key};Passive;{ability.Passive};",
                $"UPDATE;ABILITY;{ability.Key};EffectKey;{ability.EffectKey};",
            };

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
        /// Register a new update associated with an ability deletion
        /// </summary>
        /// <param name="ability">The ability to delete</param>
        /// <returns>The identifier of the new update</returns>
        public long DeleteAbility(Models.Abilities.Ability ability)
        {
            var currentUpdates = _context.GetEntities();
            
            var update = new Update
            {
                Updates = new List<string>
                {
                    $"DELETE;CHAMPION;{ability.Key}"
                },
                Identifier = currentUpdates.Count > 0 ? currentUpdates.Last().Identifier + 1 : 0,
                Key = currentUpdates.Count > 0 ? (currentUpdates.Last().Identifier + 1).ToString() : "0"
            };
            _context.AddEntity(update);
            return update.Identifier;
        }
        
        #endregion ABILITY
        
        #region ABILITY_EFFECT
        
        /// <summary>
        /// Register a new update associated with an ability effect creation
        /// </summary>
        /// <param name="effect">The ability effect to register</param>
        /// <returns>The identifier of the new update</returns>
        public long RegisterAbilityEffect(AbilityEffect effect)
        {
            var currentUpdates = _context.GetEntities();

            var updates = new List<string>
            {
                $"CREATE;ABILITYEFFECT;{effect.Key}",
                $"SET;ABILITY;{effect.Key};Name;{effect.Name};",
                $"SET;ABILITY;{effect.Key};Value;{effect.Value};",
            };

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
        /// Register a new update associated with an ability effect update
        /// </summary>
        /// <param name="effect">The updated ability effect</param>
        /// <returns>The identifier of the new update</returns>
        public long UpdateAbilityEffect(AbilityEffect effect)
        {
            var currentUpdates = _context.GetEntities();
            
            var updates = new List<string>
            {
                $"UPDATE;ABILITYEFFECT;{effect.Key};Name;{effect.Name};",
                $"UPDATE;ABILITYEFFECT;{effect.Key};Value;{effect.Value};",
            };

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
        /// Register a new update associated with an ability effect deletion
        /// </summary>
        /// <param name="effect">The ability effect to delete</param>
        /// <returns>The identifier of the new update</returns>
        public long DeleteAbilityEffect(AbilityEffect effect)
        {
            var currentUpdates = _context.GetEntities();
            
            var update = new Update
            {
                Updates = new List<string>
                {
                    $"DELETE;CHAMPION;{effect.Key}"
                },
                Identifier = currentUpdates.Count > 0 ? currentUpdates.Last().Identifier + 1 : 0,
                Key = currentUpdates.Count > 0 ? (currentUpdates.Last().Identifier + 1).ToString() : "0"
            };
            _context.AddEntity(update);
            return update.Identifier;
        }
        
        #endregion ABILITY_EFFECT

        #region UPDATES

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

        #endregion UPDATES

        #endregion METHODS
    }
}