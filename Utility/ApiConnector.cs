using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Utility {
    public static class ApiConnector {
        private static readonly HttpClient WebClient = new HttpClient();

        static ApiConnector() {
            WebClient.DefaultRequestHeaders.Add("User-Agent", "Movie Night Client 1.0");
            WebClient.DefaultRequestHeaders.Add("Accept", "*/*");
            WebClient.Timeout = TimeSpan.FromMilliseconds(2000);
        }
        
        public static async Task<Entry> AsyncGet(int id) {
            try {
                var response = await WebClient.GetAsync($"https://api.jikan.moe/v3/anime/{id}");
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Entry>(json);
            } catch {
                return null;
            }
        }
    }
}