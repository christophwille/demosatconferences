using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AqiPhoneApp.Models
{
    public class StationPush
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

        [JsonProperty(PropertyName = "stationId")]
        public string StationId { get; set; }
    }
}
