﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class RelationsExtensions
    {
        public static bool AddFriend(this DiscordClient client, NameDiscriminator recipient)
        {
            return client.HttpClient.Post("/users/@me/relationships",
                JsonConvert.SerializeObject(recipient)).StatusCode == HttpStatusCode.NoContent;
        }


        public static bool AddFriend(this DiscordClient client, string username, int discriminator)
        {
            return client.AddFriend(new NameDiscriminator { Username = username, Discriminator = discriminator });
        }


        public static bool RemoveFriend(this DiscordClient client, long userId)
        {
            var resp = client.HttpClient.Delete($"/users/@me/relationships/{userId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new UserNotFoundException(client, userId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }


        public static bool RemoveFriend(this DiscordClient client, User user)
        {
            return client.RemoveFriend(user.Id);
        }


        public static List<Channel> GetClientDMs(this DiscordClient client)
        {
            var resp = client.HttpClient.Get($"/users/@me/channels");

            return resp.Content.Json<List<Channel>>().SetClientsInList(client);
        }


        public static Channel CreateDM(this DiscordClient client, long recipientId)
        {
            var resp = client.HttpClient.Post("/users/@me/channels", "{\"recipient_id\":\"" + recipientId + "\"}");

            return resp.Content.Json<Channel>().SetClient(client);
        }
    }
}