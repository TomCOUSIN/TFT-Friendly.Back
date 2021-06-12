using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TFT_Friendly.Back.Clients;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Models.Users;
using TFT_Friendly.Back.Services.Mongo;

namespace TFT_Friendly.Back.Services.Users
{
    /// <summary>
    /// UserService class
    /// </summary>
    public class UserService : IUserService
    {
        #region MEMBERS

        private readonly JwtConfiguration _configuration;
        private readonly UsersContext _usersContext;
        private readonly LeagueOfLegendsClient _client;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="UserService"/> class
        /// </summary>
        /// <param name="configuration">The configuration to use</param>
        /// <param name="usersContext">The context to use</param>
        /// <param name="client">The client to retrieve user information from League Of Legends API</param>
        /// <exception cref="ArgumentNullException">Throw an exception if on parameter is null</exception>
        public UserService(IOptions<JwtConfiguration> configuration, UsersContext usersContext, LeagueOfLegendsClient client)
        {
            _configuration = configuration.Value ?? throw new ArgumentNullException(nameof(configuration));
            _usersContext = usersContext ?? throw new ArgumentNullException(nameof(usersContext));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="user">The user to authenticate</param>
        /// <returns>The token corresponding to the right user</returns>
        public string AuthenticateUser(User user)
        {
            var userExist = _usersContext.IsUserExist(user.Username);

            if (!userExist) throw new UserNotFoundException("Username doesn't exit.");

            var userIn = _usersContext.FindOneByUsernameAndPassword(user.Username, user.Password);

            if (userIn == null) throw new InvalidPasswordException("Incorrect password for this user.");
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userIn.Id) }),
                Expires = DateTime.UtcNow.AddDays(_configuration.ExpirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Get the user information
        /// </summary>
        /// <returns></returns>
        public User GetMe(string id)
        {
            return _usersContext.FindOneById(id);
        }

        /// <summary>
        /// Patch the user information
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <param name="user">The new user information</param>
        /// <returns>The patched user</returns>
        public User PatchMe(string id, User user)
        {
            VerifyPassword(user.Password);
            _usersContext.ReplaceOneById(id, user);
            return user;
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">The id of the user</param>
        public void DeleteMe(string id)
        {
            _usersContext.DeleteOneById(id);
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="user">The user to register</param>
        /// <returns>The token for the new user</returns>
        public async Task<string> RegisterUser(User user)
        {
            if (_usersContext.IsUserExist(user.Username))
            {
                throw new UserConflictException("User with this username already exist.");
            }
            VerifyPassword(user.Password);
            
            var userInformation = await _client.GetUserInformation(user.Username).ConfigureAwait(false);
            user.LeagueId = userInformation.Puuid;
            user.SummonerLevel = userInformation.SummonerLevel;

            _usersContext.InsertOne(user);
            return AuthenticateUser(user);
        }

        /// <summary>
        /// Verify if the password of the user is valid or not
        /// </summary>
        /// <param name="password">The password to verify</param>
        private void VerifyPassword(string password)
        {
            if (password.Length < 8)
            {
                throw new PasswordFormatException("The password should contains at least 8 characters minimum.");
            }

            if (!password.Any(char.IsDigit))
            {
                throw new PasswordFormatException("The password should contains at least 1 number.");
            }

            if (!(password.Any(ch => !char.IsLetterOrDigit(ch))))
            {
                throw new PasswordFormatException("The password should contains at least 1 special character.");
            }
        }

        #endregion METHODS
        
    }
}