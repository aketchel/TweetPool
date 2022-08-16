namespace TweetPool;

using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TweetLibrary;
using TweetLibrary.Interfaces;

public class Program
{
        private static readonly ILog myLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IHttpClientServiceImplementation, HttpClientTwitterStreamService>();
            services.AddScoped<ITweetProcessor, TweetProcessor>();
            services.AddScoped<ITweetStatsProcessor, TweetStatsProcessor>();
            services.AddTransient<TweetConsole>();
        }

        public static void ConfigureLogging(ILoggingBuilder lb)
        {
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Log4Net_Configuration"]))
                {  
                    lb.ClearProviders();
                    var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                    XmlConfigurator.Configure(logRepository, new FileInfo(ConfigurationManager.AppSettings["Log4Net_Configuration"]));
                    lb.AddLog4Net();
                }

            lb.SetMinimumLevel(LogLevel.Trace);
        }

        public static void Main(string[] args)
        {   
            IHostBuilder myBuilder = null;
            
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Log4Net_Configuration"]))
                {  
                     myBuilder = Host
                    .CreateDefaultBuilder(args).ConfigureServices( services => { ConfigureServices(services); } )
                    .ConfigureLogging(logBuilder => { ConfigureLogging(logBuilder); })
                    .UseConsoleLifetime();
                }
                else
                {
                    myBuilder = Host
                    .CreateDefaultBuilder(args)
                    .ConfigureServices( services => { ConfigureServices(services); } )
                    .UseConsoleLifetime();
                }

            var myHost = myBuilder.Build();

            using (var myServiceScope = myHost.Services.CreateScope())
            {
                var provider = myServiceScope.ServiceProvider;

                try
                {
                    myLogger.Info("Establishing service host.");

                    var myTweetConsole = provider.GetRequiredService<TweetConsole>();
                    myTweetConsole.Run(args, provider);
                }
                catch (Exception e)
                {
                    if (myLogger != null)
                    {
                        if (e.InnerException != null)
                            myLogger.Error("Error within TweetPool Main Program: " + e.InnerException.ToString());
                        else
                            myLogger.Error("Error within TweetPool Main Program: " + e.Message.ToString());
                    }
                }
            }
        }
}