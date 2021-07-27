using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Database;
using TFT_Friendly.Back.Models.Items;
using TFT_Friendly.Back.Models.Traits;
using TFT_Friendly.Back.Services.Mongo;

namespace TFT_Friendly.Back.Services.Traits
{
    /// <summary>
    /// TraitService class
    /// </summary>
    public class TraitService : ITraitService
    {
        #region MEMBERS

        private readonly EntityContext<Trait> _traitsContext;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="TraitService"/> class
        /// </summary>
        /// <param name="configuration">The database configuration to use</param>
        public TraitService(IOptions<DatabaseConfiguration> configuration)
        {
            _traitsContext = new EntityContext<Trait>(Currentdb.Traits, configuration);
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Get a list of traits
        /// </summary>
        /// <returns>The list of items</returns>
        public List<Trait> GetTraitList()
        {
            return _traitsContext.GetEntities();
        }

        /// <summary>
        /// Get a specific trait
        /// </summary>
        /// <param name="key">The key of the trait</param>
        /// <returns>The requested trait</returns>
        /// <exception cref="TraitNotFoundException">Throw an exception if trait doesn't exist</exception>
        public Trait GetTrait(string key)
        {
            if (!_traitsContext.IsEntityExists(key))
                throw new EntityNotFoundException("Trait not found");
            return _traitsContext.GetEntity(key);
        }

        /// <summary>
        /// Add a new trait
        /// </summary>
        /// <param name="trait">The trait to add</param>
        /// <returns>The newly added trait</returns>
        /// <exception cref="TraitConflictException">Throw an exception if a trait with the same key already exist</exception>
        public Trait AddTrait(Trait trait)
        {
            if (_traitsContext.IsEntityExists(trait.Key))
                throw new EntityConflictException("A trait with this key already exist");
            return _traitsContext.AddEntity(trait);
        }

        /// <summary>
        /// Update a trait
        /// </summary>
        /// <param name="trait">The trait to update</param>
        /// <returns>The updated trait</returns>
        /// <exception cref="TraitNotFoundException">Throw an exception if the trait doesn't exist</exception>
        public Trait UpdateTrait(Trait trait)
        {
            if (!_traitsContext.IsEntityExists(trait.Key))
                throw new EntityNotFoundException("Trait not found");
            return _traitsContext.UpdateEntity(trait.Key, trait);
        }
        
        /// <summary>
        /// Update a trait
        /// </summary>
        /// <param name="key">The key of the trait to update</param>
        /// <param name="trait">The trait to update</param>
        /// <returns>The updated trait</returns>
        /// <exception cref="TraitNotFoundException">Throw an exception if the trait doesn't exist</exception>
        public Trait UpdateTrait(string key, Trait trait)
        {
            if (!_traitsContext.IsEntityExists(key))
                throw new EntityNotFoundException("Trait not found");
            return _traitsContext.UpdateEntity(key, trait);
        }

        /// <summary>
        /// Delete a trait
        /// </summary>
        /// <param name="key">The key of trait to delete</param>
        /// <exception cref="TraitNotFoundException">Throw an exception if the trait doesn't exist</exception>
        public void DeleteTrait(string key)
        {
            if (!_traitsContext.IsEntityExists(key))
                throw new EntityNotFoundException("Trait not found");
            _traitsContext.DeleteEntity(key);
        }

        #endregion METHODS
    }
}