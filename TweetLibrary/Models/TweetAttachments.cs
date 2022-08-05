namespace TweetLibrary.Models;

using System.Text.Json.Serialization;
    
    
    /// <summary>
    /// Type of attachment in a tweet
    /// </summary>
    public class TweetAttachments
    {
        /// <summary>
        /// Poll IDs
        /// </summary>
        [JsonPropertyName("poll_ids")]
        public List<string>? PollIds { get; set; }

        /// <summary>
        /// Media Keys
        /// </summary>
        [JsonPropertyName("media_keys")]
        public List<string>? MediaKeys { get; set; }
    }