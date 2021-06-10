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
        
        User PatchMe(string id, User user);

        string RegisterUser(User user);
    }
}