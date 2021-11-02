using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GitAndSunAPI
{
    class repo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("pushed_at")]
        public DateTime LastPushUtc { get; set; }

        public string LastPush => LastPushUtc.ToString("MM/dd/yyyy");

        public DateTime LastPushLocal => LastPushUtc.ToLocalTime();
    }
}
