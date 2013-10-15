using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AqiPhoneApp.Models
{
    public class Feedback
    {
        public int id { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
