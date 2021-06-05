using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TFT_Friendly.Back.Models.Configurations;

namespace TFT_Friendly.Back.Services.Mongo
{
    /// <summary>
    /// MongoContext class
    /// </summary>
    public class MongoContext
    {
        #region MEMBERS

        protected readonly DatabaseConfiguration _configuration;
        private readonly IMongoClient _client;
        protected readonly IMongoDatabase _database;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="MongoContext"/> class
        /// </summary>
        /// <param name="configuration">The configuration to use</param>
        /// <exception cref="ArgumentNullException">Throw an exception if one argument is null</exception>
        public MongoContext(IOptions<DatabaseConfiguration> configuration)
        {
            _configuration = configuration.Value ?? throw new ArgumentNullException(nameof(configuration));
            _client = new MongoClient(_configuration.ConnectionString);
            _database = _client.GetDatabase(_configuration.DatabaseName);
        }

        #endregion CONSTRUCTOR
    }
}