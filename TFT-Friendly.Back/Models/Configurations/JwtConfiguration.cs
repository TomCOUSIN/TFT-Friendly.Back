using System;
using Microsoft.Extensions.Options;

namespace TFT_Friendly.Back.Models.Configurations
{
    /// <summary>
    /// JwtConfiguration class
    /// </summary>
    public class JwtConfiguration : IOptions<JwtConfiguration>, IConfigureOptions<JwtConfiguration>
    {
        public string Secret { get; set; }

        public int ExpirationTime { get; set; }

        public JwtConfiguration Value => this;

        public void Configure(JwtConfiguration options)
        {
            Secret = options.Secret ?? throw new ArgumentNullException(nameof(options.Secret));
            ExpirationTime = options.ExpirationTime > 0 ? options.ExpirationTime : 
                throw new ArgumentNullException(nameof(options.ExpirationTime));
        }
    }
}