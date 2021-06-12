using System;
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
    public interface ILeagueOfLegendsClient
    {
        [Get("/tft/summoner/v1/summoners/by-name/{summonerName}")]
        Task<LeagueOfLegendsUser> GetUserInformation(string summonerName, [Header("X-Riot-Token")] string riotToken);
    }

    /// <summary>
    /// LeagueOfLegendsClient class
    /// </summary>
    public class LeagueOfLegendsClient
    {
        #region MEMBERS

        private readonly ILeagueOfLegendsClient _api;
        private readonly RiotApiConfiguration _configuration;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="LeagueOfLegendsClient"/> class
        /// </summary>
        public LeagueOfLegendsClient(IOptions<RiotApiConfiguration> configuration)
        {
            _configuration = configuration.Value ?? throw new ArgumentNullException(nameof(configuration));
            _api = RestService.For<ILeagueOfLegendsClient>(Constants.ClientsBaseUrlConstants.LeagueOfLegendsClientBaseUrl);
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Get the user league of legends information
        /// </summary>
        /// <returns></returns>
        public async Task<LeagueOfLegendsUser> GetUserInformation(string username)
        {
            return await _api.GetUserInformation(username, _configuration.Token).ConfigureAwait(false);
        }

        #endregion METHODS
    }
}