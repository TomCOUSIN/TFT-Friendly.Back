using System;
using System.Collections.Generic;
using TFT_Friendly.Back.Exceptions;
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

        private readonly TraitsContext _traitsContext;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="TraitService"/> class
        /// </summary>
        /// <param name="traitsContext">The traits context to use</param>
        /// <exception cref="ArgumentNullException">Throw an exception if one parameter is null</exception>
        public TraitService(TraitsContext traitsContext)
        {
            _traitsContext = traitsContext ?? throw new ArgumentNullException(nameof(traitsContext));
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Get a list of traits
        /// </summary>
        /// <returns>The list of items</returns>
        public List<Trait> GetTraitList()
        {
            return _traitsContext.GetTraits();
        }

        /// <summary>
        /// Get a specific trait
        /// </summary>
        /// <param name="key">The key of the trait</param>
        /// <returns>The requested trait</returns>
        /// <exception cref="TraitNotFoundException">Throw an exception if trait doesn't exist</exception>
        public Trait GetTrait(string key)
        {
            if (!_traitsContext.IsTraitExist(key))
                throw new TraitNotFoundException("Trait not found");
            return _traitsContext.GetTrait(key);
        }

        /// <summary>
        /// Add a new trait
        /// </summary>
        /// <param name="trait">The trait to add</param>
        /// <returns>The newly added trait</returns>
        /// <exception cref="TraitConflictException">Throw an exception if a trait with the same key already exist</exception>
        public Trait AddTrait(Trait trait)
        {
            if (_traitsContext.IsTraitExist(trait.Key))
                throw new TraitConflictException("A trait with this key already exist");
            return _traitsContext.AddTrait(trait);
        }

        /// <summary>
        /// Update a trait
        /// </summary>
        /// <param name="trait">The trait to update</param>
        /// <returns>The updated trait</returns>
        /// <exception cref="TraitNotFoundException">Throw an exception if the trait doesn't exist</exception>
        public Trait UpdateTrait(Trait trait)
        {
            if (!_traitsContext.IsTraitExist(trait.Key))
                throw new TraitNotFoundException("Trait not found");
            return _traitsContext.UpdateTrait(trait);
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
            if (!_traitsContext.IsTraitExist(key))
                throw new TraitNotFoundException("Trait not found");
            return _traitsContext.UpdateTrait(key, trait);
        }

        /// <summary>
        /// Delete a trait
        /// </summary>
        /// <param name="key">The key of trait to delete</param>
        /// <exception cref="TraitNotFoundException">Throw an exception if the trait doesn't exist</exception>
        public void DeleteTrait(string key)
        {
            if (!_traitsContext.IsTraitExist(key))
                throw new TraitNotFoundException("Trait not found");
            _traitsContext.DeleteTrait(key);
        }

        #endregion METHODS
    }
}