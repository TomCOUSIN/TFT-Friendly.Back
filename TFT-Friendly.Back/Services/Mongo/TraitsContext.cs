using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Items;
using TFT_Friendly.Back.Models.Traits;

namespace TFT_Friendly.Back.Services.Mongo
{
    /// <summary>
    /// TraitsContext class
    /// </summary>
    public class TraitsContext : MongoContext
    {
        #region MEMBERS

        private readonly IMongoCollection<Trait> _traits;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="TraitsContext"/> class
        /// </summary>
        /// <param name="configuration">The database configuration to use</param>
        public TraitsContext(IOptions<DatabaseConfiguration> configuration) : base(configuration)
        {
            _traits = Database.GetCollection<Trait>(Configuration.TraitsCollectionName);
        }
        
        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Verify if a trait exist
        /// </summary>
        /// <param name="key">The key of the trait to verify</param>
        /// <returns>True if exist, false otherwise</returns>
        public bool IsTraitExist(string key)
        {
            var trait = _traits.Find(t => t.Key == key).FirstOrDefault();
            return trait != null;
        }

        /// <summary>
        /// Get all the Traits
        /// </summary>
        /// <returns></returns>
        public List<Trait> GetTraits()
        {
            return _traits.Find(trait => true).ToList();
        }

        /// <summary>
        /// Get a specific trait
        /// </summary>
        /// <param name="key">The key of the trait</param>
        /// <returns>The trait related to the key</returns>
        public Trait GetTrait(string key)
        {
            return _traits.Find(t => t.Key == key).FirstOrDefault();
        }

        /// <summary>
        /// Add a trait
        /// </summary>
        /// <param name="trait">The trait to add</param>
        /// <returns>The newly added item</returns>
        public Trait AddTrait(Trait trait)
        {
            _traits.InsertOne(trait);
            return trait;
        }

        /// <summary>
        /// Add multiple traits
        /// </summary>
        /// <param name="traits">The trait list to add</param>
        /// <returns>The newly added traits</returns>
        public List<Trait> AddTraits(List<Trait> traits)
        {
            _traits.InsertMany(traits);
            return traits;
        }

        /// <summary>
        /// Update a specific trait
        /// </summary>
        /// <param name="trait">The trait to update</param>
        /// <returns>The newly updated trait</returns>
        public Trait UpdateTrait(Trait trait)
        {
            var filter = Builders<Trait>.Filter.Eq("Key", trait.Key);
            _traits.ReplaceOne(filter, trait);
            return trait;
        }
        
        /// <summary>
        /// Update a specific trait
        /// </summary>
        /// <param name="key">The key of the trait to update</param>
        /// <param name="trait">The trait to update</param>
        /// <returns>The newly updated trait</returns>
        public Trait UpdateTrait(string key, Trait trait)
        {
            var filter = Builders<Trait>.Filter.Eq("Key", key);
            _traits.ReplaceOne(filter, trait);
            return trait;
        }

        /// <summary>
        /// Delete a trait
        /// </summary>
        /// <param name="key">The key of the trait to delete</param>
        public void DeleteTrait(string key)
        {
            _traits.DeleteOne(t => t.Key == key);
        }

        #endregion METHODS
    }
}