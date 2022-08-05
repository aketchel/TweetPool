namespace TweetPool;

using log4net;
using TweetLibrary;
using TweetLibrary.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;

public class HttpClientTwitterStreamService : IHttpClientServiceImplementation
{
    private static readonly HttpClient myClient = new HttpClient();

    public HttpClientTwitterStreamService()
    {
        myClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["Twitter_SampleAPIURL"]);
        myClient.Timeout = new TimeSpan(0, 0, 30);
        myClient.DefaultRequestHeaders.Clear();
        myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json",0.1));
    }

    public async Task Execute(ILog myLogger, TweetAgent myAgent, string apiMETHOD, int agentTimeoutSetting)
    {
        myClient.Timeout = new TimeSpan(0,0, agentTimeoutSetting);
        await myAgent.InitiateAgent(myLogger, myClient, apiMETHOD, ConfigurationManager.AppSettings["Twitter_SampleAPIURL"], ConfigurationManager.AppSettings["Twitter_BearerToken"], agentTimeoutSetting, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
    }
}