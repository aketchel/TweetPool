namespace TweetLibrary;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class HttpClientTwitterStreamService : Interfaces.IHttpClientServiceImplementation
{
    private bool firstTime = true;
    private readonly HttpClient myClient = new HttpClient();

    public HttpClientTwitterStreamService()
    {
        myClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["Twitter_SampleAPIURL"]);
        myClient.Timeout = new TimeSpan(0, 0, 30);
        myClient.DefaultRequestHeaders.Clear();
        myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json",0.1));
    }

    public async Task Execute(ILogger myLogger, TweetAgent myAgent, string apiMETHOD, int agentTimeoutSetting)
    {
        if (firstTime)
        {
            myClient.Timeout = new TimeSpan(0,0, agentTimeoutSetting);
            firstTime = false;
        }

        await myAgent.InitiateAgent(myLogger, myClient, apiMETHOD, ConfigurationManager.AppSettings["Twitter_SampleAPIURL"], ConfigurationManager.AppSettings["Twitter_BearerToken"], agentTimeoutSetting, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
    }
}