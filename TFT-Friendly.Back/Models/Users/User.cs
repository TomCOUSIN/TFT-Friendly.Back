using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TFT_Friendly.Back.Utils.CipherHandler;

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
    }
}