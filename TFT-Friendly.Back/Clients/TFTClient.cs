using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Refit;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Users;

namespace TFT_Friendly.Back.Clients
{
    /// <summary>
    /// ILeagueOfLegendsClient interface
    /// </summary>
    public interface ITftClient
    {
        [Get("/tft/summoner/v1/summoners/by-name/{summonerName}")]
        Task<TftUser> GetUserInformation(string summonerName, [Header("X-Riot-Token")] string riotToken);
        
        [Get("/tft/league/v1/entries/by-summoner/{summonerId}")]
        Task<List<TftUserLeague>> GetUserLeagueInformation(string summonerId, [Header("X-Riot-Token")] string riotToken);
    }

    /// <summary>
    /// LeagueOfLegendsClient class
    /// </summary>
    public class TftClient
    {
        #region MEMBERS

        private readonly ITftClient _api;
        private readonly RiotApiConfiguration _configuration;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="TftClient"/> class
        /// </summary>
        public TftClient(IOptions<RiotApiConfiguration> configuration)
        {
            _configuration = configuration.Value ?? throw new ArgumentNullException(nameof(configuration));
            _api = RestService.For<ITftClient>(Constants.ClientsBaseUrlConstants.LeagueOfLegendsClientBaseUrl);
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Get the user league of legends information
        /// </summary>
        /// <returns>The information of the user</returns>
        public async Task<TftUser> GetUserInformation(string username)
        {
            return await _api.GetUserInformation(username, _configuration.Token).ConfigureAwait(false);
        }

        /// <summary>
        /// Get The user's league information
        /// </summary>
        /// <param name="summonerId">The summonerID of the user</param>
        /// <returns>The league information of the user</returns>
        public async Task<List<TftUserLeague>> GetUserLeagueInformation(string summonerId)
        {
            return await _api.GetUserLeagueInformation(summonerId, _configuration.Token).ConfigureAwait(false);
        }

        #endregion METHODS
    }
}