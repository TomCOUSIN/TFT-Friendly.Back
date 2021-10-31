using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Database;
using TFT_Friendly.Back.Models.Items;
using TFT_Friendly.Back.Models.Traits;
using TFT_Friendly.Back.Services.Mongo;
using TFT_Friendly.Back.Services.Updates;

namespace TFT_Friendly.Back.Services.Traits
{
    /// <summary>
    /// TraitService class
    /// </summary>
    public class TraitService : ITraitService
    {
        #region MEMBERS

        private readonly EntityContext<Trait> _traitsContext;
        private readonly IUpdateService _updateService;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="TraitService"/> class
        /// </summary>
        /// <param name="configuration">The database configuration to use</param>
        /// <param name="updateService">The update service to use</param>
        public TraitService(IOptions<DatabaseConfiguration> configuration, IUpdateService updateService)
        {
            _traitsContext = new EntityContext<Trait>(CurrentDb.Traits, configuration);
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
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
        /// <exception cref="EntityNotFoundException">Throw an exception if trait doesn't exist</exception>
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
        /// <exception cref="EntityConflictException">Throw an exception if a trait with the same key already exist</exception>
        public Trait AddTrait(Trait trait)
        {
            if (_traitsContext.IsEntityExists(trait.Key))
                throw new EntityConflictException("A trait with this key already exist");
            _traitsContext.AddEntity(trait);
            _updateService.RegisterTrait(trait);
            return trait;
        }

        /// <summary>
        /// Update a trait
        /// </summary>
        /// <param name="trait">The trait to update</param>
        /// <returns>The updated trait</returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if the trait doesn't exist</exception>
        public Trait UpdateTrait(Trait trait)
        {
            if (!_traitsContext.IsEntityExists(trait.Key))
                throw new EntityNotFoundException("Trait not found");
            _traitsContext.UpdateEntity(trait.Key, trait);
            _updateService.UpdateTrait(trait);
            return trait;
        }
        
        /// <summary>
        /// Update a trait
        /// </summary>
        /// <param name="key">The key of the trait to update</param>
        /// <param name="trait">The trait to update</param>
        /// <returns>The updated trait</returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if the trait doesn't exist</exception>
        public Trait UpdateTrait(string key, Trait trait)
        {
            if (!_traitsContext.IsEntityExists(key))
                throw new EntityNotFoundException("Trait not found");
            _traitsContext.UpdateEntity(key, trait);
            _updateService.UpdateTrait(trait);
            return trait;
        }

        /// <summary>
        /// Delete a trait
        /// </summary>
        /// <param name="key">The key of trait to delete</param>
        /// <exception cref="EntityNotFoundException">Throw an exception if the trait doesn't exist</exception>
        public void DeleteTrait(string key)
        {
            if (!_traitsContext.IsEntityExists(key))
                throw new EntityNotFoundException("Trait not found");
            var trait = _traitsContext.GetEntity(key);
            _traitsContext.DeleteEntity(key);
            _updateService.DeleteTrait(trait);
        }

        #endregion METHODS
    }
}