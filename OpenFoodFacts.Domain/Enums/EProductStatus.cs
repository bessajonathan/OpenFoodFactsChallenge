using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace OpenFoodFacts.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EProductStatus
    {
        [EnumMember(Value = "published")]
        Published,
        [EnumMember(Value = "draft")]
        Draft,
        [EnumMember(Value = "trash")]
        Trash,
    }
}
