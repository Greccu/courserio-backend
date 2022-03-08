using System.Text.Json.Serialization;

namespace Courserio.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusEnum
    {
        Pending,
        Accepted,
        Declined
    }
}
