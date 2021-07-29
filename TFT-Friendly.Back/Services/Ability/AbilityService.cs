using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Abilities;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Database;
using TFT_Friendly.Back.Services.Mongo;
using TFT_Friendly.Back.Services.Updates;

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
        private readonly IUpdateService _updateService;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new <see cref="AbilityService"/> class
        /// </summary>
        /// <param name="configuration">The database configuration to use</param>
        /// <param name="updateService">The update service to use</param>
        public AbilityService(IOptions<DatabaseConfiguration> configuration, IUpdateService updateService)
        {
            _abilityContext = new EntityContext<Models.Abilities.Ability>(CurrentDb.Ability, configuration);
            _abilityEffectContext = new EntityContext<AbilityEffect>(CurrentDb.AbilityEffect, configuration);
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
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
            _abilityContext.AddEntity(ability);
            _updateService.RegisterAbility(ability);
            return ability;
        }

        /// <summary>
        /// Update an ability
        /// </summary>
        /// <param name="key">The ability's key to update</param>
        /// /// <param name="ability">The ability to update</param>
        /// <returns>The updated ability</returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if the ability doesn't exists</exception>
        public Models.Abilities.Ability UpdateAbility(string key, Models.Abilities.Ability ability)
        {
            if (!_abilityContext.IsEntityExists(key))
            {
                throw new EntityNotFoundException("This Ability doesn't exists");
            }
            _abilityContext.UpdateEntity(key, ability);
            _updateService.UpdateAbility(ability);
            return ability;
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
            var ability = _abilityContext.GetEntity(key);
            _abilityContext.DeleteEntity(key);
            _updateService.DeleteAbility(ability);
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
            _abilityEffectContext.AddEntity(effect);
            _updateService.RegisterAbilityEffect(effect);
            return effect;
        }

        /// <summary>
        /// Update an ability effect
        /// </summary>
        /// <param name="key">The ability effect's key to update</param>
        /// <param name="effect">The ability effect to update</param>
        /// <returns>The updated ability effect </returns>
        /// <exception cref="EntityNotFoundException">Throw an exception if the ability effect doesn't exists</exception>
        public AbilityEffect UpdateAbilityEffect(string key, AbilityEffect effect)
        {
            if (!_abilityEffectContext.IsEntityExists(key))
            {
                throw new EntityNotFoundException("This AbilityEffect doesn't exists");
            }
            _abilityEffectContext.UpdateEntity(key, effect);
            _updateService.UpdateAbilityEffect(effect);
            return effect;
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

            var effect = _abilityEffectContext.GetEntity(key);
            _abilityEffectContext.DeleteEntity(key);
            _updateService.DeleteAbilityEffect(effect);
        }

        #endregion ABILITY_EFFECT
        
        #endregion METHODS
    }
}