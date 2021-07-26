using System.Collections.Generic;
using TFT_Friendly.Back.Models.Traits;

namespace TFT_Friendly.Back.Services.Traits
{
    /// <summary>
    /// ITraitService interface
    /// </summary>
    public interface ITraitService
    {
        List<Trait> GetTraitList();
        
        Trait GetTrait(string key);

        Trait AddTrait(Trait trait);
        
        Trait UpdateTrait(Trait trait);
        
        Trait UpdateTrait(string key, Trait trait);

        void DeleteTrait(string key);
    }
}