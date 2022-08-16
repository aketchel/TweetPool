namespace TweetPool;

using log4net;
using System;
using System.Configuration;
using TweetLibrary;
using TweetLibrary.Interfaces;
using Microsoft.Extensions.Logging;

public class TweetStatsProcessor : ITweetStatsProcessor
{
    public void ProcessStats(ILogger myLogger, TweetAgent myAgent)
    {
            try
            {
                    TweetStats myStats = myAgent.AgentStats;

                    myLogger.LogInformation("---------------------------------\n");   
                    myLogger.LogInformation("Total Tweets: " + myStats.TweetCount.ToString() + "\n");

                    if (myStats.HashTagLeaderboard.Count > 0)
                    {
                        myLogger.LogInformation("Top Hash Tag: " + myStats.TopHashTag.ToString() + "\n");

                        int numberOfHashTagsToDisplay = Convert.ToInt32(ConfigurationManager.AppSettings["TweetPool_NumberOfHashtags"]);

                        foreach (KeyValuePair<string, int> hashTag in myStats.HashTagLeaderboard.OrderByDescending(d => d.Value))
                        {
                            numberOfHashTagsToDisplay--;
                            myLogger.LogInformation(hashTag.Key + ": " + hashTag.Value.ToString() + "\n");

                            if (numberOfHashTagsToDisplay <= 0)
                                break;
                        }
                    }
                    else
                        myLogger.LogInformation("No HashTags Available for Statistical Data. " + myStats.TweetCount.ToString() + " Tweets Processed. \n");

                    myLogger.LogInformation("---------------------------------\n"); 
            }
            catch (Exception ex)
            {
                myLogger.LogError("Error Processing Stats.");
            }

    }
}