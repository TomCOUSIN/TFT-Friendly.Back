using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using TFT_Friendly.Back.Models.Entities;

namespace TFT_Friendly.Back.Models.Traits
{
    /// <summary>
    /// Trait class
    /// </summary>
    public class Trait : Entity
    {
        /// <summary>
        /// Name of the Trait
        /// </summary>
        [BsonElement("Name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Description of the Trait
        /// </summary>
        [BsonElement("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Type of the Trait
        /// </summary>
        [BsonElement("Type")]
        public string Type { get; set; }
        
        /// <summary>
        /// Levels of the Trait
        /// </summary>
        [BsonElement("Levels")]
        public List<TraitLevel> Levels { get; set; }
    }
}