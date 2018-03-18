using Newtonsoft.Json;

namespace Automaton.Model
{
    internal class JSONHandler
    {
        /// <summary>
        /// Deserialize json data into object of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonContent"></param>
        /// <returns></returns>
        public static T DeserializeJson<T>(string jsonContent)
        {
            T deserializedJson = JsonConvert.DeserializeObject<T>(jsonContent);

            return deserializedJson;
        }

        /// <summary>
        /// Serialize object of type T into string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string SerializeJson<T>(T jsonObject)
        {
            return JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        }
    }
}