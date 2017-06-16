using Newtonsoft.Json;

namespace Eleonora.Core.Models
{
    public class Search
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "search")]
        public string Text { get; set; }
    }
}
