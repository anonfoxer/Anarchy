﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class ReactionExtentions
    {
        #region management
        public static Reaction CreateGuildReaction(this DiscordClient client, long guildId, ReactionCreationProperties properties)
        {
            var resp = client.HttpClient.Post($"/guilds/{guildId}/emojis", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            Reaction reaction = resp.Content.Json<Reaction>().SetClient(client);
            reaction.GuildId = guildId;
            return reaction;
        }


        public static Reaction ModifyGuildReaction(this DiscordClient client, long guildId, long reactionId, ReactionModProperties properties)
        {
            var resp = client.HttpClient.Patch($"/guilds/{guildId}/emojis/{reactionId}", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ReactionNotFoundException(client, guildId, reactionId);

            return resp.Content.Json<Reaction>().SetClient(client);
        }


        public static bool DeleteGuildReaction(this DiscordClient client, long guildId, long reactionId)
        {
            var resp = client.HttpClient.Delete($"/guilds/{guildId}/emojis/{reactionId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ReactionNotFoundException(client, guildId, reactionId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
        #endregion


        public static List<Reaction> GetGuildReactions(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.Get($"/guilds/{guildId}/emojis");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);
            
            List<Reaction> reactions = resp.Content.Json<List<Reaction>>().SetClientsInList(client);
            foreach (var reaction in reactions) reaction.GuildId = guildId;
            return reactions;
        }


        public static Reaction GetGuildReaction(this DiscordClient client, long guildId, long reactionId)
        {
            var resp = client.HttpClient.Get($"/guilds/{guildId}/emojis/{reactionId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ReactionNotFoundException(client, guildId, reactionId);

            Reaction reaction = resp.Content.Json<Reaction>().SetClient(client);
            reaction.GuildId = guildId;
            return reaction;
        }


        public static List<User> GetMessageReactions(this DiscordClient client, long channelId, long messageId, string reaction, int limit = 25, int afterId = 0)
        {
            var resp = client.HttpClient.Get($"/channels/{channelId}/messages/{messageId}/reactions/{reaction}?limit={limit}&after={afterId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            return resp.Content.Json<List<User>>();
        }


        public static bool AddMessageReaction(this DiscordClient client, long channelId, long messageId, string reaction)
        {
            var resp = client.HttpClient.Put($"/channels/{channelId}/messages/{messageId}/reactions/{reaction}/@me");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }


        public static bool RemoveMessageReaction(this DiscordClient client, long channelId, long messageId, string reaction)
        {
            var resp = client.HttpClient.Delete($"/channels/{channelId}/messages/{messageId}/reactions/{reaction}/@me");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
    }
}