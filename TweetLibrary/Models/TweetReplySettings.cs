namespace TweetLibrary.Models; 
using System.Text.Json.Serialization;
 
 
    /// <summary>
    /// Defines who can reply to a tweet
    /// </summary>
    public enum TweetReplySettings
    {
        /// <summary>
        /// No Reply Settings Defined.
        /// </summary>
        [JsonPropertyName("")]
        None,

        /// <summary>
        /// Anyone and Everyone can reply to this tweet.
        /// </summary>
        [JsonPropertyName("everyone")]
        Everyone,

        /// <summary>
        /// Only the users mentioned in the tweet can reply to this tweet.
        /// </summary>
        [JsonPropertyName("mentionedUsers")]
        MentionedUsers,

        /// <summary>
        /// Anyone following the user who tweeted can reply to this tweet.
        /// </summary>
        [JsonPropertyName("following")]
        Following
    }