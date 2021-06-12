using System.Threading.Tasks;
using Refit;
using TFT_Friendly.Back.Models.Users;

namespace TFT_Friendly.Back.Clients
{
    /// <summary>
    /// ILeagueOfLegendsClient interface
    /// </summary>
    public interface ILeagueOfLegendsClient
    {
        [Get("/tft/summoner/v1/summoners/by-name/{summonerName}")]
        [Headers("X-Riot-Token: RGAPI-82d1ddd8-4ea4-460e-9715-ef81e756b1e5")]
        Task<LeagueOfLegendsUser> GetUserInformation(string summonerName);
    }

    /// <summary>
    /// LeagueOfLegendsClient class
    /// </summary>
    public class LeagueOfLegendsClient
    {
        #region MEMBERS

        private readonly ILeagueOfLegendsClient _api;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="LeagueOfLegendsClient"/> class
        /// </summary>
        public LeagueOfLegendsClient()
        {
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
            return await _api.GetUserInformation(username).ConfigureAwait(false);
        }

        #endregion METHODS
    }
}