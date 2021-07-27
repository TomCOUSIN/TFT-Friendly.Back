using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Database;
using TFT_Friendly.Back.Models.Sets;
using TFT_Friendly.Back.Services.Mongo;

namespace TFT_Friendly.Back.Services.Sets
{
    /// <summary>
    /// SetService class
    /// </summary>
    public class SetService : ISetService
    {
        #region MEMBERS

        private readonly EntityContext<Set> _setsContext;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="SetService"/> class
        /// </summary>
        /// <param name="configuration">The database configuration to use</param>
        public SetService(IOptions<DatabaseConfiguration> configuration)
        {
            _setsContext = new EntityContext<Set>(Currentdb.Sets, configuration);
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Get a list of sets
        /// </summary>
        /// <returns>The list of sets</returns>
        public List<Set> GetSetList()
        {
            return _setsContext.GetEntities();
        }

        /// <summary>
        /// Get a specific set
        /// </summary>
        /// <param name="key">The key of the set</param>
        /// <returns>The requested set</returns>
        /// <exception cref="SetNotFoundException">Throw an exception if set doesn't exist</exception>
        public Set GetSet(string key)
        {
            if (!_setsContext.IsEntityExists(key))
                throw new SetNotFoundException("Set not found");
            return _setsContext.GetEntity(key);
        }

        /// <summary>
        /// Add a new set
        /// </summary>
        /// <param name="set">The set to add</param>
        /// <returns>The newly added set</returns>
        /// <exception cref="SetConflictException">Throw an exception if a set with the same key already exist</exception>
        public Set AddSet(Set set)
        {
            if (_setsContext.IsEntityExists(set.Key))
                throw new SetConflictException("A set with this key already exist");
            return _setsContext.AddEntity(set);
        }

        /// <summary>
        /// Update a set
        /// </summary>
        /// <param name="set">The set to update</param>
        /// <returns>The updated set</returns>
        /// <exception cref="SetNotFoundException">Throw an exception if the set doesn't exist</exception>
        public Set UpdateSet(Set set)
        {
            if (!_setsContext.IsEntityExists(set.Key))
                throw new SetNotFoundException("Set not found");
            return _setsContext.UpdateEntity(set.Key, set);
        }

        /// <summary>
        /// Update a set
        /// </summary>
        /// <param name="key">The key of the set to update</param>
        /// <param name="set">The set to update</param>
        /// <returns>The updated set</returns>
        /// <exception cref="SetNotFoundException">Throw an exception if the set doesn't exist</exception>
        public Set UpdateSet(string key, Set set)
        {
            if (!_setsContext.IsEntityExists(key))
                throw new SetNotFoundException("Set not found");
            return _setsContext.UpdateEntity(key, set);
        }

        /// <summary>
        /// Delete a set
        /// </summary>
        /// <param name="key">The key of set to delete</param>
        /// <exception cref="SetNotFoundException">Throw an exception if the set doesn't exist</exception>
        public void DeleteSet(string key)
        {
            if (!_setsContext.IsEntityExists(key))
                throw new SetNotFoundException("Set not found");
            _setsContext.DeleteEntity(key);
        }

        #endregion METHODS
    }
}