namespace TweetPool;

using log4net;
using System;
using System.Configuration;
using TweetLibrary;
using TweetLibrary.Interfaces;

public class TweetStatsProcessor : ITweetStatsProcessor
{
    public void ProcessStats(ILog myLogger, TweetAgent myAgent)
    {
            try
            {
                    TweetStats myStats = myAgent.AgentStats;

                    myLogger.Info("---------------------------------\n");   
                    myLogger.Info("Total Tweets: " + myStats.TweetCount.ToString() + "\n");

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
}