﻿using Newtonsoft.Json;

namespace Discord
{
    public class MutualGuild
    {
        [JsonProperty("id")]
        public ulong Id { get; private set; }


        [JsonProperty("nick")]
        public string Nickname { get; private set; }


        public override string ToString()
        {
            return Id.ToString();
        }


        public static implicit operator ulong(MutualGuild instance)
        {
            return instance.Id;
        }
    }
}
