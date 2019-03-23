namespace Newtonsoft.Json.Partial.Tests.Models
{
    using System;

    public class Post
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonNoPartial]
        [JsonProperty(PropertyName = "published")]
        public DateTime Published { get; set; }

        [JsonIgnore]
        public string Owner { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
    }
}
