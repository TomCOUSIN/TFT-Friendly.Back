using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using TFT_Friendly.Back.Models.Abilities;

namespace TFT_Friendly.Back.Models.Champions
{
    /// <summary>
    /// Champion class
    /// </summary>
    public class Champion
    {
        /// <summary>
        /// Name of the champion
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Key of the champion
        /// </summary>
        [BsonElement("key")]
        public string Key { get; set; }
        
        /// <summary>
        /// Cost of the champion
        /// </summary>
        [BsonElement("cost")]
        public int Cost { get; set; }
        
        /// <summary>
        /// Traits of the champion
        /// </summary>
        [BsonElement("traits")]
        public List<string> Traits { get; set; }
        
        /// <summary>
        /// Origins of the champion
        /// </summary>
        [BsonElement("origins")]
        public List<string> Origins { get; set; }

        /// <summary>
        /// Health of the champion
        /// </summary>
        [BsonElement("health")]
        public List<int> Health { get; set; }

        /// <summary>
        /// Damage of the champion
        /// </summary>
        [BsonElement("damage")]
        public List<int> Damage { get; set; }
        
        /// <summary>
        /// DPS of the champion
        /// </summary>
        [BsonElement("dps")]
        public List<int> Dps { get; set; }
        
        /// <summary>
        /// Armor of the champion
        /// </summary>
        [BsonElement("armor")]
        public int Armor { get; set; }
        
        /// <summary>
        /// Magic Resist of the champion
        /// </summary>
        [BsonElement("magic_resist")]
        public int MagicResist { get; set; }
        
        /// <summary>
        /// Speed of the champion
        /// </summary>
        [BsonElement("speed")]
        public int Speed { get; set; }
        
        /// <summary>
        /// Range of the champion
        /// </summary>
        [BsonElement("range")]
        public int Range { get; set; }
        
        /// <summary>
        /// Mana Max of the champion
        /// </summary>
        [BsonElement("mana_max")]
        public int ManaMax { get; set; }
        
        /// <summary>
        /// Ability of the champion
        /// </summary>
        [BsonElement("ability")]
        public Ability Ability { get; set; }
    }
}