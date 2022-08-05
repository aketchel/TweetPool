namespace TweetPool.Tests;

using Xunit;
using TweetLibrary;
using log4net;
using System;
using System.Reflection;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;

public class TweetTests
{
    private static readonly ILog myLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
    
            Assert.True(myAgent.isProcessingActive, "Processing is active when connection is established and processing begins successfully.");
    }

     [Fact]
    public void StatsTest()
    {
            HttpClient myClient = new HttpClient();
            TweetStats mainStats = new TweetStats();
            TweetAgent myAgent = new TweetAgent(mainStats);
            myClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["Twitter_SampleAPIURL"]);
            myClient.Timeout = new TimeSpan(0, 0, 30);
            myClient.DefaultRequestHeaders.Clear();
            myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json",0.1));
            myAgent.InitiateAgent(myLogger, myClient, "GET", ConfigurationManager.AppSettings["Twitter_SampleAPIURL"], ConfigurationManager.AppSettings["Twitter_BearerToken"], 30, "TweetPoolTests");
    
            if (myAgent.isProcessingActive)
            {
                Thread.Sleep(15000);
                Assert.True(mainStats.TweetCount > 0, "Stats Processed Successfully. " + mainStats.TweetCount.ToString() + " tweets processed.");
            }
            else
            {
                Assert.False(true, "Agent unable to process tweets for stats test.");
            }
       
    }
}