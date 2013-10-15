using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App1.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("complete")]
        public bool Complete { get; set; }
    }
}
