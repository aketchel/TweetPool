namespace TweetPool;

using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TweetLibrary;
using TweetLibrary.Interfaces;

public class Program
{
        private static readonly ILog myLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IHttpClientServiceImplementation, HttpClientTwitterStreamService>();
        }

        public static void Main(string[] args)
        {   
            var services = new ServiceCollection();
            ConfigureServices(services);

             ServiceProvider provider = services.BuildServiceProvider();

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Log4Net_Configuration"]))
            {  
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo(ConfigurationManager.AppSettings["Log4Net_Configuration"]));
            }

            try
            {
                myLogger.Info("Starting TweetPool console application.");

                MainMenu(myLogger, provider, args);
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

        public static void MainMenu(ILog myLogger, ServiceProvider provider, string[] appArgs)
        {
            TweetProcessor mainProcessor = new TweetProcessor();
            TweetStatsProcessor statsProcessor = new TweetStatsProcessor();
            TweetStats mainStats = new TweetStats();
            TweetAgent myAgent = new TweetAgent(mainStats);
            
            string commandEntry = "";
            string mainArgument = "";

                if (appArgs.Length > 0)
                {
                    mainArgument = appArgs[0];
                }

                do
                {
                    switch (mainArgument)
                    {
                            case "/p":
                                        mainProcessor.ProcessTweets(myLogger, provider, myAgent);
                                break;
                            case "/s":
                                        statsProcessor.ProcessStats(myLogger, myAgent);
                                break;
                            default:
                                Console.WriteLine("-------------------------\n");
                                Console.WriteLine("TweetPool Console Client.\n");
                                Console.WriteLine("-------------------------\n");
                                Console.WriteLine("Argument ------------ \tDescription\n");
                                Console.WriteLine("/p\t\t\tProcess the Twitter Stream.");
                                Console.WriteLine("/s\t\t\tReview the Twitter Stream Statistics.");
                                Console.WriteLine("-------------------------\n");
                                break;
                    }

                Console.WriteLine("\n You may either [1] Press enter for help, [2] Enter an Argument to Proceed, or [3] Type 'stop' to end  . . .");
                commandEntry = Console.ReadLine();

                    if (commandEntry.Contains("/"))
                        mainArgument = commandEntry;
                    else
                        mainArgument = "";

            } while (!commandEntry.ToLower().Equals("stop"));
        }
}