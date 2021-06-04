using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TFT_Friendly.Back
{
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        #region MAIN

        /// <summary>
        /// Main function of the program
        /// </summary>
        /// <param name="args">The arguments to use</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        #endregion MAIN

        #region CREATE_HOST_BUILDER

        /// <summary>
        /// Create a Web Host Builder
        /// </summary>
        /// <param name="args">The arguments to use</param>
        /// <returns>The newly created Web Host</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        #endregion CREATE_HOST_BUILDER
    }
}
