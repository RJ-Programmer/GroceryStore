using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GroceryStore.Application.Features.Customers.JsonModel
{
    public class JsonCustomersData
    {
        [JsonPropertyName("customers")]
        public List<JsonCustomer> Customers { get; set; }
    }
}
