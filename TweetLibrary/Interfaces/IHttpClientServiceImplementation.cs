namespace TweetLibrary.Interfaces;

using log4net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net;

public interface IHttpClientServiceImplementation
{
    public Task Execute(ILogger myLogger, TweetAgent myAgent, string apiMETHOD, int agentTimeoutSetting);
}