using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TFT_Friendly.Back
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        #region MEMBERS

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="Startup"/> class
        /// </summary>
        /// <param name="environment">The environment to use</param>
        /// <param name="configuration">The configuration to use</param>
        /// <exception cref="ArgumentNullException">
        /// Throw an <see cref="ArgumentNullException"/> if one of the parameter is null
        /// </exception>
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        #endregion CONSTRUCTOR

        #region CONFIGURE_SERVICES

        /// <summary>
        /// Configures services
        /// </summary>
        /// <param name="services">The services to configure</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        #endregion CONFIGURE_SERVICES

        #region CONFIGURE

        /// <summary>
        /// Configure the app
        /// </summary>
        /// <param name="app">The app to configure</param>
        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #endregion CONFIGURE
    }
}
