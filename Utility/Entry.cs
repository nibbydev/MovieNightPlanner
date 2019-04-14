using System.Collections.Generic;
using Newtonsoft.Json;

namespace Utility {
    public class Entry {
        [JsonProperty(PropertyName = "mal_id")]
        public int MalId { get; set; }
        
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; set; }
        
        [JsonProperty(PropertyName = "trailer_url")]
        public string TrailerUrl { get; set; }
        
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        
        [JsonProperty(PropertyName = "episodes")]
        public int? Episodes { get; set; }
        
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        
        [JsonProperty(PropertyName = "rating")]
        public string Rating { get; set; }
        
        [JsonProperty(PropertyName = "score")]
        public double? Score { get; set; }
        
        [JsonProperty(PropertyName = "duration")]
        public string Duration { get; set; }
        
        [JsonProperty(PropertyName = "synopsis")]
        public string Synopsis { get; set; }
        
        [JsonProperty(PropertyName = "genres")]
        public List<Genre> Genres { get; set; }
    }

    public class Genre {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}