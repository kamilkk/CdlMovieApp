using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CdlMovieCore.Rest.Deserialization;
using CdlMovieCore.Rest.Serialization;

namespace CdlMovieCore.Rest
{
    /// <summary>
    /// Represents the default implementation of <see cref="IRestRequest"/>
    /// </summary>  
    public class RestClient : IRestClient
    {
        private string baseUrlAddress;

        private CookieContainer cookieContainer;

        private IDictionary<string, string> defaultHeaders;

        /// <summary>
        /// Gets the <see cref="ISerialiazer"/> implementation for this request
        /// </summary>
        public ISerialiazer Serializer { get; private set; }

        /// <summary>
        /// Gets the <see cref="IDeserialiazer"/> implementation for this request
        /// </summary>
        public IDeserialiazer Deserialiazer { get; private set; }

        /// <summary>
        /// Gets the <see cref="IRestClientOptions"/> 
        /// </summary>
        public IRestClientOptions RestionClientOptions { get; private set; }

        /// <summary>
        /// Gets the internal <see cref="HttpClient"/>
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        /// <summary>
        /// Gets the defaults headers that will be sent on every request
        /// </summary>
        public IDictionary<string, string> DefaultHeaders
        {
            get { return defaultHeaders ?? (defaultHeaders = new Dictionary<string, string>()); }
        }

        /// <summary>
        /// Default constructor for RestionClient with JsonNet and inicialization of Dictionaries
        /// </summary>
        public RestClient()
            : this(new JsonNetSerializer(), new JsonNetDeserializer())
        {
        }

        /// <summary>
        /// Default constructor for RestionClient
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="deSerialiazer"></param>
        public RestClient(ISerialiazer serializer, IDeserialiazer deSerialiazer)
        {
            this.Serializer = serializer;
            this.Deserialiazer = deSerialiazer;
        }

        /// <summary>
        /// Default constructor for RestionClient with JsonNet and inicialization of Dictionaries
        /// </summary>
        public RestClient(string baseUrl)
            : this(new JsonNetSerializer(), new JsonNetDeserializer())
        {
            this.baseUrlAddress = baseUrl;
            this.HttpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrlAddress)
            };
        }

        /// <summary>
        /// Sets the <see cref="CookieContainer"/> 
        /// </summary>
        /// <param name="cookieContainer">The instance of CookieContainer</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestClient"/></returns>
        public IRestClient SetCookieContainer(CookieContainer cookieContainer)
        {
            if (cookieContainer == null)
            {
                throw new ArgumentNullException(nameof(cookieContainer));
            }
            this.cookieContainer = cookieContainer;
            this.HttpClient = new HttpClient(new HttpClientHandler { CookieContainer = this.cookieContainer })
            {
                BaseAddress = new Uri(baseUrlAddress)
            };

            return this;
        }

        /// <summary>
        /// Sets the base address 
        /// </summary>
        /// <param name="baseAddress">String with the base address</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestClient"/></returns>
        public IRestClient SetBaseAddress(string baseAddress)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new ArgumentNullException(nameof(baseAddress));
            }
            this.baseUrlAddress = baseAddress;
            this.HttpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrlAddress)
            };

            return this;
        }

        /// <summary>
        /// Sets the <see cref="ISerialiazer"/>
        /// </summary>
        /// <param name="serialiazer">Implementation of <see cref="ISerialiazer"/></param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestClient"/></returns>
        public IRestClient SetSerializer(ISerialiazer serialiazer)
        {
            this.Serializer = serialiazer;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="IDeserialiazer"/>
        /// </summary>
        /// <param name="deserialiazer">Implementation of <see cref="IDeserialiazer"/></param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestClient"/></returns>
        public IRestClient SetDeserializer(IDeserialiazer deserialiazer)
        {
            this.Deserialiazer = deserialiazer;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="IRestClientOptions"/>
        /// </summary>
        /// <param name="restionClientOptions">The restion client options</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestClient"/></returns>
        public IRestClient SetRestionClientOptions(IRestClientOptions restionClientOptions)
        {
            this.RestionClientOptions = restionClientOptions;
            return this;
        }

        /// <summary>
        /// Adds a default header that will be sent on every request
        /// </summary>
        /// <param name="headerKey"><see cref="string"/> with the header key</param>
        /// <param name="headerValue"><see cref="string"/> with the header value</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestClient"/></returns>
        public IRestClient AddDefaultHeader(string headerKey, string headerValue)
        {
            this.defaultHeaders.Add(headerKey, headerValue);
            return this;
        }

        /// <summary>
        /// Sets the Authorization header
        /// </summary>
        /// <param name="value">The value of the auth header e.g. a Access Token or a full scheme like : "Bearer :ACCESS_TOKEN" </param>
        /// <param name="type">The type of the auth header e.g. "Bearer" </param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestClient"/></returns>
        public IRestClient SetAuthorizationHeader(string value, string type = null)
        {
            HttpClient.DefaultRequestHeaders.Authorization =
                string.IsNullOrWhiteSpace(type) ? new AuthenticationHeaderValue(value) :
                new AuthenticationHeaderValue(type, value);
            return this;
        }

        /// <summary>
        /// Gets the default request, that is deafult implementation of <see cref="IRestRequest"/> interface.
        /// </summary>
        /// <returns>The default request.</returns>
        /// <param name="requestPath">Request relative path.</param>
        public IRestRequest GetDefaultRequest(string requestPath)
        {
            return new RestRequest(requestPath);
        }

        /// <summary>
        /// Execute asynchronously a <see cref="IRestRequest"/> 
        /// </summary>
        /// <typeparam name="TResponseContent">Content of the response</typeparam>
        /// <param name="restionRequest">The RestionRequest to be sent</param>
        /// <returns><see cref="IRestResponse{TResponseContent}"/> of the request</returns>
        public async Task<IRestResponse<TResponseContent>> ExecuteRequestAsync<TResponseContent>(IRestRequest restionRequest) where TResponseContent : class
        {
            if (restionRequest == null)
            {
                throw new ArgumentNullException(nameof(restionRequest));
            }

            if (this.Serializer == null)
            {
                throw new Exception("Serializer is not defined");
            }

            if (this.Deserialiazer == null)
            {
                throw new Exception("DeSerializer is not defined");
            }

            var restionResponse = new RestResponse<TResponseContent>();

            try
            {
                if (RestionClientOptions != null && !string.IsNullOrWhiteSpace(RestionClientOptions.DateFormat))
                {
                    Serializer.DateFormat = RestionClientOptions.DateFormat;
                    Deserialiazer.DateFormat = RestionClientOptions.DateFormat;
                }

                restionRequest.Serialiazer = Serializer;

                restionRequest.BaseUrl = baseUrlAddress;

                foreach (var defaultHeader in DefaultHeaders)
                {
                    restionRequest.AddHeader(defaultHeader.Key, defaultHeader.Value);
                }

                //Gets the HttpRequestMessage from RestionRequest
                var httpRequestMessage = await restionRequest.GetHttpRequestMessageAsync();

                //Sends HttpRequestMessage
                restionResponse.HttpResponseMessage = await HttpClient.SendAsync(httpRequestMessage);

                //Mount the restion response
                var rawContent = await restionResponse.HttpResponseMessage.Content.ReadAsStringAsync();

                //Deserializes the content
                restionResponse.Content = await Deserialiazer.DeserializeAsync<TResponseContent>(rawContent);

                //Set restion client options
                if (RestionClientOptions != null && RestionClientOptions.AllowRawContent)
                {
                    restionResponse.RawContent = rawContent;
                }
            }
            catch (Exception ex)
            {
                restionResponse.Exception = ex;
            }
            finally
            {
                restionRequest.Dispose();
            }

            return restionResponse;
        }

        public async Task<IRestResponse> ExecuteRequestAsync(IRestRequest restionRequest)
        {
            if (restionRequest == null)
            {
                throw new ArgumentNullException(nameof(restionRequest));
            }

            if (this.Serializer == null)
            {
                throw new Exception("Serializer is not defined");
            }

            if (this.Deserialiazer == null)
            {
                throw new Exception("DeSerializer is not defined");
            }

            var restionResponse = new RestResponse();

            try
            {
                if (RestionClientOptions != null && !string.IsNullOrWhiteSpace(RestionClientOptions.DateFormat))
                {
                    Serializer.DateFormat = RestionClientOptions.DateFormat;
                    Deserialiazer.DateFormat = RestionClientOptions.DateFormat;
                }

                restionRequest.Serialiazer = Serializer;

                restionRequest.BaseUrl = baseUrlAddress;

                foreach (var defaultHeader in DefaultHeaders)
                {
                    restionRequest.AddHeader(defaultHeader.Key, defaultHeader.Value);
                }

                //Gets the HttpRequestMessage from RestionRequest
                var httpRequestMessage = await restionRequest.GetHttpRequestMessageAsync();

                //Sends HttpRequestMessage
                restionResponse.HttpResponseMessage = await HttpClient.SendAsync(httpRequestMessage);

                //Mount the restion response
                restionResponse.RawContent = await restionResponse.HttpResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                restionResponse.Exception = ex;
            }
            finally
            {
                restionRequest.Dispose();
            }
            return restionResponse;
        }
    }
}