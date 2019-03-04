using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using CdlMovieCore.Rest.Deserialization;
using CdlMovieCore.Rest.Serialization;

namespace CdlMovieCore.Rest
{
    /// <summary>
    /// Represents the contracts of a client to send <see cref="IRestRequest"/>
    /// </summary>
    public interface IRestClient
    {
        /// <summary>
        /// Gets the <see cref="ISerialiazer"/> implementation for this request
        /// </summary>
        ISerialiazer Serializer { get; }

        /// <summary>
        /// Gets the <see cref="IDeserialiazer"/> implementation for this request
        /// </summary>
        IDeserialiazer Deserialiazer { get; }

        /// <summary>
        /// Gets the <see cref="RestionClientOptions"/> 
        /// </summary>
        IRestClientOptions RestionClientOptions { get; }

        /// <summary>
        /// Gets the internal <see cref="HttpClient"/>
        /// </summary>
        HttpClient HttpClient { get; }

        /// <summary>
        /// Sets the <see cref="CookieContainer"/> 
        /// </summary>
        /// <param name="cookieContainer">The instance of CookieContainer</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestClient"/></returns>
        IRestClient SetCookieContainer(CookieContainer cookieContainer);

        /// <summary>
        /// Sets the base address 
        /// </summary>
        /// <param name="baseAddress">String with the base address</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestClient"/></returns>
        IRestClient SetBaseAddress(string baseAddress);

        /// <summary>
        /// Sets the <see cref="ISerialiazer"/>
        /// </summary>
        /// <param name="serialiazer">Implementation of <see cref="ISerialiazer"/></param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestClient"/></returns>
        IRestClient SetSerializer(ISerialiazer serialiazer);

        /// <summary>
        /// Sets the <see cref="IDeserialiazer"/>
        /// </summary>
        /// <param name="deserialiazer">Implementation of <see cref="IDeserialiazer"/></param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestClient"/></returns>
        IRestClient SetDeserializer(IDeserialiazer deserialiazer);

        /// <summary>
        /// Sets the <see cref="IRestClientOptions"/>
        /// </summary>
        /// <param name="restionClientOptions">The restion client options</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestClient"/></returns>
        IRestClient SetRestionClientOptions(IRestClientOptions restionClientOptions);

        /// <summary>
        /// Adds a default heards that will be sent on every request
        /// </summary>
        /// <param name="headerKey"><see cref="string"/> with the header key</param>
        /// <param name="headerValue"><see cref="string"/> with the header value</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestClient"/></returns>
        IRestClient AddDefaultHeader(string headerKey, string headerValue);

        /// <summary>
        /// Sets the Authorization header
        /// </summary>
        /// <param name="value">The value of the auth header e.g. a Access Token or a full scheme like : "Bearer :ACCESS_TOKEN" </param>
        /// <param name="type">The type of the auth header e.g. "Bearer" </param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestClient"/></returns>
        IRestClient SetAuthorizationHeader(string value, string type = null);

        /// <summary>
        /// Gets the default request, that is deafult implementation of <see cref="IRestRequest"/> interface.
        /// </summary>
        /// <returns>The default request.</returns>
        /// <param name="requestPath">Request relative path.</param>
        IRestRequest GetDefaultRequest(string requestPath);

        /// <summary>
        /// Execute asynchronously a <see cref="IRestRequest"/> 
        /// </summary>
        /// <typeparam name="TResponseContent">Content of the response</typeparam>
        /// <param name="restionRequest">The RestionRequest to be sent</param>
        /// <returns><see cref="IRestResponse{TResponseContent}"/> of the request</returns>
        Task<IRestResponse<TResponseContent>> ExecuteRequestAsync<TResponseContent>(IRestRequest restionRequest)
            where TResponseContent : class;

        /// <summary>
        /// Execute asynchronously a <see cref="IRestRequest"/> 
        /// </summary>
        /// <param name="restionRequest">The RestionRequest to be sent</param>
        /// <returns><see cref="IRestResponse"/> of the request</returns>
        Task<IRestResponse> ExecuteRequestAsync(IRestRequest restionRequest);
    }
}
