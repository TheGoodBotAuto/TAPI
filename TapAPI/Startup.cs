using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.AccessTokenValidation;
using TapAPI.Models;
using TapAPI.Repositories;

namespace TapAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			var appSettings = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettings);


            IConfiguration test = Configuration;

			services.AddDbContext<TapoutDataContext>(opts =>
	            opts.UseSqlServer(Configuration.GetConnectionString("DataDB")));
            services.AddScoped<IServerRepository,ServerRepository>();
            services.AddScoped<IVulnerabilityRepository,VulnerabilityRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();

			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials());
			});

			services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
	            .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = appSettings["IdentityURL"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "api1";

                });




            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			

            app.UseCors("CorsPolicy");

			/*app.UseCors(policy =>
            {
                policy.WithOrigins(
                    "http://localhost:28895", 
                    "http://localhost:7017");

                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.WithExposedHeaders("WWW-Authenticate");
            });*/
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
