using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DownloadWebsiteContent
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://ppearl.vercel.app");
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                var lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                var lineContainingText = lines.FirstOrDefault(line => line.Contains("loadstring(game:HttpGet("));
                Console.WriteLine(lineContainingText);
                Console.ReadKey();
            }
        }
    }
}
