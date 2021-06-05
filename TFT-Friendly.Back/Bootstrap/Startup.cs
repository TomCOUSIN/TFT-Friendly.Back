using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TFT_Friendly.Back.Models.Configurations;
using TFT_Friendly.Back.Services.Mongo;

namespace TFT_Friendly.Back.Bootstrap
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
            
            services.Configure<DatabaseConfiguration>(_configuration.GetSection("DatabaseSettings"));
            services.ConfigureOptions<DatabaseConfiguration>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TFT-Friendly - API V1",
                    Description = "TFT-Friendly, your best TFT companion",
                    Contact = new OpenApiContact
                    {
                        Name = "Tom COUSIN",
                        Email = "tom.cousin@epitech.eu"
                    }
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            
            services.AddSingleton<UsersContext>();
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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TFT-Friendly - API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #endregion CONFIGURE
    }
}
