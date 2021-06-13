using Newtonsoft.Json;

namespace TFT_Friendly.Back.Models.Users
{
    /// <summary>
    /// LeagueOfLegendsUser class
    /// </summary>
    public class TftUser
    {
        /// <summary>
        /// Id of League of legends user
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        
        /// <summary>
        /// AccountId of League of legends user
        /// </summary>
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        
        /// <summary>
        /// Puuid of League of legends user
        /// </summary>
        [JsonProperty("puuid")]
        public string Puuid { get; set; }
        
        /// <summary>
        /// Name of League of legends user
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Level of League of legends user
        /// </summary>
        [JsonProperty("summonerLevel")]
        public int SummonerLevel { get; set; }
    }
}