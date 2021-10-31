using System.Collections.Generic;
using TFT_Friendly.Back.Models.Abilities;
using TFT_Friendly.Back.Models.Champions;
using TFT_Friendly.Back.Models.Items;
using TFT_Friendly.Back.Models.Sets;
using TFT_Friendly.Back.Models.Traits;
using TFT_Friendly.Back.Models.Updates;

namespace TFT_Friendly.Back.Services.Updates
{
    /// <summary>
    /// IUpdateService interface
    /// </summary>
    public interface IUpdateService
    {
        public long RegisterUpdate(List<string> updates);

        public long RegisterChampion(Champion champion);

        public long UpdateChampion(Champion champion);
        
        public long DeleteChampion(Champion champion);
        
        public long RegisterAbility(Models.Abilities.Ability ability);

        public long UpdateAbility(Models.Abilities.Ability ability);
        
        public long DeleteAbility(Models.Abilities.Ability ability);
        
        public long RegisterAbilityEffect(AbilityEffect effect);

        public long UpdateAbilityEffect(AbilityEffect effect);
        
        public long DeleteAbilityEffect(AbilityEffect effect);
        
        public long RegisterItem(Item item);

        public long UpdateItem(Item item);
        
        public long DeleteItem(Item item);
        
        public long RegisterTrait(Trait trait);

        public long UpdateTrait(Trait trait);
        
        public long DeleteTrait(Trait trait);
        
        public long RegisterSet(Set set);

        public long UpdateSet(Set set);
        
        public long DeleteSet(Set set);
        
        public long GetLastUpdateIdentifier();

        public List<Update> GetLastUpdates(long from);

        public Update GetUpdateByIdentifier(long identifier);

        public void DeleteUpdate(long identifier);
    }
}