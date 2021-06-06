using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GroceryStore.Persistence
{
    public static class GroceryStoreLibrary
    {
        public static T Deserialize<T>(byte[] data) where T : class
        {
            var bytesAsString = Encoding.UTF8.GetString(data);

            var businessobject = System.Text.Json.JsonSerializer.Deserialize<T>(bytesAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            });

            return businessobject;
        }

        public static string Serialize<T>(List<T> data) where T : class
        {
            var person = JsonConvert.SerializeObject(data);

            return person;
        }
    }

   
}
