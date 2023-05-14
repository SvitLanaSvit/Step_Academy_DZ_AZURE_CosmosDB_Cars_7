using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DZ_AZURE_CosmosDB_Cars_7
{
    public class Blog
    {
        [JsonProperty("id")]
        public string Id { get; set; } = default!;
        public string Manufacturer { get; set; } = default!;
        public IEnumerable<Auto>? Autos { get; set; }

        public override string ToString()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true

            };
            return System.Text.Json.JsonSerializer.Serialize(this, options);
        }
    }

    public class Auto
    {
        public string Title { get; set; } = default!;
        public IEnumerable<Photo>? PhotoLink { get; set; }
        public float Price { get; set; } = default!;
        public int PS { get; set; } = default!;
        public string? Description { get; set; }
        public Country? Country { get; set; }
    }

    public class Photo
    {
        public string Link { get; set; } = default!;
    }

    public class Country
    {
        public string Name { get; set; } = default!;
    }
}
