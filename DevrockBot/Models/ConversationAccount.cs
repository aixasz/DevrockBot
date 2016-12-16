using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DevrockBot.Models
{
    /// <summary>
    /// Channel account information for a conversation
    /// </summary>
    public partial class ConversationAccount
    {
        /// <summary>
        /// Initializes a new instance of the ConversationAccount class.
        /// </summary>
        public ConversationAccount() { }

        /// <summary>
        /// Initializes a new instance of the ConversationAccount class.
        /// </summary>
        public ConversationAccount(bool? isGroup = default(bool?), string id = default(string), string name = default(string))
        {
            IsGroup = isGroup;
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Is this a reference to a group
        /// </summary>
        [JsonProperty(PropertyName = "isGroup")]
        public bool? IsGroup { get; set; }

        /// <summary>
        /// Channel id for the user or bot on this channel (Example:
        /// joe@smith.com, or @joesmith or 123456)
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Display friendly name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }

}