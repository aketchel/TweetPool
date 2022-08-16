namespace TweetLibrary.Interfaces;

using log4net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net;

public interface ITweetStatsProcessor
{
    public void ProcessStats(ILogger myLogger, TweetAgent myAgent);
}