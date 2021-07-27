using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Database;
using TFT_Friendly.Back.Models.Entities;
using TFT_Friendly.Back.Models.Items;

namespace TFT_Friendly.Back.Services.Mongo
{
    /// <summary>
    /// EntityContext class
    /// </summary>
    public class EntityContext<T> : MongoContext where T: Entity
    {
        #region MEMBERS
        
        private readonly IMongoCollection<T> _entities;
        
        #endregion MEMBERS
        
        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new <see cref="EntityContext{T}"/> class
        /// </summary>
        /// <param name="currentDb">The current database to use</param>
        /// <param name="configuration">The database configuration to use</param>
        public EntityContext(Currentdb currentDb, IOptions<DatabaseConfiguration> configuration) : base(configuration)
        {
            _entities = currentDb switch
            {
                Currentdb.Champions => Database.GetCollection<T>(Configuration.ItemsCollectionName),
                Currentdb.Items => Database.GetCollection<T>(Configuration.ItemsCollectionName),
                Currentdb.Sets => Database.GetCollection<T>(Configuration.ItemsCollectionName),
                Currentdb.Traits => Database.GetCollection<T>(Configuration.ItemsCollectionName),
                _ => throw new Exception()
            };
        }
        
        #endregion CONSTRUCTOR

        #region METHODS
        
        /// <summary>
        /// Verify if an entity exists
        /// </summary>
        /// <param name="key">The key of the entity to verify</param>
        /// <returns>True if exist, false otherwise</returns>
        public bool IsEntityExists(string key)
        {
            var entity = _entities.Find(i => i.Key == key).FirstOrDefault();
            return entity != null;
        }

        #region GET

        /// <summary>
        /// Get all the entities
        /// </summary>
        /// <returns></returns>
        public List<T> GetEntities()
        {
            return _entities.Find(entity => true).ToList();
        }

        /// <summary>
        /// Get a specific entity
        /// </summary>
        /// <param name="key">The key of the entity</param>
        /// <returns>The entity related to the key</returns>
        public T GetEntity(string key)
        {
            return _entities.Find(e => e.Key == key).FirstOrDefault();
        }

        #endregion GET

        #region ADD

        /// <summary>
        /// Add an entity
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The newly added entity</returns>
        public T AddEntity(T entity)
        {
            _entities.InsertOne(entity);
            return entity;
        }

        /// <summary>
        /// Add multiple entities
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The newly added entities</returns>
        public List<T> AddEntities(List<T> entities)
        {
            _entities.InsertMany(entities);
            return entities;
        }

        #endregion ADD


        #region UPDATE

        /// <summary>
        /// Update a specific entity
        /// </summary>
        /// <param name="key">The key of the entity to update</param>
        /// <param name="entity">The entity to update</param>
        /// <returns>The newly updated entity</returns>
        public T UpdateEntity(string key, T entity)
        {
            var filter = Builders<T>.Filter.Eq("Key", key);
            _entities.ReplaceOne(filter, entity);
            return entity;
        }

        #endregion UPDATE

        #region DELETE

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="key">The key of the entity to delete</param>
        public void DeleteEntity(string key)
        {
            _entities.DeleteOne(e => e.Key == key);
        }

        #endregion DELETE
        
        
        #endregion METHODS
    }
}