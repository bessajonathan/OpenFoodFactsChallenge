using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace OpenFoodFacts.Domain.Enums
{

    [JsonConverter(typeof(StringEnumConverter))]
    public enum EDatabaseStatus
    {
        [EnumMember(Value = "ok")]
        Ok,
        [EnumMember(Value = "failed")]
        Failed,
    }
}
