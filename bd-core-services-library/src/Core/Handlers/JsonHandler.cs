using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BD.Core.Handlers
{
    /// <summary> The Json Handler Implementation. </summary>
    public class JsonHandler
    {
        /// <summary> The json serializer settings </summary>
        public static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new JsonConverter[] { new StringEnumConverter() }
        };

        /// <summary> Deserializes the specified input. </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string input) => JsonConvert.DeserializeObject<T>(input);

        /// <summary> Serializes the specified object. </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string Serialize(object obj) => JsonConvert.SerializeObject(obj);
    }
}
