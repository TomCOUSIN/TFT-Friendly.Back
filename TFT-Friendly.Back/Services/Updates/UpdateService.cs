using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Abilities;
using TFT_Friendly.Back.Models.Champions;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Database;
using TFT_Friendly.Back.Models.Items;
using TFT_Friendly.Back.Models.Sets;
using TFT_Friendly.Back.Models.Traits;
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
                $"SET;CHAMPION;{champion.Key};BaseMana;{champion.BaseMana};",
                $"SET;CHAMPION;{champion.Key};AbilityKey;{champion.AbilityKey};",
            };
            updates.AddRange(champion.Traits.Select(trait => $"APPEND;CHAMPION;{champion.Key};Traits;{trait}"));
            updates.AddRange(champion.Origins.Select(origin => $"APPEND;CHAMPION;{champion.Key};Origins;{origin}"));
            updates.AddRange(champion.Health.Select(health => $"APPEND;CHAMPION;{champion.Key};Health;{health}"));
            updates.AddRange(champion.Damage.Select(damage => $"APPEND;CHAMPION;{champion.Key};Damage;{damage}"));
            updates.AddRange(champion.Dps.Select(dps => $"APPEND;CHAMPION;{champion.Key};Dps;{dps}"));
            return RegisterUpdate(updates);
        }

        /// <summary>
        /// Register a new update associated with a champion update
        /// </summary>
        /// <param name="champion">The updated champion</param>
        /// <returns>The identifier of the new update</returns>
        public long UpdateChampion(Champion champion)
        {
            var updates = new List<string>
            {
                $"UPDATE;CHAMPION;{champion.Key};Name;{champion.Name};",
                $"UPDATE;CHAMPION;{champion.Key};Cost;{champion.Cost};",
                $"UPDATE;CHAMPION;{champion.Key};Armor;{champion.Armor};",
                $"UPDATE;CHAMPION;{champion.Key};MagicResist;{champion.MagicResist};",
                $"UPDATE;CHAMPION;{champion.Key};Speed;{champion.Speed};",
                $"UPDATE;CHAMPION;{champion.Key};Range;{champion.Range};",
                $"UPDATE;CHAMPION;{champion.Key};ManaMax;{champion.ManaMax};",
                $"UPDATE;CHAMPION;{champion.Key};BaseMana;{champion.BaseMana};",
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
            return RegisterUpdate(updates);
        }

        /// <summary>
        /// Register a new update associated with a champion deletion
        /// </summary>
        /// <param name="champion">The champion to delete</param>
        /// <returns>The identifier of the new update</returns>
        public long DeleteChampion(Champion champion)
        {
            return RegisterUpdate(new List<string>
            {
                $"DELETE;CHAMPION;{champion.Key}"
            });
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
            var updates = new List<string>
            {
                $"CREATE;ABILITY;{ability.Key}",
                $"SET;ABILITY;{ability.Key};Name;{ability.Name}",
                $"SET;ABILITY;{ability.Key};Active;{ability.Active}",
                $"SET;ABILITY;{ability.Key};Passive;{ability.Passive}",
            };
            updates.AddRange(ability.EffectKey.Select(effect => $"APPEND;ABILITY;{ability.Key};EffectKey;{effect}"));
            return RegisterUpdate(updates);
        }

        /// <summary>
        /// Register a new update associated with an ability update
        /// </summary>
        /// <param name="ability">The updated ability</param>
        /// <returns>The identifier of the new update</returns>
        public long UpdateAbility(Models.Abilities.Ability ability)
        {
            var updates = new List<string>
            {
                $"UPDATE;ABILITY;{ability.Key};Name;{ability.Name}",
                $"UPDATE;ABILITY;{ability.Key};Active;{ability.Active}",
                $"UPDATE;ABILITY;{ability.Key};Passive;{ability.Passive}",
                $"REMOVE;ABILITY;{ability.Key};EffectKey",
            };
            updates.AddRange(ability.EffectKey.Select(effect => $"APPEND;ABILITY;{ability.Key};EffectKey;{effect}"));
            return RegisterUpdate(updates);
        }

        /// <summary>
        /// Register a new update associated with an ability deletion
        /// </summary>
        /// <param name="ability">The ability to delete</param>
        /// <returns>The identifier of the new update</returns>
        public long DeleteAbility(Models.Abilities.Ability ability)
        {
            return RegisterUpdate(new List<string>
            {
                $"DELETE;ABILITY;{ability.Key}"
            });
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
            var updates = new List<string>
            {
                $"CREATE;ABILITYEFFECT;{effect.Key}",
                $"SET;ABILITYEFFECT;{effect.Key};Name;{effect.Name};",
                $"SET;ABILITYEFFECT;{effect.Key};IsPercentage;{effect.IsPercentage};",
            };
            updates.AddRange(effect.Value.Select(value => $"APPEND;ABILITYEFFECT;{effect.Key};Value;{value}"));
            return RegisterUpdate(updates);
        }

        /// <summary>
        /// Register a new update associated with an ability effect update
        /// </summary>
        /// <param name="effect">The updated ability effect</param>
        /// <returns>The identifier of the new update</returns>
        public long UpdateAbilityEffect(AbilityEffect effect)
        {
            var updates = new List<string>
            {
                $"UPDATE;ABILITYEFFECT;{effect.Key};Name;{effect.Name};",
                $"REMOVE;ABILITYEFFECT;{effect.Key};Value",
                $"UPDATE;ABILITYEFFECT;{effect.Key};IsPercentage;{effect.IsPercentage};",
            };
            updates.AddRange(effect.Value.Select(value => $"APPEND;ABILITYEFFECT;{effect.Key};Value;{value}"));
            return RegisterUpdate(updates);
        }

        /// <summary>
        /// Register a new update associated with an ability effect deletion
        /// </summary>
        /// <param name="effect">The ability effect to delete</param>
        /// <returns>The identifier of the new update</returns>
        public long DeleteAbilityEffect(AbilityEffect effect)
        {
            return RegisterUpdate(new List<string>
            {
                $"DELETE;ABILITYEFFECT;{effect.Key}"
            });
        }
        
        #endregion ABILITY_EFFECT

        #region ITEMS

        /// <summary>
        /// Register a new update associated with an item creation
        /// </summary>
        /// <param name="item">The item to register</param>
        /// <returns>The identifier of the new update</returns>
        public long RegisterItem(Item item)
        {

            var updates = new List<string>
            {
                $"CREATE;ITEM;{item.Key}",
                $"SET;ITEM;{item.Key};Name;{item.Name};",
                $"SET;ITEM;{item.Key};ItemId;{item.ItemId};",
                $"SET;ITEM;{item.Key};Description;{item.Description};",
                $"SET;ITEM;{item.Key};IsUnique;{item.IsUnique};",
                $"SET;ITEM;{item.Key};IsRadiant;{item.IsRadiant};",
                $"SET;ITEM;{item.Key};IsShadow;{item.IsShadow};",
            };
            updates.AddRange(item.Components.Select(component => $"APPEND;ITEM;{item.Key};Components;{component}"));
            return RegisterUpdate(updates);
        }

        /// <summary>
        /// Register a new update associated with an item update
        /// </summary>
        /// <param name="item">The updated item</param>
        /// <returns>The identifier of the new update</returns>
        public long UpdateItem(Item item)
        {
            var updates = new List<string>
            {
                $"UPDATE;ITEM;{item.Key};Name;{item.Name};",
                $"UPDATE;ITEM;{item.Key};ItemId;{item.ItemId};",
                $"UPDATE;ITEM;{item.Key};Description;{item.Description};",
                $"UPDATE;ITEM;{item.Key};IsUnique;{item.IsUnique};",
                $"UPDATE;ITEM;{item.Key};IsRadiant;{item.IsRadiant};",
                $"UPDATE;ITEM;{item.Key};IsShadow;{item.IsShadow};",
                $"REMOVE;ITEM;{item.Key};Components;",
            };
            updates.AddRange(item.Components.Select(component => $"APPEND;ITEM;{item.Key};Components;{component}"));
            return RegisterUpdate(updates);
        }

        /// <summary>
        /// Register a new update associated with an item deletion
        /// </summary>
        /// <param name="item">The item to delete</param>
        /// <returns>The identifier of the new update</returns>
        public long DeleteItem(Item item)
        {
            return RegisterUpdate(new List<string>
            {
                $"DELETE;ITEM;{item.Key}"
            });
        }

        #endregion ITEMS

        #region TRAITS

        /// <summary>
        /// Register a new update associated with a trait creation
        /// </summary>
        /// <param name="trait">The trait to register</param>
        /// <returns>The identifier of the new update</returns>
        public long RegisterTrait(Trait trait)
        {

            var updates = new List<string>
            {
                $"CREATE;TRAIT;{trait.Key}",
                $"SET;TRAIT;{trait.Key};Name;{trait.Name};",
                $"SET;TRAIT;{trait.Key};Type;{trait.Type};",
                $"SET;TRAIT;{trait.Key};Description;{trait.Description};",
                $"SET;TRAIT;{trait.Key};Passive;{trait.Passive};",
            };
            updates.AddRange(trait.Levels.Select(level => $"APPEND;TRAIT;{trait.Key};Levels;{level.ToJson()}"));
            return RegisterUpdate(updates);
        }

        /// <summary> b 
        /// Register a new update associated with a trait update
        /// </summary>
        /// <param name="trait">The updated trait</param>
        /// <returns>The identifier of the new update</returns>
        public long UpdateTrait(Trait trait)
        {
            var updates = new List<string>
            {
                $"UPDATE;TRAIT;{trait.Key};Name;{trait.Name};",
                $"UPDATE;TRAIT;{trait.Key};Type;{trait.Type};",
                $"UPDATE;TRAIT;{trait.Key};Description;{trait.Description};",
                $"REMOVE;TRAIT;{trait.Key};Levels;{trait.Levels};",
                $"UPDATE;TRAIT;{trait.Key};Passive;{trait.Passive};",
            };
            updates.AddRange(trait.Levels.Select(level => $"APPEND;TRAIT;{trait.Key};Levels;{level.ToJson()}"));
            return RegisterUpdate(updates);
        }

        /// <summary>
        /// Register a new update associated with a trait deletion
        /// </summary>
        /// <param name="trait">The trait to delete</param>
        /// <returns>The identifier of the new update</returns>
        public long DeleteTrait(Trait trait)
        {
            return RegisterUpdate(new List<string>
            {
                $"DELETE;TRAIT;{trait.Key}"
            });
        }

        #endregion TRAITS

        #region SETS

        /// <summary>
        /// Register a new update associated with a set creation
        /// </summary>
        /// <param name="set">The set to register</param>
        /// <returns>The identifier of the new update</returns>
        public long RegisterSet(Set set)
        {
            var updates = new List<string>
            {
                $"CREATE;SET;{set.Key}",
                $"SET;SET;{set.Key};Name;{set.Name};",
                $"SET;SET;{set.Key};IsCurrentSet;{set.IsCurrentSet};",
                $"SET;SET;{set.Key};StartDate;{set.StartDate};",
                $"SET;SET;{set.Key};EndDate;{set.EndDate};",
            };
            updates.AddRange(set.ChampionsKey.Select(champion => $"APPEND;SET;{set.Key};ChampionsKey;{champion}"));
            updates.AddRange(set.ItemsKey.Select(item => $"APPEND;SET;{set.Key};ItemsKey;{item}"));
            updates.AddRange(set.OriginsKey.Select(origin => $"APPEND;SET;{set.Key};OriginsKey;{origin}"));
            updates.AddRange(set.TraitsKey.Select(trait => $"APPEND;SET;{set.Key};TraitsKey;{trait}"));
            return RegisterUpdate(updates);
        }

        /// <summary>
        /// Register a new update associated with a set update
        /// </summary>
        /// <param name="set">The updated set</param>
        /// <returns>The identifier of the new update</returns>
        public long UpdateSet(Set set)
        {
            var updates = new List<string>
            {
                $"UPDATE;SET;{set.Key};Name;{set.Name};",
                $"REMOVE;SET;{set.Key};ChampionsKey;",
                $"REMOVE;SET;{set.Key};ItemsKey;",
                $"REMOVE;SET;{set.Key};OriginsKey;",
                $"REMOVE;SET;{set.Key};TraitsKey;",
                $"UPDATE;SET;{set.Key};IsCurrentSet;{set.IsCurrentSet};",
                $"UPDATE;SET;{set.Key};StartDate;{set.StartDate};",
                $"UPDATE;SET;{set.Key};EndDate;{set.EndDate};",
            };
            updates.AddRange(set.ChampionsKey.Select(champion => $"APPEND;SET;{set.Key};ChampionsKey;{champion}"));
            updates.AddRange(set.ItemsKey.Select(item => $"APPEND;SET;{set.Key};ItemsKey;{item}"));
            updates.AddRange(set.OriginsKey.Select(origin => $"APPEND;SET;{set.Key};OriginsKey;{origin}"));
            updates.AddRange(set.TraitsKey.Select(trait => $"APPEND;SET;{set.Key};TraitsKey;{trait}"));
            return RegisterUpdate(updates);
        }

        /// <summary>
        /// Register a new update associated with a set deletion
        /// </summary>
        /// <param name="set">The set to delete</param>
        /// <returns>The identifier of the new update</returns>
        public long DeleteSet(Set set)
        {
            return RegisterUpdate(new List<string>
            {
                $"DELETE;SET;{set.Key}"
            });
        }

        #endregion SETS

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
                    Identifier = currentUpdates.Last().Identifier + 1,
                    Key = (currentUpdates.Last().Identifier + 1).ToString()
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
            return updates.Count > 0 ? updates.Last().Identifier : -1;
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

            while (updates[index].Identifier <= @from)
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