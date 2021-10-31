using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using TFT_Friendly.Back.Models.Entities;

namespace TFT_Friendly.Back.Models.Sets
{
    /// <summary>
    /// Set class
    /// </summary>
    public class Set : Entity
    {
        /// <summary>
        /// Set's name
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Set's Champions
        /// </summary>
        [BsonElement("champions")]
        public List<string> ChampionsKey { get; set; }
        
        /// <summary>
        /// Set's Items
        /// </summary>
        [BsonElement("items")]
        public List<string> ItemsKey { get; set; }
        
        /// <summary>
        /// Set's traits
        /// </summary>
        [BsonElement("traits")]
        public List<string> TraitsKey { get; set; }
        
        /// <summary>
        /// Set's Origins
        /// </summary>
        [BsonElement("origins")]
        public List<string> OriginsKey { get; set; }
        
        /// <summary>
        /// Boolean to check if it's the current set
        /// </summary>
        [BsonElement("is_current_set")]
        public bool IsCurrentSet { get; set; }

        /// <summary>
        /// Start Date of the Set
        /// </summary>
        [BsonElement("start_date")]
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// End Date of the Set
        /// </summary>
        [BsonElement("end_date")]
        public DateTime EndDate { get; set; }
    }
}