using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TFT_Friendly.Back.Models.Entities;

namespace TFT_Friendly.Back.Models.Items
{
    /// <summary>
    /// Item class
    /// </summary>
    public class Item : Entity
    {
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
        
        /// <summary>
        /// Boolean field to specify if the item is radiant
        /// </summary>
        [BsonElement("IsRadiant")]
        public bool IsRadiant { get; set; }
        
        /// <summary>
        /// Components of the item
        /// </summary>
        [BsonElement("Components")]
        public List<string> Components { get; set; }
    }
}