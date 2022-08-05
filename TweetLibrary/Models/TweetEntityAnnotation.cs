namespace TweetLibrary.Models;

using System.Text.Json.Serialization;  

/// <summary>
    /// Represents an annotation entity
    /// </summary>
    public class TweetEntityAnnotation
    {
        /// <summary>
        /// Start of annotation text
        /// </summary>
        [JsonPropertyName("start")]
        public int Start { get; set; }

        /// <summary>
        /// End of annotation text
        /// </summary>
        [JsonPropertyName("end")]
        public int End { get; set; }

        /// <summary>
        /// Probability that Text belongs to Category.
        /// </summary>
        [JsonPropertyName("probability")]
        public float Probability { get; set; }

        /// <summary>
        /// Category that Text belongs to
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Annotated text
        /// </summary>
        [JsonPropertyName("normalized_text")]
        public string? NormalizedText { get; set; }
    }