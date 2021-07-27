using System.Collections.Generic;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Abilities;

namespace TFT_Friendly.Back.Services.Ability
{
    /// <summary>
    /// IAbilityService interface
    /// </summary>
    public interface IAbilityService
    {
        public List<Models.Abilities.Ability> GetAllAbilities();

        public Models.Abilities.Ability GetAbility(string key);

        public Models.Abilities.Ability AddAbility(Models.Abilities.Ability ability);

        public Models.Abilities.Ability UpdateAbility(Models.Abilities.Ability ability);

        public void DeleteAbility(string key);

        public List<AbilityEffect> GetAllAbilityEffects();

        public AbilityEffect GetAbilityEffect(string key);

        public AbilityEffect AddAbilityEffect(AbilityEffect effect);

        public AbilityEffect UpdateAbilityEffect(AbilityEffect effect);

        public void DeleteAbilityEffect(string key);
    }
}