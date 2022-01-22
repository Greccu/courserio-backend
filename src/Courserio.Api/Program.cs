using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace Courserio.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Info("Application starting...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "The application failed :( ");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((context, config) =>
                    {
                        config.AddEnvironmentVariables();
                        if (context.HostingEnvironment.IsProduction())
                        {
                            //IConfigurationRoot partialConfig = config.Build(); // build partial config
                            //string keyVaultName = partialConfig["KeyVaultName"]; // read value from configuration
                            //var secretClient = new SecretClient(
                            //    new Uri($"https://{keyVaultName}.vault.azure.net/"),
                            //    new DefaultAzureCredential());
                            //config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager()); // add an extra configuration source
                            //                                                                    // The framework calls config.Build() AGAIN to build the final IConfigurationRoot
                        }
                    });
                    webBuilder.UseStartup<Startup>()
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                    })
                    .UseNLog();
                    
                });

    }
}
