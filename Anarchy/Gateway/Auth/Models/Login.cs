﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.Gateway
{
    /// <summary>
    /// A successful gateway login
    /// </summary>
    internal class Login : Controllable
    {
        public Login()
        {
            OnClientUpdated += (sender, e) =>
            {
                User.SetClient(Client);
                Guilds.SetClientsInList(Client);
                Relationships.SetClientsInList(Client);
                PrivateChannels.SetClientsInList(Client);
            };
        }


        [JsonProperty("session_id")]
        internal string SessionId { get; private set; }


        [JsonProperty("user")]
        public ClientUser User { get; private set; }


        [JsonProperty("guilds")]
        public IReadOnlyList<LoginGuild> Guilds { get; private set; }


        [JsonProperty("private_channels")]
        public List<DMChannel> PrivateChannels { get; private set; }


        [JsonProperty("relationships")]
        public List<Relationship> Relationships { get; private set; }


        public override string ToString()
        {
            return User.ToString();
        }
    }
}