using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using TFT_Friendly.Back.Models.Entities;

namespace TFT_Friendly.Back.Models.Abilities
{
    /// <summary>
    /// Ability class
    /// </summary>
    public class Ability : Entity
    {
        /// <summary>
        /// Name of the ability
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Passive of the ability
        /// </summary>
        [BsonElement("passive")]
        public string Passive { get; set; }
        
        /// <summary>
        /// Active of the ability
        /// </summary>
        [BsonElement("active")]
        public string Active { get; set; }
        
        /// <summary>
        /// Effect of the ability
        /// </summary>
        [BsonElement("effect")]
        public List<AbilityEffect> Effect { get; set; }
    }
}