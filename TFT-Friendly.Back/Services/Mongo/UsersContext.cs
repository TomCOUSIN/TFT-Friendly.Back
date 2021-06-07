using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
        /// Verify if the user given as parameter is valid
        /// </summary>
        /// <param name="user">The user to verify</param>
        /// <returns>The user if valid, null otherwise</returns>
        public User IsUserValid(User user)
        {
            var filter =
                Builders<User>.Filter.Where(u => u.Username == user.Username && u.Password == user.Password);

            var userIn = _users.Find(filter);
            return userIn.FirstOrDefault();
        }
        
        #region FIND
        
        /// <summary>
        /// Find all the users
        /// </summary>
        /// <returns>The list of users</returns>
        public async Task<List<User>> FindAsync()
        {
            return await _users.Find(user => true).ToListAsync();
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
            _users.ReplaceOneAsync(user => user.Id == userIn.Id, userIn);
        }
        
        /// <summary>
        /// Replace a user according to is id
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <param name="userIn">The user to replace</param>
        public void ReplaceOneById(string id, User userIn)
        {
            userIn.Id = id;
            _users.ReplaceOneAsync(user => user.Id == id, userIn);
        }

        #endregion REPLACE

        #region DELETE

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="userIn">The user to delete</param>
        public async void DeleteOneAsync(User userIn)
        {
            await _users.DeleteOneAsync(user => user.Id == userIn.Id);
        }

        /// <summary>
        /// Delete a user according to is id
        /// </summary>
        /// <param name="id">The id of the user to delete</param>
        public async void DeleteOneByIdAsync(string id)
        {
            await _users.DeleteOneAsync(user => user.Id == id);
        }

        #endregion DELETE
        
        #endregion METHODS
    }
}