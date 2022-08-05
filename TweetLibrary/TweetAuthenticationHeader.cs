
namespace TweetLibrary;
using System;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.Collections;

public class TweetAuthenticationHeader
{
        private string OAuthConsumerKey;
        private string OAuthConsumerSecret;
        private string OAuthAccessToken;
        private string OAuthAccessKey;

        public string ConsumerKey { get { return OAuthConsumerKey; } set { OAuthConsumerKey = value;} }
        public string ConsumerSecret  { get { return OAuthConsumerSecret; } set { OAuthConsumerSecret = value;} }

        public string AccessToken  { get { return OAuthAccessToken; } set { OAuthAccessToken = value;} }

        public string AccessKey  { get { return OAuthAccessKey; } set { OAuthAccessKey = value;} }


        public TweetAuthenticationHeader()
        {
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Twitter_APIKey"]))
            {
                 this.OAuthConsumerKey = ConfigurationManager.AppSettings["Twitter_APIKey"];
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Twitter_BearerToken"]))
            {
                this.OAuthConsumerSecret = ConfigurationManager.AppSettings["Twitter_BearerToken"];
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Twitter_AccessToken"]))
            {
                this.OAuthAccessToken = ConfigurationManager.AppSettings["Twitter_AccessToken"];
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Twitter_AccessSecret"]))
            {
                this.OAuthAccessKey = ConfigurationManager.AppSettings["Twitter_AccessSecret"];
            }
        }

        public TweetAuthenticationHeader(string consumerKey, string consumerSecret, string accessToken, string accessKey)
        {
            this.OAuthConsumerKey = consumerKey;
            this.OAuthConsumerSecret = consumerSecret;
            this.OAuthAccessToken = accessToken;
            this.OAuthAccessKey = accessKey;
        }

        public string CreateTweetAuthenticationHeader(string method, string url, Dictionary<string, string> parameters, bool includeKey = false)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            SortedDictionary<string, string> sd = new SortedDictionary<string, string>();

            if (parameters != null)
                foreach (string key in parameters.Keys)
                    sd.Add(key, Uri.EscapeDataString(parameters[key]));

            sd.Add("oauth_version", "1.0");
            sd.Add("oauth_consumer_key", this.OAuthConsumerKey);
            sd.Add("oauth_nonce", Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString())));
            sd.Add("oauth_signature_method", "HMAC-SHA1");
            sd.Add("oauth_timestamp", Convert.ToInt64(ts.TotalSeconds).ToString());
            sd.Add("oauth_token", this.OAuthAccessToken);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}&{1}&", method, Uri.EscapeDataString(url));
            foreach (KeyValuePair<string, string> entry in sd)
                sb.Append(Uri.EscapeDataString(string.Format("{0}={1}&", entry.Key, entry.Value)));
            string baseString = sb.ToString().Substring(0, sb.Length - 3);

            HMACSHA1 hasher = new HMACSHA1(new ASCIIEncoding().GetBytes(string.Format("{0}&{1}", Uri.EscapeDataString(this.OAuthConsumerSecret), Uri.EscapeDataString(this.OAuthAccessKey))));
            string oauth_signature = Convert.ToBase64String(hasher.ComputeHash(new ASCIIEncoding().GetBytes(baseString)));

            if (includeKey)
                sb = new StringBuilder("OAuth ");
            else
                sb = new StringBuilder("");

            sb.AppendFormat("oauth_consumer_key=\"{0}\",", Uri.EscapeDataString(this.OAuthConsumerKey));
            sb.AppendFormat("oauth_nonce=\"{0}\",", Uri.EscapeDataString(Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()))));
            sb.AppendFormat("oauth_signature=\"{0}\",", Uri.EscapeDataString(oauth_signature));
            sb.AppendFormat("oauth_signature_method=\"{0}\",", Uri.EscapeDataString("HMAC-SHA1"));
            sb.AppendFormat("oauth_timestamp=\"{0}\",", Uri.EscapeDataString(Convert.ToInt64(ts.TotalSeconds).ToString()));
            sb.AppendFormat("oauth_token=\"{0}\",", Uri.EscapeDataString(this.OAuthAccessToken));
            sb.AppendFormat("oauth_version=\"{0}\"", Uri.EscapeDataString("1.0"));

            return sb.ToString();
        }
}