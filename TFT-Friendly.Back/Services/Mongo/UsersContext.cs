using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Users;

namespace TFT_Friendly.Back.Services.Mongo
{
    /// <summary>
    /// UsersMongoService class
    /// </summary>
    public class UsersContext : MongoContext
    {
        #region MEMBERS
        
        private readonly IMongoCollection<User> _users;
        
        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="UsersContext"/> class
        /// </summary>
        /// <param name="configuration">The configuration to use</param>
        /// <exception cref="ArgumentNullException">Throw an exception if one of the parameter is null</exception>
        public UsersContext(IOptions<DatabaseConfiguration> configuration) : base(configuration)
        {
            _users = Database.GetCollection<User>(Configuration.UsersCollectionName);
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Verify if the username exist
        /// </summary>
        /// <param name="username">The username to verify</param>
        /// <returns>True if the username is valid, null otherwise</returns>
        public bool IsUserExist(string username)
        {
            var filter = Builders<User>.Filter.Where(u => u.Username == username);
            var userIn = _users.Find(filter);
            return userIn.FirstOrDefault() != null;
        }
        
        #region FIND
        
        /// <summary>
        /// Find all the users
        /// </summary>
        /// <returns>The list of users</returns>
        public List<User> FindAsync()
        {
            return _users.Find(user => true).ToList();
        }

        /// <summary>
        /// Find a specific user
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <param name="password">The password of the user</param>
        /// <returns>The user</returns>
        public User FindOneByUsernameAndPassword(string username, string password)
        {
            var filter = 
                Builders<User>.Filter.Where(u => u.Username == username && u.Password == password);
            var userIn = _users.Find(filter);
            return userIn.FirstOrDefault();
        }

        /// <summary>
        /// Find a specific user
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>The user</returns>
        public User FindOneById(string id)
        {
            var user = _users.Find(u => u.Id == id);
            return user.FirstOrDefault();
        }
        
        #endregion FIND
        
        #region INSERT
        
        /// <summary>
        /// Insert a new user in the database
        /// </summary>
        /// <param name="user">The user to add</param>
        /// <returns>The newly inserted user</returns>
        public User InsertOne(User user)
        {
            _users.InsertOne(user);
            return user;
        }
        
        #endregion INSERT

        #region REPLACE

        /// <summary>
        /// Replace a user
        /// </summary>
        /// <param name="userIn">The user to replace</param>
        public void ReplaceOne(User userIn)
        { 
            _users.ReplaceOne(user => user.Id == userIn.Id, userIn);
        }
        
        /// <summary>
        /// Replace a user according to is id
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <param name="userIn">The user to replace</param>
        public void ReplaceOneById(string id, User userIn)
        {
            userIn.Id = id;
            _users.ReplaceOne(user => user.Id == id, userIn);
        }

        #endregion REPLACE

        #region DELETE

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="userIn">The user to delete</param>
        public void DeleteOne(User userIn)
        {
            _users.DeleteOne(user => user.Id == userIn.Id);
        }

        /// <summary>
        /// Delete a user according to is id
        /// </summary>
        /// <param name="id">The id of the user to delete</param>
        public void DeleteOneById(string id)
        {
            _users.DeleteOne(user => user.Id == id);
        }

        #endregion DELETE
        
        #endregion METHODS
    }
}