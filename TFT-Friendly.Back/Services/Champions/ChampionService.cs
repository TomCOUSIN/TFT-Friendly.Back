using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Champions;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Database;
using TFT_Friendly.Back.Services.Mongo;

namespace TFT_Friendly.Back.Services.Champions
{
    /// <summary>
    /// ChampionService class
    /// </summary>
    public class ChampionService : IChampionService
    {
        #region MEMBERS

        private readonly EntityContext<Champion> _championsContext;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="ChampionService"/> class
        /// </summary>
        /// <param name="configuration">The database configuration to use</param>
        public ChampionService(IOptions<DatabaseConfiguration> configuration)
        {
            _championsContext = new EntityContext<Champion>(Currentdb.Champions, configuration);
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Get a list of champions
        /// </summary>
        /// <returns>The list of champions</returns>
        public List<Champion> GetChampionList()
        {
            return _championsContext.GetEntities();
        }

        /// <summary>
        /// Get a specific champion
        /// </summary>
        /// <param name="key">The key of the champion</param>
        /// <returns>The requested champion</returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if champion doesn't exist</exception>
        public Champion GetChampion(string key)
        {
            if (!_championsContext.IsEntityExists(key))
                throw new EntityNotFoundException("Champion not found");
            return _championsContext.GetEntity(key);
        }

        /// <summary>
        /// Add a new champion
        /// </summary>
        /// <param name="champion">The champion to add</param>
        /// <returns>The newly added champion</returns>
        /// <exception cref="EntityConflictException">Throw an exception if a champion with the same key already exist</exception>
        public Champion AddChampion(Champion champion)
        {
            if (_championsContext.IsEntityExists(champion.Key))
                throw new EntityConflictException("A champion with this key already exist");
            return _championsContext.AddEntity(champion);
        }

        /// <summary>
        /// Update a champion
        /// </summary>
        /// <param name="champion">The champion to update</param>
        /// <returns>The updated champion</returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if the champion doesn't exist</exception>
        public Champion UpdateChampion(Champion champion)
        {
            if (!_championsContext.IsEntityExists(champion.Key))
                throw new EntityNotFoundException("Champion not found");
            return _championsContext.UpdateEntity(champion.Key, champion);
        }
        
        /// <summary>
        /// Update a champion
        /// </summary>
        /// <param name="key">The key of the champion to update</param>
        /// <param name="champion">The champion to update</param>
        /// <returns>The updated champion</returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if the champion doesn't exist</exception>
        public Champion UpdateChampion(string key, Champion champion)
        {
            if (!_championsContext.IsEntityExists(key))
                throw new EntityNotFoundException("Champion not found");
            return _championsContext.UpdateEntity(key, champion);
        }

        /// <summary>
        /// Delete a champion
        /// </summary>
        /// <param name="key">The key of champion to delete</param>
        /// <exception cref="EntityNotFoundException">Throw an exception if the champion doesn't exist</exception>
        public void DeleteChampion(string key)
        {
            if (!_championsContext.IsEntityExists(key))
                throw new EntityNotFoundException("Champion not found");
            _championsContext.DeleteEntity(key);
        }

        #endregion METHODS
    }
}