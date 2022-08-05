namespace TweetLibrary;

using System.Text.Json.Serialization;

public class TweetStats
{
        private string myTopHashTag = "";
        private int myTweetCount = 0;
        private SortedDictionary<string, int> myHashTagLeaderboard = new SortedDictionary<string, int>();

        /// <summary>
        /// Total Count of Processed Tweets
        /// </summary>
        [JsonPropertyName("TweetCount")]
        public int TweetCount { get { return this.myTweetCount; } set { this.myTweetCount = value; } }

        public string TopHashTag { get { var maxElement = myHashTagLeaderboard.OrderByDescending(d => d.Value).First(); this.myTopHashTag = maxElement.Key; return this.myTopHashTag; } }

        /// <summary>
        /// Sorted Dictionary of Processed HashTags from the Current Processed Tweets
        /// </summary>
        [JsonPropertyName("HashTagLeaderboard")]
        public SortedDictionary<string, int> HashTagLeaderboard { get { return this.myHashTagLeaderboard; } set { this.myHashTagLeaderboard = value; } }

}