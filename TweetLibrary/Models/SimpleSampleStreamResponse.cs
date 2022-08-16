namespace TweetLibrary.Models;

using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

/*
{
  "data": {
    "attachments": {
      "media_keys": [
        "13_1064638969197977600"
      ]
    },
    "id": "1067094924124872705",
    "text": "Just getting started with Twitter APIs? Find out what you need in order to build an app. Watch this video! https://t.co/Hg8nkfoizN"
  },
  "includes": {
    "media": [
      {
        "duration_ms": 136637,
        "media_key": "13_1064638969197977600",
        "type": "video"
      }
    ]
  }
}
*/
/// <summary>
/// Simple response from Sample API returning data associated with a tweet
/// </summary>
public class SimpleSampleStreamResponse
    {
        StreamResponseData myData = new StreamResponseData();

        /// <summary>
        /// Tweet Data Object from Sample Stream
        /// </summary>
        [JsonPropertyName("data")]
        public StreamResponseData? data { get {return myData; } set { myData = value; } }

        public string ToString()
        {
            return "Data: " + myData.ToString();
        }
    }

    public class StreamResponseData
    {
        private string myID = "";
        private string myText = "";

        /// <summary>
        /// Tweet Unique ID from Sample Stream
        /// </summary>
        [JsonPropertyName("id")]
        public string id { get { return this.myID; } set { this.myID = value; } }

        /// <summary>
        /// Tweet User Text from Sample Stream
        /// </summary>
        [JsonPropertyName("text")]
        public string text { get { return this.myText; } set { this.myText = value; } }

        public MatchCollection hashTags { get { Regex? hashExp = new Regex(@"#\w+"); return hashExp.Matches(this.myText); } }

        public string ToString()
        {
            return "ID: " + myID.ToString() + " Text: " + myText.ToString();
        }
    }