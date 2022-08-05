namespace TweetLibrary.Models;

using System.Text.Json.Serialization; 
    
    /// <summary>
    /// Tweet image details
    /// </summary>
    public class TweetEntityImage
    {
        /// <summary>
        /// Image height
        /// </summary>
        [JsonPropertyName("height")]
        public int Height { get; set; }

        /// <summary>
        /// Url for image
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>
        /// Image width
        /// </summary>
        [JsonPropertyName("width")]
        public int Width { get; set; }
    }