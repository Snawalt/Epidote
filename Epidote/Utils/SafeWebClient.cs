using System.Net;
using System.Net.Http;

namespace Epidote.Utils
{
    public class SafeWebClient
    {
        // Static constructor that sets the security protocol to be used by the ServicePointManager to TLS1.2, TLS1.1 and TLS.
        static SafeWebClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        // Method that sends a GET request to the specified URL and returns the response as a HttpResponseMessage.
        public static HttpResponseMessage Get(string url)
        {
            // Use a HttpClient instance within a using block for efficient resource management.
            using (var client = new HttpClient())
            {
                // Set the ExpectContinue header to false.
                client.DefaultRequestHeaders.ExpectContinue = false;
                // Send the GET request and return the result.
                return client.GetAsync(url).Result;
            }
        }

        // Method that sends a POST request to the specified URL with the specified content and returns the response as a HttpResponseMessage.
        public static HttpResponseMessage Post(string url, HttpContent content)
        {
            // Use a HttpClient instance within a using block for efficient resource management.
            using (var client = new HttpClient())
            {
                // Set the ExpectContinue header to false.
                client.DefaultRequestHeaders.ExpectContinue = false;
                // Send the POST request and return the result.
                return client.PostAsync(url, content).Result;
            }
        }

        // Method that sends a PUT request to the specified URL with the specified content and returns the response as a HttpResponseMessage.
        public static HttpResponseMessage Put(string url, HttpContent content)
        {
            // Use a HttpClient instance within a using block for efficient resource management.
            using (var client = new HttpClient())
            {
                // Set the ExpectContinue header to false.
                client.DefaultRequestHeaders.ExpectContinue = false;
                // Send the PUT request and return the result.
                return client.PutAsync(url, content).Result;
            }
        }

        // Method that sends a DELETE request to the specified URL and returns the response as a HttpResponseMessage.
        public static HttpResponseMessage Delete(string url)
        {
            // Use a HttpClient instance within a using block for efficient resource management.
            using (var client = new HttpClient())
            {
                // Set the ExpectContinue header to false.
                client.DefaultRequestHeaders.ExpectContinue = false;
                // Send the DELETE request and return the result.
                return client.DeleteAsync(url).Result;
            }
        }

        // Method to download a string from a given URL
        public static string DownloadString(string url)
        {
            // Using a WebClient to download the string from the URL
            using (var client = new WebClient())
            {
                // Adding a user agent to the request headers
                client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
                // Return the string from the URL
                return client.DownloadString(url);
            }
        }

        // Method to download a file from a given URL to a given filepath
        public static void DownloadFile(string url, string filepath)
        {
            // Using a WebClient to download the file from the URL
            using (var client = new WebClient())
            {
                // Downloading the file from the URL to the specified filepath
                client.DownloadFile(url, filepath);
            }
        }
    }
}
