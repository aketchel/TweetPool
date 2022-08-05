namespace TweetLibrary.Interfaces;

using log4net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

public interface ITweetProcessor
{
   public Task ProcessTweets(ILog myLogger, ServiceProvider provider, TweetAgent myAgent);
}