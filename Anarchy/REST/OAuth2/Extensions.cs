﻿using System.Collections.Generic;

namespace Discord
{
    public static class OAuth2Extensions
    {
        public static IReadOnlyList<AuthorizedApp> GetAuthorizedApps(this DiscordClient client)
        {
            return client.HttpClient.Get($"/oauth2/tokens")
                                .Deserialize<IReadOnlyList<AuthorizedApp>>().SetClientsInList(client);
        }


        //doesnt work btw
        /// <summary>
        /// Removes an authorized app from the current user
        /// </summary>
        /// <param name="appId">ID of the application</param>
        public static void RemoveAuthorizedApp(this DiscordClient client, ulong appId)
        {
            client.HttpClient.Delete($"/oauth2/tokens/{appId}");
        }
    }
}
