using System;
using Microsoft.Extensions.Options;

namespace TFT_Friendly.Back.Models.Configurations
{
    /// <summary>
    /// RiotAPIConfiguration class
    /// </summary>
    public class RiotApiConfiguration : IOptions<RiotApiConfiguration>, IConfigureOptions<RiotApiConfiguration>
    {
        public string Token { get; set; }

        public RiotApiConfiguration Value => this;

        public void Configure(RiotApiConfiguration options)
        {
            Token = options.Token ?? throw new ArgumentNullException(nameof(options));
        }
    }
}