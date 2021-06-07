using Microsoft.AspNetCore.Http;
using TFT_Friendly.Back.Models.Users;

namespace TFT_Friendly.Back.Services.Users
{
    /// <summary>
    /// IUserService interface
    /// </summary>
    public interface IUserService
    {
        string AuthenticateUser(User user);
        User GetMe(string id);

        string RegisterUser(User user);
    }
}