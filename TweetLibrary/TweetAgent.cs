namespace TweetLibrary;

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net;

public class TweetAgent : Interfaces.ITweetAgent
{
    private bool firstTime = true;
    private bool forceStop = false;
    private bool processingActive = false;
    private TweetStats myAgentStats;

    public TweetStats AgentStats { get { return this.myAgentStats; } set { this.myAgentStats = value; } }

    public bool isProcessingActive { get{ return processingActive; } }

    public bool forceProcessingStop { get { return forceStop; } set { forceStop = value; processingActive = !value; } }

    public TweetAgent()
    {
        myAgentStats = new TweetStats();
    }

    public TweetAgent(TweetStats newStats)
    {
        myAgentStats = newStats;
    }

public async Task HandleAPIResponse(HttpResponseMessage serverResponse, ILogger myLogger)
{
    try
    {
                            myLogger.LogInformation("Handle Response Requested...");

                            if (serverResponse.IsSuccessStatusCode)
                            {
                                List<Models.SimpleSampleStreamResponse> myTweets = new List<Models.SimpleSampleStreamResponse>();

                                myLogger.LogInformation("Successful Response Recieved... Processing Response.");

                                    var stream = await serverResponse.Content.ReadAsStreamAsync();

                                    using (StreamReader streamReader = new StreamReader(stream))
                                    {
                                        string tweetLine;

                                     myLogger.LogInformation("Reading Responses...");
                                        while ((tweetLine = streamReader.ReadLine()) != null && !forceStop)
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

                                                        //myLogger.LogInformation(myTweet.ToString());
                                                }
                                            }
                                            else
                                            {
                                                 myLogger.LogWarning("WARNING: No Additional Results Returned from Stream.");
                                            }
                                        }
                                    }
                            }
                            else
                            {
                                myLogger.LogError("Request failed. Response StatusCode: " + serverResponse.StatusCode.ToString());
                            }
                        
                        if (serverResponse != null)
                            serverResponse.Dispose();

        processingActive = false;
    }
    catch (Exception ex)
    {
        if (myLogger != null)
            myLogger.LogError("Handle Request error: " + ex.InnerException.ToString());
    }
}

public async Task InitiateAgent(ILogger myLogger, HttpClient myClient, string apiMETHOD, string apiURL, string bearerToken, int agentTimeoutSetting, string agentName)
{
        bool agentSuccess = false;

            myLogger.LogInformation("Initating Tweet Agent. Please stand by while we finish Reticulating Splines.");

            try
            {
                if (firstTime)
                {
                    myClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", bearerToken));
                    firstTime = false;
                }

                if (apiMETHOD.ToUpper().Equals("GET"))
                {
                    myLogger.LogInformation("Setting up connection to " + apiURL);
                    myLogger.LogInformation("Request Headers: " + myClient.DefaultRequestHeaders.ToString());

                    try
                    {
                        processingActive = true;
                        forceStop = false;
                        myLogger.LogInformation("Request Started at " + System.DateTime.Now.ToString());

                        HttpResponseMessage myResult = await myClient.GetAsync(apiURL, HttpCompletionOption.ResponseHeadersRead);

                        myLogger.LogInformation("Request Finished at " + System.DateTime.Now.ToString());

                        if (myResult != null)
                        {   
                            myLogger.LogInformation("Response Status Code: " + myResult.StatusCode.ToString());

                            if (myResult.StatusCode.ToString().Equals("TooManyRequests"))
                            {
                                forceStop = true;
                                processingActive = false;
                                myLogger.LogWarning("The endpoint has indicated you have initated too many requests. If multiple connections are allowed, please try again later.");
                            }
                            else
                            {
                                myResult.EnsureSuccessStatusCode();

                                if (myResult.IsSuccessStatusCode)
                                {
                                    myLogger.LogInformation("Processing Request at " + System.DateTime.Now.ToString());
                                    await HandleAPIResponse(myResult, myLogger); 
                                    myLogger.LogInformation("Finished Request Processing at " + System.DateTime.Now.ToString());
                                }
                            }
                        }
                        else
                            myLogger.LogWarning("WARNING: No Result from Client Request.");
                    }
                    catch (Exception e)
                    {
                        if (myLogger != null)
                        {
                            if (e.InnerException != null)
                                myLogger.LogError("Error while submitting API request: " + e.InnerException.ToString());
                            else
                                myLogger.LogError("Error while submitting API request: " + e.Message.ToString() + " - " + e.StackTrace.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (myLogger != null)
                {
                    if (ex.InnerException != null)
                        myLogger.LogError("Error while Initiating TweetAgent: " + ex.InnerException.ToString());
                    else
                        myLogger.LogError("Error while Initiating TweetAgent: " + ex.Message.ToString() + " - " + ex.StackTrace.ToString());
                }
            }
}


}