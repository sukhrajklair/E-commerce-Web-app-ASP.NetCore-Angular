using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DutchTreat
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = BuildWebHost(args);

      RunSeeding(host);

      host.Run();
    }

    private static void RunSeeding(IWebHost host)
    {
      var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
      using (var scope = scopeFactory.CreateScope())
      {
        var seeder = host.Services.GetService<DutchSeeder>();
        seeder.Seed();
      }
      
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(SetupConfiguration)
            .UseStartup<Startup>()
            .Build();

    private static void SetupConfiguration(WebHostBuilderContext context, IConfigurationBuilder builder)
    {
            //remove default configuration option
            builder.Sources.Clear();

            //configuration is loaded from multiple sources
            //the hierarchy is in the same order as the source is added
            //i.e variable from Environment will override the one from xml file which will override the one from Json file
            builder.AddJsonFile("config.json", false, true)
                    .AddXmlFile("config.xml", true)
                    .AddEnvironmentVariables();
    }
  }
}
