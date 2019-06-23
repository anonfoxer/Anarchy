﻿using Newtonsoft.Json;

namespace Discord
{
    internal class Experiments
    {
        [JsonProperty("fingerprint")]
        public string Fingerprint { get; set; }

        public override string ToString()
        {
            return Fingerprint;
        }
    }
}