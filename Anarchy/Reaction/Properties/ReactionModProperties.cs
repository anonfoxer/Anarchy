﻿using Newtonsoft.Json;

namespace Discord
{
    public class ReactionModProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
