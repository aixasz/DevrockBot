using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using DevrockBot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace DevrockBot.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        /// <summary>
        /// memoryCache
        /// </summary>
        readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Bot Credentials
        /// </summary>
        readonly BotCredentials _botCredentials;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="memoryCache">memory cache service</param>
        /// <param name="botCredentials">bot credentials</param>
        public MessagesController(IMemoryCache memoryCache, IOptions<BotCredentials> botCredentials)
        {
            this._memoryCache = memoryCache;
            this._botCredentials = botCredentials.Value;
        }

        /// <summary>
        /// This method will be called every time the bot receives an activity. This is the messaging endpoint
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="token"></param>
        /// <param name="activity">The activity sent to the bot. I'm using dynamic here to simplify the code for the post</param>
        /// <returns>201 Created</returns>
        [HttpPost]
        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity != null)
            {
                // one of these will have an interface and process it
                switch (GetActivityType(activity.Type))
                {
                    case ActivityTypes.Message:
                        break;
                    case ActivityTypes.ConversationUpdate:
                    case ActivityTypes.ContactRelationUpdate:
                    case ActivityTypes.Typing:
                    case ActivityTypes.DeleteUserData:
                    default:
                        //Trace.TraceError($"Unknown activity type ignored: {toBot.GetActivityType()}");
                        break;
                }
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }

        /// <summary>
        /// Gets and caches a valid token so the bot can send messages.
        /// </summary>
        /// <returns>The token</returns>
        private async Task<string> GetBotApiToken()
        {
            // Check to see if we already have a valid token
            var token = _memoryCache.Get("token")?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                // we need to get a token.
                using (var client = new HttpClient())
                {
                    // Create the encoded content needed to get a token
                    var parameters = new Dictionary<string, string>
                    {
                        {"client_id", _botCredentials.ClientId },
                        {"client_secret", _botCredentials.ClientSecret },
                        {"scope", "https://graph.microsoft.com/.default" },
                        {"grant_type", "client_credentials" }
                    };
                    var content = new FormUrlEncodedContent(parameters);

                    // Post
                    var response = await client.PostAsync("https://login.microsoftonline.com/common/oauth2/v2.0/token", content);

                    // Get the token response
                    token = await response.Content.ReadAsStringAsync();

                    // Cache the token for 15 minutes.
                    _memoryCache.Set("token", token, new DateTimeOffset(DateTime.Now.AddMinutes(15)));
                }
            }

            return token;
        }


        private static string GetActivityType(string type)
        {
            if (string.Equals(type, ActivityTypes.Message, StringComparison.OrdinalIgnoreCase))
                return ActivityTypes.Message;

            if (string.Equals(type, ActivityTypes.ContactRelationUpdate, StringComparison.OrdinalIgnoreCase))
                return ActivityTypes.ContactRelationUpdate;

            if (string.Equals(type, ActivityTypes.ConversationUpdate, StringComparison.OrdinalIgnoreCase))
                return ActivityTypes.ConversationUpdate;

            if (string.Equals(type, ActivityTypes.DeleteUserData, StringComparison.OrdinalIgnoreCase))
                return ActivityTypes.DeleteUserData;

            if (string.Equals(type, ActivityTypes.Typing, StringComparison.OrdinalIgnoreCase))
                return ActivityTypes.Typing;

            if (string.Equals(type, ActivityTypes.Ping, StringComparison.OrdinalIgnoreCase))
                return ActivityTypes.Ping;

            return $"{char.ToLower(type[0])}{type.Substring(1)}";
        }
    }
}
