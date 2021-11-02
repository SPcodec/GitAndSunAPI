using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitAndSunAPI
{
    class Program
    {
        private static async Task<List<repo>> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/users/SPcodec/repos");
            var repositories = await System.Text.Json.JsonSerializer.DeserializeAsync<List<repo>>(await streamTask);
            return repositories;
        }

        private static async Task<string> ProcessSun(string date = "today")
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var stringTask = client.GetStringAsync($"https://api.sunrise-sunset.org/json?lat=51.5072&lng=0.1276&date={date}");

            var msg = await stringTask;
            return msg;
        }

        private static readonly HttpClient client = new HttpClient();

        public static async Task Main(string[] args)
        {
            var repositories = await ProcessRepositories();

            foreach (var repo in repositories)
            {
                Console.WriteLine(repo.Name);
                Console.WriteLine(repo.Description);
                Console.WriteLine(repo.LastPush);

                var jsonstring = await ProcessSun(repo.LastPush);
                sun account = JsonConvert.DeserializeObject<sun>(jsonstring);
                Console.WriteLine($"Sunrise: {account.results.sunrise}");
                Console.WriteLine($"Sunset: {account.results.sunset}");
                Console.WriteLine();
            }
        }
    }
}
