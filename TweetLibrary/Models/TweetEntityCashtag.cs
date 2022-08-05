 namespace TweetLibrary.Models;

using System.Text.Json.Serialization;    
    
    
    /// <summary>
    /// Represents a cashtag entity
    /// </summary>
    public class TweetEntityCashtag
    {
        /// <summary>
        /// Start of cashtag
        /// </summary>
        [JsonPropertyName("start")]
        public int Start { get; set; }

        /// <summary>
        /// End of cashtag
        /// </summary>
        [JsonPropertyName("end")]
        public int End { get; set; }

        /// <summary>
        /// Hashtag text
        /// </summary>
        [JsonPropertyName("tag")]
        public string? Tag { get; set; }
    }