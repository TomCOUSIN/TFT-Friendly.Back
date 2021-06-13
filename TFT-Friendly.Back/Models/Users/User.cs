using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TFT_Friendly.Back.Models.Users
{
    /// <summary>
    /// User class
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id of the user
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Name of the user
        /// </summary>
        [BsonElement("Username")]
        public string Username { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        [BsonElement("Password")]
        public string Password { get; set; }

        /// <summary>
        /// SummonerLevel of the user
        /// </summary>
        [BsonElement("SummonerLevel")]
        public int SummonerLevel { get; set; }
        
        /// <summary>
        /// SummonerId of the user
        /// </summary>
        [BsonElement("SummonerId")]
        public string SummonerId { get; set; }
        
        /// <summary>
        /// Unique id of the user
        /// </summary>
        [BsonElement("UniqueId")]
        public string UniqueId { get; set; }
        
        /// <summary>
        /// League tier of the user
        /// </summary>
        [BsonElement("LeagueTier")]
        public string LeagueTier { get; set; }
        
        /// <summary>
        /// League rank of the user
        /// </summary>
        [BsonElement("LeagueRank")]
        public string LeagueRank { get; set; }
    }
}