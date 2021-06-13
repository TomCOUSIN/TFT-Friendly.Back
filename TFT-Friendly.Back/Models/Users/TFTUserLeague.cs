using Newtonsoft.Json;

namespace TFT_Friendly.Back.Models.Users
{
    /// <summary>
    /// TFTUserLeague class
    /// </summary>
    public class TftUserLeague
    {
        /// <summary>
        /// Tier of the user league
        /// </summary>
        [JsonProperty("tier")]
        public string Tier { get; set; }
        
        /// <summary>
        /// Rank if the user league
        /// </summary>
        [JsonProperty("rank")]
        public string Rank { get; set; }
    }
}