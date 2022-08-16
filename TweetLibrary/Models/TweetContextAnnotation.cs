namespace TweetLibrary.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Tweet context annotation - a Domain/Entity pair
/// </summary>
public class TweetContextAnnotation
{
    private TweetContextAnnotationDetails? domain = new TweetContextAnnotationDetails();
    private TweetContextAnnotationDetails? entity = new TweetContextAnnotationDetails();

    /// <summary>
    /// Domain details
    /// </summary>
    [JsonPropertyName("domain")]
    public TweetContextAnnotationDetails? Domain { get => domain; set => domain = value; }

    /// <summary>
    /// Entity details
    /// </summary>
    [JsonPropertyName("entity")]
    public TweetContextAnnotationDetails? Entity { get => entity; set => entity = value; }

}