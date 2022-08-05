namespace TweetLibrary.Models;

using System.Text.Json.Serialization;

/*
{
  "created_at": "Fri Sep 18 18:36:15 +0000 2020",
  "id": 1307025659294674945,
  "id_str": "1307025659294674945",
  "full_text": "Here’s an article that highlights the updates in the new Tweet payload v2 https:\/\/t.co\/oeF3ZHeKQQ",
  "truncated": false,
  "display_text_range": [
    0,
    97
  ],
  "entities": {
    "hashtags": [],
    "symbols": [],
    "user_mentions": [],
    "urls": [
      {
        "url": "https:\/\/t.co\/oeF3ZHeKQQ",
        "expanded_url": "https:\/\/dev.to\/twitterdev\/understanding-the-new-tweet-payload-in-the-twitter-api-v2-1fg5",
        "display_url": "dev.to\/twitterdev\/und…",
        "indices": [
          74,
          97
        ]
      }
    ]
  },
  "source": "<a href=\"https:\/\/mobile.twitter.com\" rel=\"nofollow\">Twitter Web App<\/a>",
  "in_reply_to_status_id": 1304102743196356610,
  "in_reply_to_status_id_str": "1304102743196356610",
  "in_reply_to_user_id": 2244994945,
  "in_reply_to_user_id_str": "2244994945",
  "in_reply_to_screen_name": "TwitterDev",
  "user": {
    "id": 2244994945,
    "id_str": "2244994945",
    "name": "Twitter Dev",
    "screen_name": "TwitterDev",
    "location": "127.0.0.1",
    "description": "The voice of the #TwitterDev team and your official source for updates, news, and events, related to the #TwitterAPI.",
    "url": "https:\/\/t.co\/3ZX3TNiZCY",
    "entities": {
      "url": {
        "urls": [
          {
            "url": "https:\/\/t.co\/3ZX3TNiZCY",
            "expanded_url": "https:\/\/developer.twitter.com\/en\/community",
            "display_url": "developer.twitter.com\/en\/community",
            "indices": [
              0,
              23
            ]
          }
        ]
      },
      "description": {
        "urls": []
      }
    },
    "protected": false,
    "followers_count": 513958,
    "friends_count": 2039,
    "listed_count": 1672,
    "created_at": "Sat Dec 14 04:35:55 +0000 2013",
    "favourites_count": 2145,
    "utc_offset": null,
    "time_zone": null,
    "geo_enabled": true,
    "verified": true,
    "statuses_count": 3635,
    "lang": null,
    "contributors_enabled": false,
    "is_translator": false,
    "is_translation_enabled": false,
    "profile_background_color": "FFFFFF",
    "profile_background_image_url": "http:\/\/abs.twimg.com\/images\/themes\/theme1\/bg.png",
    "profile_background_image_url_https": "https:\/\/abs.twimg.com\/images\/themes\/theme1\/bg.png",
    "profile_background_tile": false,
    "profile_image_url": "http:\/\/pbs.twimg.com\/profile_images\/1283786620521652229\/lEODkLTh_normal.jpg",
    "profile_image_url_https": "https:\/\/pbs.twimg.com\/profile_images\/1283786620521652229\/lEODkLTh_normal.jpg",
    "profile_banner_url": "https:\/\/pbs.twimg.com\/profile_banners\/2244994945\/1594913664",
    "profile_link_color": "0084B4",
    "profile_sidebar_border_color": "FFFFFF",
    "profile_sidebar_fill_color": "DDEEF6",
    "profile_text_color": "333333",
    "profile_use_background_image": false,
    "has_extended_profile": true,
    "default_profile": false,
    "default_profile_image": false,
    "following": null,
    "follow_request_sent": null,
    "notifications": null,
    "translator_type": "regular"
  },
  "geo": null,
  "coordinates": null,
  "place": null,
  "contributors": null,
  "is_quote_status": false,
  "retweet_count": 11,
  "favorite_count": 70,
  "favorited": false,
  "retweeted": false,
  "possibly_sensitive": false,
  "possibly_sensitive_appealable": false,
  "lang": "en"
}
*/

public class Tweet
{
        /// <summary>
        /// Attachments, such as polls, images, and videos
        /// </summary>
        [JsonPropertyName("attachments")]
        public TweetAttachments? Attachments { get; init; }

        /// <summary>
        /// Tweet Authors User ID
        /// </summary>
        [JsonPropertyName("author_id")]
        public string? AuthorID { get; init; }

        /// <summary>
        /// Contextual metadata associated with tweet
        /// </summary>
        [JsonPropertyName("context_annotations")]
        public List<TweetContextAnnotation>? ContextAnnotations { get; init; }

        /// <summary>
        /// ID of conversation this tweet is part of (matches original tweet ID)
        /// </summary>
        [JsonPropertyName("conversation_id")]
        public string? ConversationID { get; init; }

        /// <summary>
        /// Date and Time of Original Tweet
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; init; }

        /// <summary>
        /// Extracted parts of the tweet (subobjects), like hashtags, urls, etc.
        /// </summary>
        [JsonPropertyName("entities")]
        public TweetEntities? Entities { get; init; }

        /// <summary>
        /// Tweet location
        /// </summary>
        [JsonPropertyName("geo")]
        public TweetGeo? Geo { get; init; }

        /// <summary>
        /// Unique ID
        /// </summary>
        [JsonPropertyName("id")]
        public string? ID { get; init; }

        /// <summary>
        /// If replying, Unique User ID of the user being replied to
        /// </summary>
        [JsonPropertyName("in_reply_to_user_id")]
        public string? InReplyToUserID { get; init; }

        /// <summary>
        /// BCP47 Language tag - https://tools.ietf.org/html/bcp47
        /// </summary>
        [JsonPropertyName("lang")]
        public string? Language { get; init; }

        /// <summary>
        /// Non-Public Metrics
        /// </summary>
        [JsonPropertyName("non_public_metrics")]
        public object? NonPublicMetrics { get; init; }

        /// <summary>
        /// Organic Metrics
        /// </summary>
        [JsonPropertyName("organic_metrics")]
        public object? OrganicMetrics { get; init; }

        /// <summary>
        /// Media or links contain sensitive information
        /// </summary>
        [JsonPropertyName("possibly_sensitive")]
        public bool? PossiblySensitive { get; init; }

        /// <summary>
        /// Promoted content metrics
        /// </summary>
        [JsonPropertyName("promoted_metrics")]
        public object? PromotedMetrics { get; init; }

        /// <summary>
        /// Public metrics
        /// </summary>
        [JsonPropertyName("public_metrics")]
        public TweetPublicMetrics? PublicMetrics { get; init; }

        /// <summary>
        /// Any other tweets that this one refers to
        /// </summary>
        [JsonPropertyName("referenced_tweets")]
        public List<TweetReference>? ReferencedTweets { get; init; }

        /// <summary>
        /// Reply Rules - Who Can Reply To This Tweet?
        /// </summary>
        [JsonPropertyName("reply_settings")]
        public TweetReplySettings ReplySettings { get; set; }

        /// <summary>
        /// Source Application Sending Tweet
        /// </summary>
        [JsonPropertyName("source")]
        public string? Source { get; init; }

        /// <summary>
        /// Tweet Text String
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; init; }
}
