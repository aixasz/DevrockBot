﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevrockBot.Models
{
    /// <summary>
    /// Types of Activities
    /// </summary>
    public static class ActivityTypes
    {
        /// <summary>
        /// Message from a user -> bot or bot -> User
        /// </summary>
        public const string Message = "message";

        /// <summary>
        /// Bot added removed to contact list
        /// </summary>
        public const string ContactRelationUpdate = "contactRelationUpdate";

        /// <summary>
        /// This notification is sent when the conversation's properties change, for example the topic name, or when user joins or leaves the group.
        /// </summary>
        public const string ConversationUpdate = "conversationUpdate";

        /// <summary>
        /// a user is typing
        /// </summary>
        public const string Typing = "typing";

        /// <summary>
        /// Bounce a message off of the server without replying or changing it's state
        /// </summary>
        public const string Ping = "ping";

        /// <summary>
        /// End a conversation
        /// </summary>
        public const string EndOfConversation = "endOfConversation";

        /// <summary>
        /// External system has triggered 
        /// </summary>
        public const string Trigger = "trigger";

        /// <summary>
        /// Delete user data
        /// </summary>
        public const string DeleteUserData = "deleteUserData";
    }
}
