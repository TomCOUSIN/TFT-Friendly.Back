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
        /// Name of the user stored in database
        /// </summary>
        [BsonElement("Name")]
        private string _username;
        
        /// <summary>
        /// Password of the user stored in database
        /// </summary>
        [BsonElement("Password")]
        private string _password;

        /// <summary>
        /// Name of the user
        /// </summary>
        [BsonIgnore]
        
        public string Username
        {
            get => CipherHandler.Decrypt(_username);
            set => _username = CipherHandler.Encrypt(value);
        }

        /// <summary>
        /// Password of the user
        /// </summary>
        [BsonIgnore]
        public string Password
        {
            get => CipherHandler.Decrypt(_password);
            set => _password = CipherHandler.Encrypt(value);
        }
    }
}