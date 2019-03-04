﻿using System.Threading.Tasks;

namespace CdlMovieCore.Rest.Serialization
{
    /// <summary>
    /// Represents the contracts for Deserialization
    /// </summary>
    public interface ISerialiazer
    {
    	/// <summary>
    	/// Gets or sets the root element for serialization
    	/// </summary>
    	string RootElement { get; set; }

    	/// <summary>
    	/// Gets or sets the date format for deserialization
    	/// </summary>
    	string DateFormat { get; set; }

    	/// <summary>
    	/// Serializes <see cref="T"/> asynchronously based on a string value
    	/// </summary>
    	/// <typeparam name="T">The type to be Serialized</typeparam>
    	/// <param name="obj">Instance of the <see cref="T"/></param>
    	/// <returns>The <see cref="T"/> serialized into a string</returns>
    	Task<string> SerializeAsync<T>(T obj) where T : class;
    }
}
