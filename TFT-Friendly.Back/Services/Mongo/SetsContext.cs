using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Items;
using TFT_Friendly.Back.Models.Sets;

namespace TFT_Friendly.Back.Services.Mongo
{
    /// <summary>
    /// SetsContext class
    /// </summary>
    public class SetsContext : MongoContext
    {
        #region MEMBERS

        private readonly IMongoCollection<Set> _sets;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="SetsContext"/> class
        /// </summary>
        /// <param name="configuration">The database configuration to use</param>
        public SetsContext(IOptions<DatabaseConfiguration> configuration) : base(configuration)
        {
            _sets = Database.GetCollection<Set>(Configuration.SetsCollectionName);
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Verify if a set exist
        /// </summary>
        /// <param name="key">The key of the set to verify</param>
        /// <returns>True if exist, false otherwise</returns>
        public bool IsSetExist(string key)
        {
            var set = _sets.Find(i => i.Key == key).FirstOrDefault();
            return set != null;
        }

        /// <summary>
        /// Get all the sets
        /// </summary>
        /// <returns></returns>
        public List<Set> GetSets()
        {
            return _sets.Find(set => true).ToList();
        }

        /// <summary>
        /// Get a specific set
        /// </summary>
        /// <param name="key">The key of the set</param>
        /// <returns>The set related to the id</returns>
        public Set GetSet(string key)
        {
            return _sets.Find(i => i.Key == key).FirstOrDefault();
        }

        /// <summary>
        /// Add a set
        /// </summary>
        /// <param name="set">The set to add</param>
        /// <returns>The newly added set</returns>
        public Set AddSet(Set set)
        {
            _sets.InsertOne(set);
            return set;
        }

        /// <summary>
        /// Add multiple sets
        /// </summary>
        /// <param name="sets">The set list to add</param>
        /// <returns>The newly added sets</returns>
        public List<Set> AddSets(List<Set> sets)
        {
            _sets.InsertMany(sets);
            return sets;
        }

        /// <summary>
        /// Update a specific set
        /// </summary>
        /// <param name="set">The set to update</param>
        /// <returns>The newly updated set</returns>
        public Set UpdateSet(Set set)
        {
            var filter = Builders<Set>.Filter.Eq("Key", set.Key);
            _sets.ReplaceOne(filter, set);
            return set;
        }
        
        /// <summary>
        /// Update a specific set
        /// </summary>
        /// <param name="key">The key of the set to update</param>
        /// <param name="set">The set to update</param>
        /// <returns>The newly updated set</returns>
        public Set UpdateSet(string key, Set set)
        {
            var filter = Builders<Set>.Filter.Eq("Key", key);
            _sets.ReplaceOne(filter, set);
            return set;
        }

        /// <summary>
        /// Delete a set
        /// </summary>
        /// <param name="key">The key of the set to delete</param>
        public void DeleteSet(string key)
        {
            _sets.DeleteOne(i => i.Key == key);
        }

        #endregion METHODS
    }
}