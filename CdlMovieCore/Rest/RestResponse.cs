﻿using System;
using System.Net;
using System.Net.Http;

namespace CdlMovieCore.Rest
{
    /// <summary>
    /// Default implementation of <see cref="IRestResponse{TContent}"/>
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    internal class RestResponse<TContent> : IRestResponse<TContent> where TContent : class
    {
        /// <summary>
        /// Gets or sets the <see cref="TContent"/> instance of the response
        /// </summary>
        public TContent Content { get; set; }

        /// <summary>
        /// Get the raw content of the response if allowed through the <see cref="RestClientOptions"/>
        /// </summary>
        public string RawContent { get; set; }

        /// <summary>
        /// Gets the HttpStatusCode of the response
        /// </summary>
        public HttpStatusCode HttpStatusCode
        {
            get
            {
                return HttpResponseMessage != null ? HttpResponseMessage.StatusCode : default(HttpStatusCode);
            }
        }

        /// <summary>
        /// Gets or sets the HttpResponseMessage of the response
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; set; }

        /// <summary>
        /// Gets or sets the exception of the response
        /// </summary>
        public Exception Exception { get; set; }
    }

    /// <summary>
    /// Default implementation of <see cref="IRestResponse{TContent}"/>
    /// </summary>
    internal class RestResponse : IRestResponse
    {
        /// <summary>
        /// Get the raw content of the response
        /// </summary>
        public string RawContent { get; set; }

        /// <summary>
        /// Gets the HttpStatusCode of the response
        /// </summary>
        public HttpStatusCode HttpStatusCode
        {
            get
            {
                return HttpResponseMessage != null ? HttpResponseMessage.StatusCode : default(HttpStatusCode);
            }
        }

        /// <summary>
        /// Gets or sets the HttpResponseMessage of the response
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; set; }

        /// <summary>
        /// Gets or sets the exception of the response
        /// </summary>
        public Exception Exception { get; set; }
    }
}
