using System.Text.Json.Serialization;

namespace CoffeeBreak.Application.Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Admin,
        Client,
        DeliveryMan
    }
}
