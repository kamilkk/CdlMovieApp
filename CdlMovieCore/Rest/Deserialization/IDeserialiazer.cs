using System.Threading.Tasks;

namespace CdlMovieCore.Rest.Deserialization
{
    /// <summary>
    /// Represents the contracts for Deserialization
    /// </summary>
    public interface IDeserialiazer
    {
    	/// <summary>
    	/// Gets or sets the root element for deserialization
    	/// </summary>
    	string RootElement { get; set; }

    	/// <summary>
    	/// Gets or sets the date format for deserialization
    	/// </summary>
    	string DateFormat { get; set; }

    	/// <summary>
    	/// Deserializes <typeparamref name="T"/> asynchronously based on a string value
    	/// </summary>
    	/// <typeparam name="T">Type to be deserialized</typeparam>
    	/// <param name="value">String to deserialize</param>
    	/// <returns>A instance of <typeparamref name="T"/> deserialized</returns>
    	Task<T> DeserializeAsync<T>(string value) where T : class;
    }
}
