namespace TweetLibrary.Interfaces;

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

public interface ITweetAgent
{
    public TweetStats AgentStats { get; set; }

    public bool isProcessingActive { get; }

   public Task HandleAPIResponse(HttpResponseMessage serverResponse, ILogger myLogger);

   public Task InitiateAgent(ILogger myLogger, HttpClient myClient, string apiMETHOD, string apiURL, string bearerToken, int agentTimeoutSetting, string agentName);

}