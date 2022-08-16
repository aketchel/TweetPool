namespace TweetPool;

using log4net;
using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using TweetLibrary;
using TweetLibrary.Interfaces;

public class TweetProcessor : ITweetProcessor
{
    private IHttpClientServiceImplementation tweetClient;

    public TweetProcessor()
    {
        tweetClient = new HttpClientTwitterStreamService();
    }

    public TweetProcessor(IHttpClientServiceImplementation newClient)
    {
        tweetClient = newClient;
    }
    
   public async Task ProcessTweets(ILogger myLogger, TweetAgent myAgent)
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
                                    await Task.WhenAll(tweetClient.Execute(myLogger, myAgent, "GET", 30));
                                else
                                    myLogger.LogWarning("WARNING: Stream Processing is already active - only one active stream allowed per member in the current implementation.");
                        }
                        else
                        {
                            myLogger.LogError("Missing Parameter [Twitter_BearerToken] within App.Config file. Please specify the Bearer Token within the configuration file.");
                        }
                    }
                    else
                    {
                         myLogger.LogError("Missing Parameter [Twitter_APIKey] within App.Config file. Please specify the API Key within the configuration file.");
                    }
                }
                else
                {
                    myLogger.LogError("Missing Parameter [Twitter_SampleAPIURL] within App.Config file. Please specify the URL within the configuration file.");
                }
            }
            catch (Exception e)
            {
                if (myLogger != null)
                {
                    if (e.InnerException != null)
                        myLogger.LogError("Error while Processing Tweets " + e.InnerException.ToString());
                    else
                        myLogger.LogError("Error while Processing Tweets " + e.Message.ToString() + " - " + e.StackTrace.ToString());
                }
            }
    }
}