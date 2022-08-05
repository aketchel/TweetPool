namespace TweetLibrary.Interfaces;

using log4net;

public interface IHttpClientServiceImplementation
{
    public Task Execute(ILog myLogger, TweetAgent myAgent, string apiMETHOD, int agentTimeoutSetting);
}