using MongoDB.Bson.Serialization.Attributes;

namespace TFT_Friendly.Back.Models.Traits
{
    /// <summary>
    /// TraitLevel class
    /// </summary>
    public class TraitLevel
    {
        /// <summary>
        /// TraitLevel type
        /// </summary>
        [BsonElement("Type")]
        public string Type { get; set; }
        
        /// <summary>
        /// Max Level Trait
        /// </summary>
        [BsonElement("Max")]
        public int Max { get; set; }
        
        /// <summary>
        /// Min Level Trait
        /// </summary>
        [BsonElement("Min")]
        public int Min { get; set; }
    }
}