using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using CdlMovieCore.Extensions;
using CdlMovieCore.Rest.Serialization;

namespace CdlMovieCore.Rest
{
    /// <summary>
    /// Represents a default the implementation of <see cref="IRestClient"/> 
    /// </summary>
    public class RestRequest : IRestRequest
    {
        /// <summary>
        /// Content of the request
        /// </summary>
        private object content;

        /// <summary>
        /// Dictionary of parameters
        /// </summary>
        private IDictionary<string, string> parameters;

        /// <summary>
        /// Dictionary of form url encoded
        /// </summary>
        private IDictionary<string, string> formUrlEncoded;

        /// <summary>
        /// Dictionary of form url encoded
        /// </summary>
        private IList<Tuple<string, object>> formData;

        /// <summary>
        /// Dictionary of headers
        /// </summary>
        private IDictionary<string, string> headers;

        /// <summary>
        /// Url of the request
        /// </summary>
        private string url;

        /// <summary>
        /// Enconding of the request
        /// </summary>
        private Encoding encoding;

        /// <summary>
        /// Media-Type for the request
        /// </summary>
        private string mediaType;

        /// <summary>
        /// Gets the HttpMethod for the http request.
        /// </summary>
        public HttpMethod Method { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="ISerialiazer"/> implementation for this request
        /// </summary>
        public ISerialiazer Serialiazer { get; set; }

        /// <summary>
        /// Gets or sets the base url for the request
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets the parameters
        /// </summary>
        private IDictionary<string, string> Parameters
        {
            get { return parameters ?? (parameters = new Dictionary<string, string>()); }
        }

        /// <summary>
        /// Gets the headers
        /// </summary>
        private IDictionary<string, string> Headers
        {
            get { return headers ?? (headers = new Dictionary<string, string>()); }
        }

        /// <summary>
        /// Dictionary of form url encoded parameters
        /// </summary>
        public IDictionary<string, string> FormUrlEncoded
        {
            get { return formUrlEncoded ?? (formUrlEncoded = new Dictionary<string, string>()); }
        }

        /// <summary>
        /// Dictionary of form data
        /// </summary>
        public IList<Tuple<string, object>> FormData
        {
            get { return formData ?? (formData = new List<Tuple<string, object>>()); }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RestRequest"/> class
        /// </summary>
        public RestRequest()
        {
            //Sets default httpMethod
            this.Method = HttpMethod.Get;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RestRequest"/> class using a base url
        /// </summary>
        /// <param name="url">Base url of the request</param>
        public RestRequest(string url) : this()
        {
            this.url = url;
        }

        /// <summary>
        /// Sets the content of the request
        /// </summary>
        /// <param name="content">The content of the request</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestRequest"/></returns>
        public IRestRequest WithContent(object content)
        {
            this.content = content;
            return this;
        }

        /// <summary>
        /// Sets the enconding of the request
        /// </summary>
        /// <param name="encoding"><see cref="Encoding"/> of the request</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestRequest"/></returns>
        public IRestRequest WithContentEnconding(Encoding encoding)
        {
            this.encoding = encoding;
            return this;
        }

        /// <summary>
        /// Sets the media type of the request
        /// </summary>
        /// <param name="mediaType">String with the media type</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestRequest"/></returns>
        public IRestRequest WithContentMediaType(string mediaType)
        {
            this.mediaType = mediaType;
            return this;
        }

        /// <summary>
        /// Adds a parameter to the url of the request
        /// </summary>
        /// <param name="parameterKey">String with the parameter name</param>
        /// <param name="parameterValue">String with the parameter value</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestRequest"/></returns>
        public IRestRequest AddParameter(string parameterKey, string parameterValue)
        {
            if (string.IsNullOrWhiteSpace(parameterKey))
            {
                throw new ArgumentNullException(nameof(parameterKey));
            }

            if (string.IsNullOrWhiteSpace(parameterValue))
            {
                throw new ArgumentNullException(nameof(parameterValue));
            }

            if (Parameters.ContainsKey(parameterKey))
            {
                throw new ArgumentException("There is already a paramater with this key");
            }

            this.Parameters.Add(parameterKey, parameterValue);
            return this;
        }

        /// <summary>
        /// Adds a header in the request
        /// </summary>
        /// <param name="headerKey">String with the header name</param>
        /// <param name="headerValue">String with the header value</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestRequest"/></returns>
        public IRestRequest AddHeader(string headerKey, string headerValue)
        {
            if (string.IsNullOrWhiteSpace(headerKey))
            {
                throw new ArgumentNullException(nameof(headerKey));
            }

            if (string.IsNullOrWhiteSpace(headerValue))
            {
                throw new ArgumentNullException(nameof(headerValue));
            }

            if (Headers.ContainsKey(headerKey))
            {
                throw new ArgumentException("There is already a paramater with this key");
            }
            this.Headers.Add(headerKey, headerValue);
            return this;
        }

        /// <summary>
        /// Adds a form url enconded into the request
        /// </summary>
        /// <param name="formUrlKey">String with the form url key</param>
        /// <param name="formUrlValue">String with the  form url value</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestRequest"/></returns>
        public IRestRequest AddFormUrl(string formUrlKey, string formUrlValue)
        {
            if (string.IsNullOrWhiteSpace(formUrlKey))
            {
                throw new ArgumentNullException(nameof(formUrlKey));
            }

            if (string.IsNullOrWhiteSpace(formUrlValue))
            {
                throw new ArgumentNullException(nameof(formUrlValue));
            }

            if (FormUrlEncoded.ContainsKey(formUrlValue))
            {
                throw new ArgumentException("There is already a paramater with this key");
            }

            this.FormUrlEncoded.Add(formUrlKey, formUrlValue);
            return this;
        }

        /// <summary>
        /// Adds a form data into the request
        /// </summary>
        /// <param name="formDataKey">String with the form data key</param>
        /// <param name="formDataValue">String with the form data value</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestRequest"/></returns>
        public IRestRequest AddFormData(string formDataKey, object formDataValue)
        {
            if (string.IsNullOrWhiteSpace(formDataKey))
            {
                throw new ArgumentNullException(nameof(formDataKey));
            }

            if (formDataValue == null)
            {
                throw new ArgumentNullException(nameof(formDataValue));
            }

            if (FormData.Any(predicate => predicate.Item1 == formDataKey))
            {
                throw new ArgumentException("There is already a paramater with this key");
            }

            this.FormData.Add(new Tuple<string, object>(formDataKey, formDataValue));
            return this;
        }

        /// <summary>
        /// Sets the <see cref="HttpMethod"/> of the request
        /// </summary>
        /// <param name="method">An instance of<see cref="HttpMethod"/></param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestRequest"/></returns>
        public IRestRequest WithHttpMethod(HttpMethod method)
        {
            this.Method = method;
            return this;
        }

        /// <summary>
        /// Builds asynchronously a <see cref="HttpRequestMessage"/> based on this <see cref="IRestRequest"/>
        /// </summary>
        /// <returns>The <see cref="HttpRequestMessage"/> built</returns>
        public async Task<HttpRequestMessage> GetHttpRequestMessageAsync()
        {
            try
            {
                var httpRequestMessage = new HttpRequestMessage { Method = Method };

                foreach (var header in Headers)
                {
                    httpRequestMessage.Headers.Add(header.Key, header.Value);
                }

                var parametersUrl = Parameters.ToQueryString();

                var uri = new Uri(BaseUrl + url + parametersUrl);

                httpRequestMessage.RequestUri = uri;

                //If the request is get method there is no content
                if (Method == HttpMethod.Get)
                    return httpRequestMessage;

                //application/x-www-form-urlencoded
                if (!formUrlEncoded.IsNullOrEmpty())
                {
                    httpRequestMessage.Content = new FormUrlEncodedContent(formUrlEncoded);

                    return httpRequestMessage;
                }

                //Multipart/form-data
                if (!formData.IsNullOrEmpty())
                {
                    var formDataMultipartFormData = new MultipartFormDataContent();

                    foreach (var data in formData)
                    {
                        var bytes = data.Item2 as byte[];
                        if (bytes != null)
                        {
                            formDataMultipartFormData.Add(new ByteArrayContent(bytes), data.Item1);
                        }
                        else if (data.Item2 is Stream)
                        {
                            formDataMultipartFormData.Add(new StreamContent((Stream)data.Item2), data.Item1);
                        }
                        else
                        {
                            formDataMultipartFormData.Add(new StringContent(data.Item2.ToString()), data.Item1);
                        }
                    }

                    httpRequestMessage.Content = formDataMultipartFormData;

                    return httpRequestMessage;
                }

                //Default string content
                var contentSerialized = await Serialiazer.SerializeAsync(content);

                httpRequestMessage.Content = new StringContent(contentSerialized, encoding, mediaType);

                return httpRequestMessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Disposes the <see cref="RestRequest"/>
        /// </summary>
        public void Dispose()
        {
            this.parameters = null;
            this.headers = null;
            this.url = null;
            this.encoding = null;
            this.mediaType = null;
            this.Method = null;
            this.Serialiazer = null;
            this.BaseUrl = null;

            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose for derived classes
        /// </summary>
        /// <param name="disposing">If it is disposing</param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
