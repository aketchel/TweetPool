namespace TweetLibrary.Models;

using System.Text.Json.Serialization;    
    
    /// <summary>
    /// References a tweet
    /// </summary>
    public class TweetReference
    {
        /// <summary>
        /// Reference Type e.g. reply, quote, retweet etc.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Unique ID of Referenced Tweet
        /// </summary>
        [JsonPropertyName("id")]
        public string? ID { get; set; }
    }