using System.Collections.Generic;
using Microsoft.Extensions.Options;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Abilities;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Database;
using TFT_Friendly.Back.Services.Mongo;

namespace TFT_Friendly.Back.Services.Ability
{
    /// <summary>
    /// AbilityService class
    /// </summary>
    public class AbilityService : IAbilityService
    {
        #region MEMBERS

        private readonly EntityContext<Models.Abilities.Ability> _abilityContext;
        private readonly EntityContext<AbilityEffect> _abilityEffectContext;

        #endregion MEMBERS

        #region CONSTRUCTOR
        
        /// <summary>
        /// Initializes a new <see cref="AbilityService"/> class
        /// </summary>
        /// <param name="configuration">The database configuration to use</param>
        public AbilityService(IOptions<DatabaseConfiguration> configuration)
        {
            _abilityContext = new EntityContext<Models.Abilities.Ability>(CurrentDb.Ability, configuration);
            _abilityEffectContext = new EntityContext<AbilityEffect>(CurrentDb.AbilityEffect, configuration);
        }
        
        #endregion CONSTRUCTOR
        
        #region METHODS

        #region ABILITY
        
        /// <summary>
        /// Get all abilities
        /// </summary>
        /// <returns>All the abilities</returns>
        public List<Models.Abilities.Ability> GetAllAbilities()
        {
            return _abilityContext.GetEntities();
        }
        
        /// <summary>
        /// Get a specific ability
        /// </summary>
        /// <param name="key">The key of the ability</param>
        /// <returns>The requested ability</returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if the ability doesn't exists</exception>
        public Models.Abilities.Ability GetAbility(string key)
        {
            if (!_abilityContext.IsEntityExists(key))
            {
                throw new EntityNotFoundException("This Ability doesn't exists");
            }
            return _abilityContext.GetEntity(key);
        }

        /// <summary>
        /// Add a new ability
        /// </summary>
        /// <param name="ability">the ability to add</param>
        /// <returns>The added ability</returns>
        /// <exception cref="EntityConflictException">Throw an exception if an ability with the same key already exists</exception>
        public Models.Abilities.Ability AddAbility(Models.Abilities.Ability ability)
        {
            if (_abilityContext.IsEntityExists(ability.Key))
            {
                throw new EntityConflictException("An ability with this key already exists");
            }
            return _abilityContext.AddEntity(ability);
        }

        /// <summary>
        /// Update an ability
        /// </summary>
        /// <param name="ability">The ability to update</param>
        /// <returns>The updated ability</returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if the ability doesn't exists</exception>
        public Models.Abilities.Ability UpdateAbility(Models.Abilities.Ability ability)
        {
            if (!_abilityContext.IsEntityExists(ability.Key))
            {
                throw new EntityNotFoundException("This Ability doesn't exists");
            }
            return _abilityContext.UpdateEntity(ability.Key, ability);
        }

        /// <summary>
        /// Delete an ability
        /// </summary>
        /// <param name="key">The key of the ability to delete</param>
        /// <exception cref="EntityNotFoundException">Throw an exception if the ability doesn't exists</exception>
        public void DeleteAbility(string key)
        {
            if (!_abilityContext.IsEntityExists(key))
            {
                throw new EntityNotFoundException("This Ability doesn't exists");
            }
            _abilityContext.DeleteEntity(key);
        }

        #endregion ABILITY

        #region ABILITY_EFFECT

        /// <summary>
        /// Get all ability effects
        /// </summary>
        /// <returns>All the ability effects</returns>
        public List<AbilityEffect> GetAllAbilityEffects()
        {
            return _abilityEffectContext.GetEntities();
        }
        
        /// <summary>
        /// Get a specific ability effect
        /// </summary>
        /// <param name="key">The key of the ability effect</param>
        /// <returns>The requested ability effect</returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if the ability effect doesn't exists</exception>
        public AbilityEffect GetAbilityEffect(string key)
        {
            if (!_abilityEffectContext.IsEntityExists(key))
            {
                throw new EntityNotFoundException("This AbilityEffect doesn't exists");
            }
            return _abilityEffectContext.GetEntity(key);
        }

        /// <summary>
        /// Add a new ability effect
        /// </summary>
        /// <param name="effect">the ability effect to add</param>
        /// <returns>The added ability effect</returns>
        /// <exception cref="EntityConflictException">Throw an exception if an ability effect with the same key already exists</exception>
        public AbilityEffect AddAbilityEffect(AbilityEffect effect)
        {
            if (_abilityEffectContext.IsEntityExists(effect.Key))
            {
                throw new EntityConflictException("An AbilityEffect with this key already exists");
            }
            return _abilityEffectContext.AddEntity(effect);
        }

        /// <summary>
        /// Update an ability effect
        /// </summary>
        /// <param name="effect">The ability effect to update</param>
        /// <returns>The updated ability effect </returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if the ability effect doesn't exists</exception>
        public AbilityEffect UpdateAbilityEffect(AbilityEffect effect)
        {
            if (!_abilityEffectContext.IsEntityExists(effect.Key))
            {
                throw new EntityNotFoundException("This AbilityEffect doesn't exists");
            }
            return _abilityEffectContext.UpdateEntity(effect.Key, effect);
        }

        /// <summary>
        /// Delete an ability effect
        /// </summary>
        /// <param name="key">The key of the ability effect to delete</param>
        /// <exception cref="EntityNotFoundException">Throw an exception if the ability effect doesn't exists</exception>
        public void DeleteAbilityEffect(string key)
        {
            if (!_abilityEffectContext.IsEntityExists(key))
            {
                throw new EntityNotFoundException("This AbilityEffect doesn't exists");
            }
            _abilityEffectContext.DeleteEntity(key);
        }

        #endregion ABILITY_EFFECT
        
        #endregion METHODS
    }
}