using System.Threading.Tasks;

using Newtonsoft.Json;

namespace CdlMovieCore.Rest.Deserialization
{
    /// <summary>
    /// Represents the Json.NET implementation of <see cref="IDeserialiazer"/> 
    /// </summary>
    public class JsonNetDeserializer : IDeserialiazer
    {
        /// <summary>
        /// Gets or sets the root element for deserialization
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        /// Gets or sets the date format for deserialization
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// Deserializes <see cref="T"/> asynchronously using Json.NET based on a string value
        /// </summary>
        /// <typeparam name="T">Type to be deserialized</typeparam>
        /// <param name="value">String to deserialize</param>
        /// <returns>A instance of <see cref="T"/> deserialized</returns>
        public async Task<T> DeserializeAsync<T>(string value) where T : class
        {
            var deserializationTask = Task.Run(() =>
            {
                if (DateFormat != null)
                {
                    var jsonNetSerializerSetting = new JsonSerializerSettings { DateFormatString = DateFormat };

                    return JsonConvert.DeserializeObject<T>(value, jsonNetSerializerSetting);
                }

                return JsonConvert.DeserializeObject<T>(value);
            });

            return await deserializationTask;
        }
    }
}