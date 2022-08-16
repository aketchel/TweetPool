namespace TweetPool.Tests;

using Xunit;
using TweetLibrary;
using TweetLibrary.Interfaces;
using log4net;
using log4net.Config;
using System;
using System.Reflection;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net;

public class TweetTests
{
    private static readonly ILogger<TweetTests> myLogger;


    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IHttpClientServiceImplementation, HttpClientTwitterStreamService>();
    }
    
    public static void ConfigureLogging(ILoggingBuilder lb)
        {
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Log4Net_Configuration"]))
                {  
                    var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                    XmlConfigurator.Configure(logRepository, new FileInfo(ConfigurationManager.AppSettings["Log4Net_Configuration"]));
                    lb.AddLog4Net();
                }

            lb.SetMinimumLevel(LogLevel.Trace);
        }

    static TweetTests()
    {
        var myServiceCollection = new ServiceCollection();

        var services = myServiceCollection
            .AddLogging(logBuilder => { ConfigureLogging(logBuilder); })
            .BuildServiceProvider();

        myLogger = services.GetService<ILoggerFactory>()
            .AddLog4Net()
            .CreateLogger<TweetTests>();

        ConfigureServices(myServiceCollection);
    }

    [Fact]
    public void ConnectionTest()
    {
            HttpClient myClient = new HttpClient();
            TweetAgent myAgent = new TweetAgent();
            myClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["Twitter_SampleAPIURL"]);
            myClient.Timeout = new TimeSpan(0, 0, 30);
            myClient.DefaultRequestHeaders.Clear();
            myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json",0.1));
            myAgent.InitiateAgent(myLogger, myClient, "GET", ConfigurationManager.AppSettings["Twitter_SampleAPIURL"], ConfigurationManager.AppSettings["Twitter_BearerToken"], 30, "TweetPoolTests");        
            Assert.True(myAgent.isProcessingActive, "Processing is active when connection is established and processing begins.");
            myAgent.forceProcessingStop = true;
    }

     [Fact]
    public void StatsTest()
    {
            HttpClient myClient = new HttpClient();
            TweetAgent myAgent = new TweetAgent();
            myClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["Twitter_SampleAPIURL"]);
            myClient.Timeout = new TimeSpan(0, 0, 30);
            myClient.DefaultRequestHeaders.Clear();
            myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json",0.1));
            Task.WhenAny(myAgent.InitiateAgent(myLogger, myClient, "GET", ConfigurationManager.AppSettings["Twitter_SampleAPIURL"], ConfigurationManager.AppSettings["Twitter_BearerToken"], 30, "TweetPoolTests"));
    
            if (myAgent.isProcessingActive)
            {
                Thread.Sleep(15000);
                Assert.True(myAgent.AgentStats.TweetCount > 0, "Stats Processed Successfully. " + myAgent.AgentStats.TweetCount.ToString() + " tweets processed.");         
                myAgent.forceProcessingStop = true;
            }
            else
            {
                Assert.False(true, "Agent unable to process tweets for stats test.");
            }
       
    }
}