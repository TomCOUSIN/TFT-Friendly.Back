using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using TFT_Friendly.Back.Models.Entities;

namespace TFT_Friendly.Back.Models.Abilities
{
    /// <summary>
    /// AbilityEffect class
    /// </summary>
    public class AbilityEffect : Entity
    {
        /// <summary>
        /// Name of the ability effect
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Value of the ability effect
        /// </summary>
        [BsonElement("value")]
        public List<int> Value { get; set; }
    }
}