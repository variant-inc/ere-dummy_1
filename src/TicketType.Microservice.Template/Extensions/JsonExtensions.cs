using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TicketType.Microservice.Template.Extensions
{
    public static class JsonExtensions
    {
        public static readonly JsonSerializerSettings SnakeCaseSettings =
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                DateFormatString = "yyyy-MM-ddTHH:mm:ssZ",
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };

        private static readonly JsonSerializerSettings DefaultSettings =
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new DefaultNamingStrategy()
                }
            };

        public static T Deserialize<T>(this string json, JsonSerializerSettings settings) =>
            JsonConvert.DeserializeObject<T>(json, settings ?? DefaultSettings);
        
        public static T Deserialize<T>(this string json) => Deserialize<T>(json, null);

        public static string Serialize<T>(this T obj, JsonSerializerSettings settings) =>
            JsonConvert.SerializeObject(obj, settings ?? DefaultSettings);
        
        public static string Serialize<T>(this T obj) => Serialize(obj, null);
    }
}