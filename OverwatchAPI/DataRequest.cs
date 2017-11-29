using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//IO 
using System.Net;
using System.IO;

//JSON
using Newtonsoft.Json;

namespace OverwatchStats
{
    delegate void Callback(DataRequest data);

    class DataRequest
    {
        Callback callback;

        public Profile profile { get; private set; }

        public DataRequest(Callback cb)
        {
            callback = cb;
            profile = new Profile();
        }

        public async Task<bool> Lookup(string name)
        {
            string url = "https://ow-api.com/v1/stats/pc/eu/" + name + "/profile";
            string json = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }
            catch (WebException wException)
            {
                return false;
            }

            ParseJson(json);
            callback(this);
            return true;
        }

        private void ParseJson(string data)
        {
            profile = JsonConvert.DeserializeObject<Profile>(data);
        }
    }
}
