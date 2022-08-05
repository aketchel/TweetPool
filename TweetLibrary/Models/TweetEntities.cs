namespace TweetLibrary.Models;

using System.Text.Json.Serialization;


    /// <summary>
    /// Entities associated with a tweet
    /// </summary>
    public class TweetEntities
    {
        /// <summary>
        /// Annotation entities
        /// </summary>
        [JsonPropertyName("annotations")]
        public List<TweetEntityAnnotation>? Annotations { get; set; }

        /// <summary>
        /// Cashtag entities
        /// </summary>
        [JsonPropertyName("cashtags")]
        public List<TweetEntityCashtag>? Cashtags { get; set; }

        /// <summary>
        /// Hashtag entities
        /// </summary>
        [JsonPropertyName("hashtags")]
        public List<TweetEntityHashtag>? Hashtags { get; set; }

        /// <summary>
        /// Mention entities
        /// </summary>
        [JsonPropertyName("mentions")]
        public List<TweetEntityMention>? Mentions { get; set; }

        /// <summary>
        /// URL entities
        /// </summary>
        [JsonPropertyName("urls")]
        public List<TweetEntityUrl>? Urls { get; set; }
    }