﻿using Newtonsoft.Json;
using System.Drawing;
using System.Net;
using System.Net.Http;

namespace Discord
{
    public class User : Controllable
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("username")]
        public string Username { get; protected set; }

        [JsonProperty("discriminator")]
        public int Discriminator { get; protected set; }

        [JsonProperty("avatar")]
        public string AvatarId { get; protected set; }

        [JsonProperty("bot")]
        private readonly bool _bot;


        public UserType Type
        {
            get
            {
                if (Discriminator == 0)
                    return UserType.Webhook;

                return _bot ? UserType.Bot : UserType.User;
            }
        }


        public virtual void Update()
        {
            User user = Client.GetUser(Id);
            Username = user.Username;
            Discriminator = user.Discriminator;
            AvatarId = user.AvatarId;
        }


        public bool SendFriendRequest()
        {
            if (Id == Client.User.Id)
                return false;

            return Client.SendFriendRequest(Username, Discriminator);
        }


        public void Block()
        {
            Client.BlockUser(Id);
        }


        public void RemoveRelationship()
        {
            if (Id == Client.User.Id)
                return;

            Client.RemoveRelationship(Id);
        }


        public Image GetAvatar()
        {
            if (AvatarId == null)
                return null;

            return (Bitmap)new ImageConverter().ConvertFrom(new HttpClient().GetAsync($"https://cdn.discordapp.com/avatars/{Id}/{AvatarId}.png")
                                                                    .Result.Content.ReadAsByteArrayAsync().Result);
        }


        public override string ToString()
        {
            return $"{Username}#{"0000".Remove(4 - Discriminator.ToString().Length) + Discriminator.ToString()}";
        }
    }
}