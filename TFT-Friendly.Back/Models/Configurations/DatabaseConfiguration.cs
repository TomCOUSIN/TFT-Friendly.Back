using System;
using Microsoft.Extensions.Options;

namespace TFT_Friendly.Back.Models.Configurations
{
    /// <summary>
    /// DatabaseConfiguration class
    /// </summary>
    public class DatabaseConfiguration : IOptions<DatabaseConfiguration>, IConfigureOptions<DatabaseConfiguration>
    {
        public string UsersCollectionName { get; set; }
        
        public string ItemsCollectionName { get; set; }
        
        public string ConnectionString { get; set; }
        
        public string DatabaseName { get; set; }

        public DatabaseConfiguration Value => this;

        public void Configure(DatabaseConfiguration options)
        {
            UsersCollectionName = options.UsersCollectionName ?? throw new ArgumentNullException(nameof(options));
            ItemsCollectionName = options.ItemsCollectionName ?? throw new ArgumentNullException(nameof(options));
            ConnectionString = options.ConnectionString ?? throw new ArgumentNullException(nameof(options));
            DatabaseName = options.DatabaseName ?? throw new ArgumentNullException(nameof(options));
        }
    }
}