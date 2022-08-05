namespace TweetLibrary.Models;

using System.Text.Json.Serialization;  

    /// <summary>
    /// Represents a mention entity
    /// </summary>
    public class TweetEntityMention
    {
        /// <summary>
        /// End of mention
        /// </summary>
        [JsonPropertyName("end")]
        public int End { get; set; }

        /// <summary>
        /// ID of mentioned user
        /// </summary>
        [JsonPropertyName("id")]
        public string? ID { get; set; }

        /// <summary>
        /// Start of mention
        /// </summary>
        [JsonPropertyName("start")]
        public int Start { get; set; }

        /// <summary>
        /// Mentioned username
        /// </summary>
        [JsonPropertyName("username")]
        public string? Username { get; set; }
    }