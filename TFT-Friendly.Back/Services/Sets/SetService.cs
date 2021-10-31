using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Database;
using TFT_Friendly.Back.Models.Sets;
using TFT_Friendly.Back.Services.Mongo;
using TFT_Friendly.Back.Services.Updates;

namespace TFT_Friendly.Back.Services.Sets
{
    /// <summary>
    /// SetService class
    /// </summary>
    public class SetService : ISetService
    {
        #region MEMBERS

        private readonly EntityContext<Set> _setsContext;
        private readonly IUpdateService _updateService;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="SetService"/> class
        /// </summary>
        /// <param name="configuration">The database configuration to use</param>
        /// <param name="updateService">The update service to use</param>
        public SetService(IOptions<DatabaseConfiguration> configuration, IUpdateService updateService)
        {
            _setsContext = new EntityContext<Set>(CurrentDb.Sets, configuration);
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
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
        /// <exception cref="EntityNotFoundException">Throw an exception if set doesn't exist</exception>
        public Set GetSet(string key)
        {
            if (!_setsContext.IsEntityExists(key))
                throw new EntityNotFoundException("Set not found");
            return _setsContext.GetEntity(key);
        }

        /// <summary>
        /// Add a new set
        /// </summary>
        /// <param name="set">The set to add</param>
        /// <returns>The newly added set</returns>
        /// <exception cref="EntityConflictException">Throw an exception if a set with the same key already exist</exception>
        public Set AddSet(Set set)
        {
            if (_setsContext.IsEntityExists(set.Key))
                throw new EntityConflictException("A set with this key already exist");
            _setsContext.AddEntity(set);
            _updateService.RegisterSet(set);
            return set;
        }

        /// <summary>
        /// Update a set
        /// </summary>
        /// <param name="set">The set to update</param>
        /// <returns>The updated set</returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if the set doesn't exist</exception>
        public Set UpdateSet(Set set)
        {
            if (!_setsContext.IsEntityExists(set.Key))
                throw new EntityNotFoundException("Set not found");
            _setsContext.UpdateEntity(set.Key, set);
            _updateService.UpdateSet(set);
            return set;
        }

        /// <summary>
        /// Update a set
        /// </summary>
        /// <param name="key">The key of the set to update</param>
        /// <param name="set">The set to update</param>
        /// <returns>The updated set</returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if the set doesn't exist</exception>
        public Set UpdateSet(string key, Set set)
        {
            if (!_setsContext.IsEntityExists(key))
                throw new EntityNotFoundException("Set not found");
            _setsContext.UpdateEntity(key, set);
            _updateService.UpdateSet(set);
            return set;
        }

        /// <summary>
        /// Delete a set
        /// </summary>
        /// <param name="key">The key of set to delete</param>
        /// <exception cref="EntityNotFoundException">Throw an exception if the set doesn't exist</exception>
        public void DeleteSet(string key)
        {
            if (!_setsContext.IsEntityExists(key))
                throw new EntityNotFoundException("Set not found");
            var set = _setsContext.GetEntity(key);
            _setsContext.DeleteEntity(key);
            _updateService.DeleteSet(set);
        }

        #endregion METHODS
    }
}