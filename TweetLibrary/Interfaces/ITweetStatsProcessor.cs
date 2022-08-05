namespace TweetLibrary.Interfaces;

using log4net;

public interface ITweetStatsProcessor
{
    public void ProcessStats(ILog myLogger, TweetAgent myAgent);
}