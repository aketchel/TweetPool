namespace TweetLibrary.Models;

using System.Text.Json.Serialization;      
    
    /// <summary>
    /// Entity URL details
    /// </summary>
    public class TweetEntityUrl
    {
        /// <summary>
        /// From Twitter description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// How URL appears in the tweet
        /// </summary>
        [JsonPropertyName("display_url")]
        public string? DisplayUrl { get; set; }

        /// <summary>
        /// Ending position of URL
        /// </summary>
        [JsonPropertyName("end")]
        public int End { get; set; }

        /// <summary>
        /// URL that user typed, may or may not be shortened URL
        /// </summary>
        [JsonPropertyName("expanded_url")]
        public string? ExpandedUrl { get; set; }

        /// <summary>
        /// Details on images (see "TweetEntityImage" for more details) attached to tweet
        /// </summary>
        [JsonPropertyName("images")]
        public List<TweetEntityImage>? Images { get; set; }

        /// <summary>
        /// Starting position of URL
        /// </summary>
        [JsonPropertyName("start")]
        public int Start { get; set; }

        /// <summary>
        /// HTTP status
        /// </summary>
        [JsonPropertyName("status")]
        public int? Status { get; set; }

        /// <summary>
        /// From Twitter title
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Final destination URL
        /// </summary>
        [JsonPropertyName("unwound_url")]
        public string? UnwoundUrl { get; set; }

        /// <summary>
        /// Twitter's internal URL
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }