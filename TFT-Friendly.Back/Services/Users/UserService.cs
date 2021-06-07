using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="UserService"/> class
        /// </summary>
        /// <param name="configuration">The configuration to use</param>
        /// <param name="usersContext">The context to use</param>
        /// <exception cref="ArgumentNullException">Throw an exception if on parameter is null</exception>
        public UserService(IOptions<JwtConfiguration> configuration, UsersContext usersContext)
        {
            _configuration = configuration.Value ?? throw new ArgumentNullException(nameof(configuration));
            _usersContext = usersContext ?? throw new ArgumentNullException(nameof(usersContext));
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
            var userIn = _usersContext.IsUserValid(user);

            if (userIn == null) return string.Empty;
            
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
            _usersContext.ReplaceOneById(id, user);
            return user;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="user">The user to register</param>
        /// <returns>The token for the new user</returns>
        public string RegisterUser(User user)
        {
            _usersContext.InsertOne(user);
            return AuthenticateUser(user);
        }

        #endregion METHODS
        
    }
}