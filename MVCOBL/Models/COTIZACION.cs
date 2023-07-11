using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MVCOBL.Models
{


    public partial class COTIZACION
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("terms")]
        public Uri Terms { get; set; }

        [JsonProperty("privacy")]
        public Uri Privacy { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("quotes")]
        public Quotes Quotes { get; set; }
    }

    public partial class Quotes
    {
        [JsonProperty("USDUYU")]
        public double Usduyu { get; set; }
    }

}
