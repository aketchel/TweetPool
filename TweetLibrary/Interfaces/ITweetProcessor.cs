namespace TweetLibrary.Interfaces;

using log4net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net;
using Microsoft.Extensions.DependencyInjection;

public interface ITweetProcessor
{
   public Task ProcessTweets(ILogger myLogger, TweetAgent myAgent);
}