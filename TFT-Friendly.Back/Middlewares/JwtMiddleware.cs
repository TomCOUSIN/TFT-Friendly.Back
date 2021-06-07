using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Services.Users;

namespace TFT_Friendly.Back.Middlewares
{
    /// <summary>
    /// JwtMiddleware class
    /// </summary>
    public class JwtMiddleware
    {
        #region MEMBERS

        private readonly RequestDelegate _next;
        private readonly JwtConfiguration _configuration;

        #endregion MEMBERS

        #region CONSTRUCTOR

        public JwtMiddleware(RequestDelegate next, IOptions<JwtConfiguration> configuration)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _configuration = configuration.Value ?? throw new ArgumentNullException(nameof(configuration));
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Invoke when requested
        /// </summary>
        /// <param name="context">The context of the request</param>
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                AttachUserIdToContext(context, token);
            }

            await _next(context);
        }
        
        /// <summary>
        /// Get UserId from token and attach it to the request context
        /// </summary>
        /// <param name="context">The request context</param>
        /// <param name="token">The token</param>
        private void AttachUserIdToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.Secret);
                
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
                
                context.Items["UserId"] = userId;
            }
            catch
            {
                // Do nothing if jwt validation fails
                // UserId is not attached to context so request won't have access to secure routes
            }
        }

        #endregion METHODS
    }
}