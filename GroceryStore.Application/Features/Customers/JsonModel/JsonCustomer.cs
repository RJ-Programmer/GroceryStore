using System.Text.Json.Serialization;

namespace GroceryStore.Application.Features.Customers.JsonModel
{
    public class JsonCustomer
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

    }
}
