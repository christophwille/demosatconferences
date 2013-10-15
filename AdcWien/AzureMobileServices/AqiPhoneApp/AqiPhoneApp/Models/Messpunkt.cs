using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AqiPhoneApp.Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Messpunkt
    {
        public int id { get; set; }
        [JsonProperty("name")]
        public string StationsName { get; set; }
        public string timestamp_utc { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string height { get; set; }
        public string state { get; set; }
        public string ozon1h { get; set; }
        public string ozon1hTimestamp_utc { get; set; }
        public string ozon8h { get; set; }
        public string ozon1hMax { get; set; }
        public string ozon1hMaxTimestamp_utc { get; set; }
        public string stationId { get; set; }
    }
}
