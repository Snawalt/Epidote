using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Epidote.Utils
{
    public class SafeWebClient
    {
        static SafeWebClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        public static HttpResponseMessage Get(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.ExpectContinue = false;
                return client.GetAsync(url).Result;
            }
        }

        public static HttpResponseMessage Post(string url, HttpContent content)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.ExpectContinue = false;
                return client.PostAsync(url, content).Result;
            }
        }

        public static HttpResponseMessage Put(string url, HttpContent content)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.ExpectContinue = false;
                return client.PutAsync(url, content).Result;
            }
        }

        public static HttpResponseMessage Delete(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.ExpectContinue = false;
                return client.DeleteAsync(url).Result;
            }
        }

        public static string DownloadString(string url)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
                return client.DownloadString(url);
            }
        }

        public static void DownloadFile(string url, string filepath)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, filepath);
            }
        }
    }
}
