﻿using Newtonsoft.Json;

namespace Discord
{
    public class Invite : BaseInvite
    {
        [JsonProperty("approximate_presence_count")]
        public int OnlineMembers { get; private set; }

        [JsonProperty("approximate_member_count")]
        public int TotalMembers { get; private set; }


        public override string ToString()
        {
            return Code;
        }
    }
}