namespace TweetLibrary;

using System;
using log4net;

public static class TweetLibrary
{

    public static void HelloWorld()
    {
        Console.WriteLine("Hello World from TweetLibrary");
    }

    public static void HelloWorldWithLogger(ILog myLogger)
    {
        myLogger.Info("Hello World from TweetLibrary logger.");
    }

}
