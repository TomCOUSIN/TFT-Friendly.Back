using MongoDB.Bson.Serialization.Attributes;

namespace TFT_Friendly.Back.Models.Entities
{
    /// <summary>
    /// Entity class
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// The key of the entity
        /// </summary>
        [BsonElement("key")]
        public string Key { get; set; }
    }
}