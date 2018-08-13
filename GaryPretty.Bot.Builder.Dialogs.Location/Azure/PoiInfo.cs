using Newtonsoft.Json;
using System;

namespace GaryPretty.Bot.Builder.Dialogs.Location.Azure
{
    [Serializable]
    public class PoiInfo
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
