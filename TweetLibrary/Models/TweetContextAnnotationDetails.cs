namespace TweetLibrary.Models;

using System.Text.Json.Serialization;  
    
    /// <summary>
    /// Details for the Tweet context annotation domains and entities
    /// </summary>
    public class TweetContextAnnotationDetails
    {
        /// <summary>
        /// Annotation ID
        /// </summary>
        [JsonPropertyName("id")]
        public string? ID { get; set; }

        /// <summary>
        /// Annotation Name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Annotation Description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }