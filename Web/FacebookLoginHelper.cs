using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook;
using System.Net;
using System.IO;
using System.Configuration;


namespace Web
{
    public class FacebookLoginHelper
    {
        public FacebookLoginHelper()
        {

        }
        ///<summary>
        /// Get the authorisation token
        ///</summary>
        ///<param name="code"></param>
        ///<returns></returns>
        public Dictionary<string, string> GetAccessToken(string code, string scope, string redirectUrl)
        {
            Dictionary<string, string> tokens = new Dictionary<string, string>();
            string clientId = ConfigurationManager.AppSettings["Facebook_API_Key"];
            string clientSecret = ConfigurationManager.AppSettings["Facebook_API_Secret"];
            string url = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}&scope={4}",
                                        clientId, redirectUrl, clientSecret, code, scope);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string retVal = reader.ReadToEnd();

                foreach (string token in retVal.Split('&'))
                {
                    tokens.Add(token.Substring(0, token.IndexOf("=")),
                        token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
                }
            }
            return tokens;
        }
    }

}
