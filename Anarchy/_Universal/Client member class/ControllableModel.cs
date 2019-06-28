﻿using System;

namespace Discord
{
    public class ControllableModel
    {
        protected event EventHandler OnClientUpdated;

        private DiscordClient _client;
        internal DiscordClient Client
        {
            get { return _client; }
            set
            {
                _client = value;

                OnClientUpdated?.Invoke(this, new EventArgs());
            }
        }
    }
}