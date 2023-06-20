using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Instance de HttpClient
            using (HttpClient httpClient = new HttpClient())
            {
                // URL API
                string url = "http://localhost:5000/api/books";

                // Make GET request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(url);

                
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine("Error getting books. Status code: " + response.StatusCode);
                }
            }
        }
    }
}
