using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TapClientAngular
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static void Main(string[] args)
        {
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).ToString())
			.AddJsonFile("appsettings.json");

			Configuration = builder.Build();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
				   .ConfigureAppConfiguration((builderContext, config) =>
				   {
					   IHostingEnvironment env = builderContext.HostingEnvironment;

					   config.AddJsonFile(Directory.GetParent(Directory.GetCurrentDirectory()).ToString() + "/appsettings.json", optional: false, reloadOnChange: true)
						   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
				   })
                   .UseUrls(Configuration.GetSection("AppSettings")["ClientURL"])
                .Build();
    }
}
