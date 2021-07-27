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
        
        public string TraitsCollectionName { get; set; }
        
        public string ChampionsCollectionName { get; set; }
        
        public string SetsCollectionName { get; set; }
        
        public string UpdatesCollectionName { get; set; }
        
        public string AbilitiesCollectionName { get; set; }
        
        public string AbilityEffectsCollectionName { get; set; }

        public string ConnectionString { get; set; }
        
        public string DatabaseName { get; set; }

        public DatabaseConfiguration Value => this;

        public void Configure(DatabaseConfiguration options)
        {
            UsersCollectionName = options.UsersCollectionName ?? throw new ArgumentNullException(nameof(options));
            ItemsCollectionName = options.ItemsCollectionName ?? throw new ArgumentNullException(nameof(options));
            TraitsCollectionName = options.TraitsCollectionName ?? throw new ArgumentNullException(nameof(options));
            ChampionsCollectionName = options.ChampionsCollectionName ?? throw new ArgumentNullException(nameof(options));
            SetsCollectionName = options.SetsCollectionName ?? throw new ArgumentNullException(nameof(options));
            ConnectionString = options.ConnectionString ?? throw new ArgumentNullException(nameof(options));
            DatabaseName = options.DatabaseName ?? throw new ArgumentNullException(nameof(options));
        }
    }
}