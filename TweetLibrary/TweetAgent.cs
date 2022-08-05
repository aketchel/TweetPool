namespace TweetLibrary;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using log4net;

public class TweetAgent
{
    private bool processingActive = false;
    private TweetStats myAgentStats;

    public TweetStats AgentStats { get { return this.myAgentStats; } set { this.myAgentStats = value; } }

    public bool isProcessingActive { get{ return processingActive; } }

    public TweetAgent()
    {
        myAgentStats = new TweetStats();
    }

    public TweetAgent(TweetStats newStats)
    {
        myAgentStats = newStats;
    }

public async Task HandleAPIResponse(HttpResponseMessage serverResponse, ILog myLogger)
{
    try
    {
                            myLogger.Info("Handle Response Requested...");

                            if (serverResponse.IsSuccessStatusCode)
                            {
                                List<Models.SimpleSampleStreamResponse> myTweets = new List<Models.SimpleSampleStreamResponse>();

                                myLogger.Info("Successful Response Recieved... Processing Response.");
                                //var streamString = await serverResponse.Content.ReadAsStringAsync();
                                //myTweets = JsonSerializer.Deserialize<List<Models.SimpleSampleStreamResponse>>(streamString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                                var stream = await serverResponse.Content.ReadAsStreamAsync();

                                //if (stream != null)
                                //    myTweets = await JsonSerializer.DeserializeAsync<List<Models.SimpleSampleStreamResponse>>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                                    using (StreamReader streamReader = new StreamReader(stream))
                                    {
                                        string tweetLine;

                                     myLogger.Info("Reading Responses...");
                                        while ((tweetLine = streamReader.ReadLine()) != null)
                                        {
                                            if (!String.IsNullOrEmpty(tweetLine))
                                            {
                                                Models.SimpleSampleStreamResponse myTweet = JsonSerializer.Deserialize<Models.SimpleSampleStreamResponse>(tweetLine, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                                                if (myTweet != null)
                                                {
                                                    myAgentStats.TweetCount = myAgentStats.TweetCount + 1;
                                                    MatchCollection tweetHashTags = myTweet.data.hashTags;

                                                    foreach (Match match in tweetHashTags)
                                                    {
                                                        string matchKey = match.Value;

                                                        if (((SortedDictionary<string,int>)myAgentStats.HashTagLeaderboard).ContainsKey(matchKey))
                                                        {
                                                            ((SortedDictionary<string,int>)myAgentStats.HashTagLeaderboard)[matchKey] = ((int)((SortedDictionary<string,int>)myAgentStats.HashTagLeaderboard)[matchKey]) + 1;
                                                        }
                                                        else
                                                        {
                                                            ((SortedDictionary<string,int>)myAgentStats.HashTagLeaderboard).Add(matchKey, 1);
                                                        }
                                                    }

                                                        //myLogger.Info(myTweet.ToString());
                                                }
                                            }
                                            else
                                            {
                                                 myLogger.Warn("WARNING: No Additional Results Returned from Stream.");
                                            }
                                        }
                                    }
                            }
                            else
                            {
                                myLogger.Error("Request failed. Response StatusCode: " + serverResponse.StatusCode.ToString());
                            }
                        
                        if (serverResponse != null)
                            serverResponse.Dispose();

        processingActive = false;
    }
    catch (Exception ex)
    {
        if (myLogger != null)
            myLogger.Error("Handle Request error: " + ex.InnerException.ToString());
    }
}

public async Task InitiateAgent(ILog myLogger, HttpClient myClient, string apiMETHOD, string apiURL, string bearerToken, int agentTimeoutSetting, string agentName)
{
        bool agentSuccess = false;

            myLogger.Info("Initating Tweet Agent. Please stand by while we finish Reticulating Splines.");

            try
            {
                myClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", bearerToken));

                if (apiMETHOD.ToUpper().Equals("GET"))
                {
                    myLogger.Info("Setting up connection to " + apiURL);
                    myLogger.Info("Request Headers: " + myClient.DefaultRequestHeaders.ToString());

                    try
                    {
                        processingActive = true;
                        myLogger.Info("Request Started at " + System.DateTime.Now.ToString());

                        HttpResponseMessage myResult = await myClient.GetAsync(apiURL, HttpCompletionOption.ResponseHeadersRead);

                        myLogger.Info("Request Finished at " + System.DateTime.Now.ToString());

                       myResult.EnsureSuccessStatusCode();

                        if (myResult.IsSuccessStatusCode)
                        {
                            await HandleAPIResponse(myResult, myLogger); 
                            myLogger.Info("Request Processed at " + System.DateTime.Now.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        myLogger.Error("Error while submitting API request: " + e.InnerException.ToString());

                        if (myLogger != null)
                            myLogger.Error("Error while submitting API request: " + e.InnerException.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                if (myLogger != null)
                    myLogger.Error("Error while Initiating TweetAgent: " + ex.InnerException.ToString());
            }
}


}