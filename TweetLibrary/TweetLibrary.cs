namespace TweetLibrary;

using System;
using log4net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net;

public static class TweetLibrary
{

    public static void HelloWorld()
    {
        Console.WriteLine("Hello World from TweetLibrary");
    }

    public static void HelloWorldWithLogger(ILogger myLogger)
    {
        myLogger.LogInformation("Hello World from TweetLibrary logger.");
    }

}
