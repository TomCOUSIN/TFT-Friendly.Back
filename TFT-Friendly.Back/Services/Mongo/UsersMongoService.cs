using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class UsersMongoService
    {
        #region MEMBERS

        private readonly DatabaseConfiguration _configuration;
        private readonly IMongoCollection<User> _users;
        
        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="UsersMongoService"/> class
        /// </summary>
        /// <param name="configuration">The configuration to use</param>
        /// <exception cref="ArgumentNullException">Throw an exception if one of the parameter is null</exception>
        public UsersMongoService(IOptions<DatabaseConfiguration> configuration)
        {
            _configuration = configuration.Value ?? throw new ArgumentNullException(nameof(configuration));
            var client = new MongoClient(_configuration.ConnectionString);
            var database = client.GetDatabase(_configuration.DatabaseName);
            _users = database.GetCollection<User>(_configuration.UsersCollectionName);
        }

        #endregion CONSTRUCTOR
        
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
        public async Task<User> FindOneByIdAsync(string id)
        {
            var user = await _users.FindAsync(u => u.Id == id);
            return await user.FirstOrDefaultAsync();
        }
        
        #endregion FIND
        
        #region INSERT
        
        /// <summary>
        /// Insert a new user in the database
        /// </summary>
        /// <param name="user">The user to add</param>
        /// <returns>The newly inserted user</returns>
        public async Task<User> InsertOneAsync(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }
        
        #endregion INSERT

        #region REPLACE

        /// <summary>
        /// Replace a user
        /// </summary>
        /// <param name="userIn">The user to replace</param>
        public async void ReplaceOneAsync(User userIn)
        {
            await _users.ReplaceOneAsync(user => user.Id == userIn.Id, userIn);
        }
        
        /// <summary>
        /// Replace a user according to is id
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <param name="userIn">The user to replace</param>
        public async void ReplaceOneByIdAsync(string id, User userIn)
        {
            await _users.ReplaceOneAsync(user => user.Id == id, userIn);
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
    }
}