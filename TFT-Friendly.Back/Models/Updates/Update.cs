using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using TFT_Friendly.Back.Models.Entities;

namespace TFT_Friendly.Back.Models.Updates
{
    /// <summary>
    /// Update class
    /// </summary>
    public class Update : Entity
    {
        /// <summary>
        /// Updates
        /// </summary>
        [BsonElement("updates")]
        public List<string> Updates { get; set; }
        
        /// <summary>
        /// Update identifier
        /// </summary>
        [BsonElement("id")]
        public long Identifier { get; set; }
    }
}