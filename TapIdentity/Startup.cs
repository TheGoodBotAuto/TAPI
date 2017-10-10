using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TapIdentity.Data;
using TapIdentity.Models;
using TapIdentity.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TapIdentity
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

            string connectionString = Configuration.GetConnectionString("UserDB");
            services.AddDbContext<ApplicationDbContext>(options =>
                                                        options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddIdentityServer();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
			services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
				.AddInMemoryPersistedGrants()
				.AddInMemoryIdentityResources(Config.GetIdentityResources())
				.AddInMemoryApiResources(Config.GetApiResources())
				.AddInMemoryClients(Config.GetClients(appSettings))
				.AddAspNetIdentity<ApplicationUser>();
			services.AddAuthentication()
			   .AddJwtBearer(jwt =>
			   {
				   jwt.Authority = appSettings["IdentityURL"];
				   jwt.RequireHttpsMetadata = false;
				   jwt.Audience = "api1";
			   });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

			app.UseIdentityServer();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
