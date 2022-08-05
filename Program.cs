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
                                        ProcessTweets(myLogger, provider, myAgent);
                                break;
                            case "/s":
                                        ProcessStats(myLogger, myAgent);
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

        public static void ProcessStats(ILog myLogger, TweetAgent myAgent)
        {

            try
            {
                    TweetStats myStats = myAgent.AgentStats;

                    myLogger.Info("---------------------------------\n");   
                    myLogger.Info("Total: " + myStats.TweetCount.ToString() + "\n");

                    if (myStats.HashTagLeaderboard.Count > 0)
                    {
                        myLogger.Info("Top Hash Tag: " + myStats.TopHashTag.ToString() + "\n");

                        int numberOfHashTagsToDisplay = Convert.ToInt32(ConfigurationManager.AppSettings["TweetPool_NumberOfHashtags"]);

                        foreach (KeyValuePair<string, int> hashTag in myStats.HashTagLeaderboard.OrderByDescending(d => d.Value))
                        {
                            numberOfHashTagsToDisplay--;
                            myLogger.Info(hashTag.Key + ": " + hashTag.Value.ToString() + "\n");

                            if (numberOfHashTagsToDisplay <= 0)
                                break;
                        }
                    }
                    else
                        myLogger.Info("No HashTags Available for Statistical Data. " + myStats.TweetCount.ToString() + " Tweets Processed. \n");

                    myLogger.Info("---------------------------------\n"); 
            }
            catch (Exception ex)
            {
                myLogger.Error("Error Processing Stats.");
            }

        }

        public static async Task ProcessTweets(ILog myLogger, ServiceProvider provider, TweetAgent myAgent)
        {
            try
            {
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Twitter_SampleAPIURL"]))
                {
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Twitter_APIKey"]))
                    {
                        if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Twitter_BearerToken"]))
                        {
                                if (!myAgent.isProcessingActive)
                                    await provider.GetRequiredService<IHttpClientServiceImplementation>().Execute(myLogger, myAgent, "GET", 30);
                                else
                                    myLogger.Warn("WARNING: Stream Processing is already active - only one active stream allowed in the current implementation.");
                        }
                        else
                        {
                            myLogger.Error("Missing Parameter [Twitter_BearerToken] within App.Config file. Please specify the Bearer Token within the configuration file.");
                        }
                    }
                    else
                    {
                         myLogger.Error("Missing Parameter [Twitter_APIKey] within App.Config file. Please specify the API Key within the configuration file.");
                    }
                }
                else
                {
                    myLogger.Error("Missing Parameter [Twitter_SampleAPIURL] within App.Config file. Please specify the URL within the configuration file.");
                }
            }
            catch (Exception e)
            {
                if (myLogger != null)
                {
                    if (e.InnerException != null)
                        myLogger.Error("Error while Processing Tweets " + e.InnerException.ToString());
                    else
                        myLogger.Error("Error while Processing Tweets " + e.Message.ToString());
                }
            }
        }
    }