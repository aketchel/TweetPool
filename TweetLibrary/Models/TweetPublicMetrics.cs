namespace TweetLibrary.Models;

using System.Text.Json.Serialization;  

    /// <summary>
    /// Metrics available to the public
    /// </summary>
    public class TweetPublicMetrics
    {
        /// <summary>
        /// Total Number of ReTweets
        /// </summary>
        [JsonPropertyName("retweet_count")]
        public int RetweetCount { get; set; }

        /// <summary>
        /// Total Number of Replies
        /// </summary>
        [JsonPropertyName("reply_count")]
        public int ReplyCount { get; set; }

        /// <summary>
        /// Total Number of Likes
        /// </summary>
        [JsonPropertyName("like_count")]
        public int LikeCount { get; set; }

        /// <summary>
        /// Total Number of Quotes
        /// </summary>
        [JsonPropertyName("quote_count")]
        public int QuoteCount { get; set; }
    }