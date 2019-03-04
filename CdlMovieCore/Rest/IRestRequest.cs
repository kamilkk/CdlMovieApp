﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using CdlMovieCore.Rest.Serialization;

namespace CdlMovieCore.Rest
{
    /// <summary>
    /// Represents the contracts to send a rest request that will be executed by <see cref="IRestClient"/>
    /// </summary>  
    public interface IRestRequest : IDisposable
    {
        /// <summary>
        /// Gets the HttpMethod for the http request.
        /// </summary>
        HttpMethod Method { get; }

        /// <summary>
        /// Gets or sets the <see cref="ISerialiazer"/> implementation for this request
        /// </summary>
        ISerialiazer Serialiazer { get; set; }

        /// <summary>
        /// Gets or sets the base url for the request
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Sets the content of the request
        /// </summary>
        /// <param name="content">The content of the request</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestRequest"/></returns>
        IRestRequest WithContent(object content);

        /// <summary>
        /// Sets the enconding of the request
        /// </summary>
        /// <param name="encoding"><see cref="Encoding"/> of the request</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestRequest"/></returns>
        IRestRequest WithContentEnconding(Encoding encoding);

        /// <summary>
        /// Sets the media type of the request
        /// </summary>
        /// <param name="mediaType">String with the media type</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestRequest"/></returns>
        IRestRequest WithContentMediaType(string mediaType);

        /// <summary>
        /// Sets the <see cref="HttpMethod"/> of the request
        /// </summary>
        /// <param name="method">An instance of<see cref="HttpMethod"/></param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestRequest"/></returns>
        IRestRequest WithHttpMethod(HttpMethod method);

        /// <summary>
        /// Adds a parameter to the url of the request
        /// </summary>
        /// <param name="parameterKey">String with the parameter name</param>
        /// <param name="parameterValue">String with the parameter value</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestRequest"/></returns>
        IRestRequest AddParameter(string parameterKey, string parameterValue);

        /// <summary>
        /// Adds a header ont the request
        /// </summary>
        /// <param name="headerKey">String with the header name</param>
        /// <param name="headerValue">String with the header value</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestRequest"/></returns>
        IRestRequest AddHeader(string headerKey, string headerValue);

        /// <summary>
        /// Adds a header ont the request
        /// </summary>
        /// <param name="formUrlKey">String with the header name</param>
        /// <param name="formUrlValue">String with the header value</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestRequest"/></returns>
        IRestRequest AddFormUrl(string formUrlKey, string formUrlValue);

        /// <summary>
        /// Adds a header ont the request
        /// </summary>
        /// <param name="formDataKey">String with the header name</param>
        /// <param name="formDataValue">String with the header value</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestRequest"/></returns>
        IRestRequest AddFormData(string formDataKey, object formDataValue);

        /// <summary>
        /// Builds asynchronously a <see cref="HttpRequestMessage"/> based on this <see cref="IRestRequest"/>
        /// </summary>
        /// <returns>The <see cref="HttpRequestMessage"/> built</returns>
        Task<HttpRequestMessage> GetHttpRequestMessageAsync();
    }
}