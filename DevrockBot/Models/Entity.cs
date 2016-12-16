using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DevrockBot.Models
{
    /// <summary>
    /// Object of schema.org types
    /// </summary>
    public partial class Entity
    {
        /// <summary>
        /// Initializes a new instance of the Entity class.
        /// </summary>
        public Entity() { }

        /// <summary>
        /// Initializes a new instance of the Entity class.
        /// </summary>
        public Entity(string type = default(string))
        {
            Type = type;
        }

        /// <summary>
        /// Entity Type (typically from schema.org types)
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

    }
}
