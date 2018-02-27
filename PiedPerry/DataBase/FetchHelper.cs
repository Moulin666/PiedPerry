using System;
using System.IO;
using System.Json;
using System.Net;
using System.Threading.Tasks;

namespace PiedPerry.DataBase
{
    public class FetchHelper
    {
        public async Task<string> FetchObject(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    JsonValue json = await Task.Run(() => JsonValue.Load(stream));
                    return json.ToString();
                }
            }
        }
    }
}
