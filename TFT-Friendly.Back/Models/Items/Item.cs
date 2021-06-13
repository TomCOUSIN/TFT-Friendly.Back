using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TFT_Friendly.Back.Models.Items
{
    /// <summary>
    /// Item class
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Id of the item
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        /// <summary>
        /// Specific Id of the item
        /// </summary>
        [BsonElement("ItemId")]
        public int ItemId { get; set; }
        
        /// <summary>
        /// Name of the item
        /// </summary>
        [BsonElement("Name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Description of the item
        /// </summary>
        [BsonElement("Description")]
        public string Description { get; set; }
        
        /// <summary>
        /// Boolean field to specify if the item is unique
        /// </summary>
        [BsonElement("IsUnique")]
        public bool IsUnique { get; set; }
        
        /// <summary>
        /// Boolean field to specify if the item is shadow
        /// </summary>
        [BsonElement("IsShadow")]
        public bool IsShadow { get; set; }
    }
}