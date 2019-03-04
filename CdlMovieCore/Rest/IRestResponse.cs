using System;
using System.Net;
using System.Net.Http;

namespace CdlMovieCore.Rest
{
    /// <summary>
    /// Represents the contracts for a response of a <see cref="IRestRequest"/>
    /// </summary>
    /// <typeparam name="TContent">Type of the content of the response</typeparam>
    public interface IRestResponse<out TContent>
    {
        /// <summary>
        /// Gets or sets the <see cref="TContent"/> instance of the response
        /// </summary>
        TContent Content { get; }

        /// <summary>
        /// Get the raw content of the response if allowed through the <see cref="RestionClientOptions"/>
        /// </summary>
        string RawContent { get; }

        /// <summary>
        /// Gets the HttpStatusCode of the response
        /// </summary>
        HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// Gets or sets the HttpResponseMessage of the response
        /// </summary>
        HttpResponseMessage HttpResponseMessage { get; }

        /// <summary>
        /// Gets or sets the exception of the response
        /// </summary>
        Exception Exception { get; }
    }

    /// <summary>
    /// Represents the contracts for a response of a <see cref="IRestRequest"/>
    /// </summary>
    public interface IRestResponse
    {
        /// <summary>
        /// Get the raw content of the response 
        /// </summary>
        string RawContent { get; }

        /// <summary>
        /// Gets the HttpStatusCode of the response
        /// </summary>
        HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// Gets or sets the HttpResponseMessage of the response
        /// </summary>
        HttpResponseMessage HttpResponseMessage { get; }

        /// <summary>
        /// Gets or sets the exception of the response
        /// </summary>
        Exception Exception { get; }
    }
}
