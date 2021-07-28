using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Champions;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Database;
using TFT_Friendly.Back.Services.Mongo;
using TFT_Friendly.Back.Services.Updates;

namespace TFT_Friendly.Back.Services.Champions
{
    /// <summary>
    /// ChampionService class
    /// </summary>
    public class ChampionService : IChampionService
    {
        #region MEMBERS

        private readonly EntityContext<Champion> _championsContext;
        private readonly IUpdateService _updateService;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="ChampionService"/> class
        /// </summary>
        /// <param name="configuration">The database configuration to use</param>
        /// <param name="updateService">The update service to use</param>
        public ChampionService(IOptions<DatabaseConfiguration> configuration, IUpdateService updateService)
        {
            _championsContext = new EntityContext<Champion>(CurrentDb.Champions, configuration);
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
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
            _championsContext.AddEntity(champion);
            _updateService.RegisterChampion(champion);
            return champion;
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
            _championsContext.UpdateEntity(champion.Key, champion);
            _updateService.UpdateChampion(champion);
            return champion;
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
            _championsContext.UpdateEntity(key, champion);
            _updateService.UpdateChampion(champion);
            return champion;
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
            var champion = _championsContext.GetEntity(key);
            _championsContext.DeleteEntity(key);
            _updateService.DeleteChampion(champion);
        }

        #endregion METHODS
    }
}