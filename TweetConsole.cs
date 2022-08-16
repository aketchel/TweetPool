namespace TweetPool;

using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using TweetLibrary;
using TweetLibrary.Interfaces;

public class TweetConsole
{
        private static ILogger myLogger;

        public TweetConsole(ILogger<TweetConsole> newLogger)
        {
            myLogger = newLogger;
        }

        public void Run(string[] args, IServiceProvider provider)
        {   
            try
            {
                myLogger.LogInformation("Starting TweetPool console application.");

                MainMenu(myLogger, provider, args);
            }
            catch (Exception e)
            {
                if (myLogger != null)
                {
                    if (e.InnerException != null)
                        myLogger.LogError("Error within TweetPool Main Program: " + e.InnerException.ToString());
                    else
                        myLogger.LogError("Error within TweetPool Main Program: " + e.Message.ToString());
                }
            }
        }

        public void MainMenu(ILogger myLogger, IServiceProvider provider, string[] appArgs)
        {
            TweetStatsProcessor statsProcessor = new TweetStatsProcessor();

            List<TweetProcessor> processorPool = new List<TweetProcessor>();
            processorPool.Add(new TweetProcessor()); // Pre-seed the pool with at least one processor.

            List<TweetAgent> agentPool = new List<TweetAgent>();
            agentPool.Add(new TweetAgent()); // Pre-seed the pool with at least one agent.
            
            string commandEntry = "";
            string mainArgument = "";
            string argModifier = "";

                if (appArgs.Length > 0)
                {
                    mainArgument = appArgs[0];
                }

                do
                {
                    switch (mainArgument)
                    {
                            case "/a":
                                    int maxClients = 1;

                                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["TweetPool_MaxClients"]))
                                    maxClients = Convert.ToInt32(ConfigurationManager.AppSettings["TweetPool_MaxClients"]);

                                    if (agentPool.Count <= (maxClients-1))
                                    {        
                                        Console.WriteLine("-------------------------\n");
                                        agentPool.Add(new TweetAgent());
                                        processorPool.Add(new TweetProcessor());
                                        myLogger.LogInformation("Added additional agent, per user request. Current Agent Count: " + agentPool.Count.ToString());
                                        myLogger.LogInformation("Added additional processor, per user request. Current Processor Count: " + processorPool.Count.ToString());
                                        Console.WriteLine("-------------------------\n");
                                    }
                                    else
                                        myLogger.LogWarning("WARNING: You have reached the maximum number of agents allowed in the current implementation.");
                                break;
                            case "/r":
                                    if (agentPool.Count - 1 > 0)
                                    {
                                        Console.WriteLine("-------------------------\n");
                                        agentPool.RemoveAt((agentPool.Count - 1));
                                        processorPool.RemoveAt((agentPool.Count - 1));
                                        myLogger.LogInformation("Removing agent from pool, per user request. Current Agent Count: " + agentPool.Count.ToString());
                                        myLogger.LogInformation("Removing processor from pool, per user request. Current Processor Count: " + processorPool.Count.ToString());
                                        Console.WriteLine("-------------------------\n");
                                    }
                                    else
                                        myLogger.LogWarning("WARNING: Must maintain at least one agent and processor within the pool. Request denied.");
                                break;
                            case "/c":
                                    Console.WriteLine("-------------------------\n");
                                    myLogger.LogInformation("Current Agent Count: " + agentPool.Count.ToString());
                                    myLogger.LogInformation("Current Processor Count: " + processorPool.Count.ToString());
                                    Console.WriteLine("-------------------------\n");
                                break;
                            case "/f":
                                        if (!String.IsNullOrEmpty(argModifier))
                                        {
                                            int agentNum = Convert.ToInt32(argModifier);
                                            
                                            if (agentNum <= 0)
                                                agentNum = 1;

                                            if (agentPool.Count >= agentNum)
                                            {
                                                agentPool[(agentNum-1)].forceProcessingStop = true;
                                                myLogger.LogInformation("Processing Force Stopped on Agent " + agentNum.ToString() + " .");
                                            }
                                            else
                                                myLogger.LogWarning("Agent " + agentNum.ToString() + " is not available.");
                                        }
                                        else
                                        {
                                            agentPool[0].forceProcessingStop = true;
                                            myLogger.LogInformation("Processing Force Stopped on Default Agent 1 .");
                                        }
                                break;
                            case "/p":
                                        if (!String.IsNullOrEmpty(argModifier))
                                        {
                                            int agentNum = Convert.ToInt32(argModifier);
                                            
                                            if (agentNum <= 0)
                                                agentNum = 1;

                                            if (agentPool.Count >= agentNum)
                                            {
                                                Task.WhenAll(processorPool[(agentNum-1)].ProcessTweets(myLogger, agentPool[(agentNum-1)]));
                                            }
                                            else
                                                myLogger.LogWarning("Agent " + agentNum.ToString() + " is not available.");
                                        }
                                        else
                                        {
                                            processorPool[0].ProcessTweets(myLogger, agentPool[0]);
                                        }
                                break;
                            case "/s":
                                        if (!String.IsNullOrEmpty(argModifier))
                                        {
                                            int agentNum = Convert.ToInt32(argModifier);

                                            if (agentNum <= 0)
                                                agentNum = 1;

                                            if (agentPool.Count >= agentNum)
                                                statsProcessor.ProcessStats(myLogger, agentPool[(agentNum-1)]);
                                            else
                                                myLogger.LogWarning("Agent " + agentNum.ToString() + " is not available.");
                                        }
                                        else
                                        {
                                            statsProcessor.ProcessStats(myLogger, agentPool[0]);
                                        }
                                break;
                            default:
                                Console.WriteLine("-------------------------\n");
                                Console.WriteLine("TweetPool Console Client.\n");
                                Console.WriteLine("-------------------------\n");
                                Console.WriteLine("Argument ------------ \tDescription\n");
                                Console.WriteLine("/a\t\t\tAdd new agent to pool.");
                                Console.WriteLine("/r\t\t\tRemove last agent from pool.");
                                Console.WriteLine("/c\t\t\tShow current number of agents.");
                                Console.WriteLine("/f #\t\t\tForce stop processing on the agent (# - optional).");
                                Console.WriteLine("/p #\t\t\tProcess the Twitter Stream on the agent (# - optional).");
                                Console.WriteLine("/s #\t\t\tReview the Twitter Stream Statistics on the agent (# - optional).");
                                Console.WriteLine("-------------------------\n");
                                break;
                    }

                Console.WriteLine("\n You may either [1] Press enter for help, [2] Enter an Argument to Proceed, or [3] Type 'stop' to end  . . .");
                commandEntry = Console.ReadLine();

                    if (commandEntry.Contains("/"))
                    {
                        string[] commandArray = commandEntry.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries);

                        if (commandArray.Length > 0)
                            mainArgument = commandEntry.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries)[0];
                        else
                            mainArgument = "";

                        if (commandArray.Length > 1)
                            argModifier = commandEntry.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries)[1];
                        else
                            argModifier = "";
                    }
                    else
                    {
                        mainArgument = "";
                        argModifier = "";
                    }

            } while (!commandEntry.ToLower().Equals("stop"));
        }
}