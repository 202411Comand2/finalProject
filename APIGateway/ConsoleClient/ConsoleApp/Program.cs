using System.Net.Http.Json;

namespace ConsoleApp
{
    internal class Program
    {
           
            static async Task Main()
            {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5010")
            };

            // GET запрос
            var response = await client.GetAsync("");
            var s = await response.Content.ReadAsStringAsync();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // POST запрос
            var newProduct = new StringContent("\"Tablet\"");
            await client.PostAsync("/products", newProduct);
        }
    }
}
