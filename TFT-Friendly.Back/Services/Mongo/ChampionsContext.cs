using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TFT_Friendly.Back.Models.Champions;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Items;

namespace TFT_Friendly.Back.Services.Mongo
{
    /// <summary>
    /// ChampionsContext class
    /// </summary>
    public class ChampionsContext : MongoContext
    {
        #region MEMBERS

        private readonly IMongoCollection<Champion> _champions;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="ChampionsContext"/> class
        /// </summary>
        /// <param name="configuration">The database configuration to use</param>
        public ChampionsContext(IOptions<DatabaseConfiguration> configuration) : base(configuration)
        {
            _champions = Database.GetCollection<Champion>(Configuration.ChampionsCollectionName);
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Verify if a champion exist
        /// </summary>
        /// <param name="key">The key of the item to verify</param>
        /// <returns>True if exist, false otherwise</returns>
        public bool IsChampionExist(string key)
        {
            var item = _champions.Find(i => i.Key == key).FirstOrDefault();
            return item != null;
        }

        /// <summary>
        /// Get all the champions
        /// </summary>
        /// <returns></returns>
        public List<Champion> GetChampions()
        {
            return _champions.Find(user => true).ToList();
        }

        /// <summary>
        /// Get a specific champion
        /// </summary>
        /// <param name="key">The key of the champion</param>
        /// <returns>The champion related to the key</returns>
        public Champion GetChampion(string key)
        {
            return _champions.Find(i => i.Key == key).FirstOrDefault();
        }

        /// <summary>
        /// Add a champion
        /// </summary>
        /// <param name="champion">The champion to add</param>
        /// <returns>The newly added champion</returns>
        public Champion AddChampion(Champion champion)
        {
            _champions.InsertOne(champion);
            return champion;
        }

        /// <summary>
        /// Add multiple champion
        /// </summary>
        /// <param name="champions">The champion list to add</param>
        /// <returns>The newly added champions</returns>
        public List<Champion> AddChampions(List<Champion> champions)
        {
            _champions.InsertMany(champions);
            return champions;
        }

        /// <summary>
        /// Update a specific champion
        /// </summary>
        /// <param name="champion">The champion to update</param>
        /// <returns>The newly updated champion</returns>
        public Champion UpdateChampion(Champion champion)
        {
            var filter = Builders<Champion>.Filter.Eq("Key", champion.Key);
            _champions.ReplaceOne(filter, champion);
            return champion;
        }
        
        /// <summary>
        /// Update a specific champion
        /// </summary>
        /// <param name="key">The key of the champion to update</param>
        /// <param name="champion">The champion to update</param>
        /// <returns>The newly updated champion</returns>
        public Champion UpdateChampion(string key, Champion champion)
        {
            var filter = Builders<Champion>.Filter.Eq("Key", champion);
            _champions.ReplaceOne(filter, champion);
            return champion;
        }

        /// <summary>
        /// Delete a champion
        /// </summary>
        /// <param name="key">The key of the champion to delete</param>
        public void DeleteChampion(string key)
        {
            _champions.DeleteOne(i => i.Key == key);
        }

        #endregion METHODS
    }
}