using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Eleonora.Core.Models
{
    public class Landmark
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "confidence")]
        public double Confidence { get; set; }
    }

    public class Landmarks
    {
        private List<Landmark> items;
        [JsonProperty(PropertyName = "landmarks")]
        public List<Landmark> Items { get => items; set => items = value; }

        public Landmarks()
        {
            items = new List<Landmark>();
        }
    }
}
