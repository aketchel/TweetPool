namespace TweetLibrary.Models;

using System.Text.Json.Serialization;  
    
    
    /// <summary>
    /// Geographical location of tweet
    /// </summary>
    public class TweetGeo
    {
        /// <summary>
        /// Place ID to define the Location of the Tweet
        /// </summary>
        [JsonPropertyName("place_id")]
        public string? PlaceID { get; set; }
    }