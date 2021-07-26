using System;
using System.Collections.Generic;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Champions;
using TFT_Friendly.Back.Models.Traits;
using TFT_Friendly.Back.Services.Mongo;

namespace TFT_Friendly.Back.Services.Champions
{
    /// <summary>
    /// ChampionService class
    /// </summary>
    public class ChampionService : IChampionService
    {
        #region MEMBERS

        private readonly ChampionsContext _championsContext;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="ChampionService"/> class
        /// </summary>
        /// <param name="championsContext">The traits context to use</param>
        /// <exception cref="ArgumentNullException">Throw an exception if one parameter is null</exception>
        public ChampionService(ChampionsContext championsContext)
        {
            _championsContext = championsContext ?? throw new ArgumentNullException(nameof(championsContext));
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Get a list of champions
        /// </summary>
        /// <returns>The list of champions</returns>
        public List<Champion> GetChampionList()
        {
            return _championsContext.GetChampions();
        }

        /// <summary>
        /// Get a specific champion
        /// </summary>
        /// <param name="key">The key of the champion</param>
        /// <returns>The requested champion</returns>
        /// <exception cref="ChampionNotFoundException">Throw an exception if champion doesn't exist</exception>
        public Champion GetChampion(string key)
        {
            if (!_championsContext.IsChampionExist(key))
                throw new ChampionNotFoundException("Champion not found");
            return _championsContext.GetChampion(key);
        }

        /// <summary>
        /// Add a new champion
        /// </summary>
        /// <param name="champion">The champion to add</param>
        /// <returns>The newly added champion</returns>
        /// <exception cref="ChampionConflictException">Throw an exception if a champion with the same key already exist</exception>
        public Champion AddChampion(Champion champion)
        {
            if (_championsContext.IsChampionExist(champion.Key))
                throw new ChampionConflictException("A champion with this key already exist");
            return _championsContext.AddChampion(champion);
        }

        /// <summary>
        /// Update a champion
        /// </summary>
        /// <param name="champion">The champion to update</param>
        /// <returns>The updated champion</returns>
        /// <exception cref="ChampionNotFoundException">Throw an exception if the champion doesn't exist</exception>
        public Champion UpdateChampion(Champion champion)
        {
            if (!_championsContext.IsChampionExist(champion.Key))
                throw new ChampionNotFoundException("Champion not found");
            return _championsContext.UpdateChampion(champion);
        }
        
        /// <summary>
        /// Update a champion
        /// </summary>
        /// <param name="key">The key of the champion to update</param>
        /// <param name="champion">The champion to update</param>
        /// <returns>The updated champion</returns>
        /// <exception cref="ChampionNotFoundException">Throw an exception if the champion doesn't exist</exception>
        public Champion UpdateChampion(string key, Champion champion)
        {
            if (!_championsContext.IsChampionExist(key))
                throw new ChampionNotFoundException("Champion not found");
            return _championsContext.UpdateChampion(key, champion);
        }

        /// <summary>
        /// Delete a champion
        /// </summary>
        /// <param name="key">The key of champion to delete</param>
        /// <exception cref="ChampionNotFoundException">Throw an exception if the champion doesn't exist</exception>
        public void DeleteChampion(string key)
        {
            if (!_championsContext.IsChampionExist(key))
                throw new ChampionNotFoundException("Champion not found");
            _championsContext.DeleteChampion(key);
        }

        #endregion METHODS
    }
}