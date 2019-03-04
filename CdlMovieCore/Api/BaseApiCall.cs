using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CdlMovieCore.Rest;

namespace CdlMovieCore.Api
{
    /// <summary>
    /// Base class for the JSON based API calls.
    /// </summary>
    public abstract class BaseApiCall
    {
        /// <summary>
        /// The rest client instance.
        /// </summary>
        private IRestClient restClient;

        /// <summary>
        /// Gets or sets the API base address.
        /// </summary>
        /// <value>The API base address.</value>
        private string ApiBaseAddress { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CdlMovieCore.Api.BaseApiCall"/> class.
        /// </summary>
        /// <param name="restClient">Rest client instance.</param>
        public BaseApiCall(IRestClient restClient)
        {
            this.restClient = restClient;
            SetBaseApiAddress();
        }

        /// <summary>
        /// Sets the base API address.
        /// </summary>
        /// <param name="apiAddress">API address.</param>
        protected void SetBaseApiAddress(string apiAddress = null)
        {
            ApiBaseAddress = string.IsNullOrEmpty(apiAddress) ? Consts.RestApiBaseAddress : ApiBaseAddress = apiAddress;
        }

        /// <summary>
        /// Perform GET request in the async manner with serialised response.
        /// </summary>
        /// <returns>The serialized request response.</returns>
        /// <param name="relativePath">Relative path of the request (will be merged with base address).</param>
        /// <typeparam name="TResponseContent">Type used for response serialisation.</typeparam>
        protected async Task<IRestResponse<TResponseContent>> GetAsync<TResponseContent>(string relativePath) where TResponseContent : class
        {
            IRestRequest restRequest = PrepareRequest(relativePath);
            var response = await restClient.ExecuteRequestAsync<TResponseContent>(restRequest);

            if (response.HttpStatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException(response.HttpResponseMessage.ToString());
            }
            return response;
        }

        /// <summary>
        /// Perform GET request int the async manner.
        /// </summary>
        /// <param name="relativePath">Relative path of the request (will be merged with base address).</param>
        /// <param name="relativePath">Relative path.</param>
        protected async Task<IRestResponse> GetAsync(string relativePath)
        {
            IRestRequest restRequest = PrepareRequest(relativePath);
            var response = await restClient.ExecuteRequestAsync(restRequest);

            if (response.HttpStatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException(response.HttpResponseMessage.ToString());
            }
            return response;
        }

        /// <summary>
        /// Perform POST request in the async manner with serialised response.
        /// </summary>
        /// <returns>The serialized request response.</returns>
        /// <param name="relativePath">Relative path of the request (will be merged with base address).</param>
        /// <param name="content">The POST request content object.</param>
        /// <typeparam name="TResponseContent">Type used for response serialisation.</typeparam>
        protected async Task<IRestResponse<TResponseContent>> PostAsync<TResponseContent>(string relativePath, object content) where TResponseContent : class
        {
            IRestRequest request = PrepareRequest(relativePath)
                                     .WithHttpMethod(HttpMethod.Post)
                                     .WithContent(content)
                                     .WithContentMediaType(MediaTypes.ApplicationJson)
                                     .WithContentEnconding(Encoding.UTF8);

            IRestResponse<TResponseContent> response = await this.restClient.ExecuteRequestAsync<TResponseContent>(request);
            if (response.HttpStatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException(response.HttpResponseMessage.ToString());
            }
            return response;
        }

        /// <summary>
        /// Perform POST request in the async manner.
        /// </summary>
        /// <returns>The request response.</returns>
        /// <param name="relativePath">Relative path of the request (will be merged with base address).</param>
        /// <param name="content">The POST request content object.</param>
        protected async Task<IRestResponse> PostAsync(string relativePath, object content)
        {
            return await RequestAsync(relativePath, content, HttpMethod.Post);
        }

        /// <summary>
        /// Perform request in the async manner.
        /// </summary>
        /// <returns>The request response.</returns>
        /// <param name="relativePath">Relative path of the request (will be merged with base address).</param>
        /// <param name="content">The POST request content object.</param>
        /// <param name="method">The request <see cref="HttpMethod"/>.</param>
        protected async Task<IRestResponse> RequestAsync(string relativePath, object content, HttpMethod method)
        {
            IRestRequest request = PrepareRequest(relativePath)
                                      .WithHttpMethod(method)
                                     .WithContent(content)
                                     .WithContentMediaType(MediaTypes.ApplicationJson)
                                     .WithContentEnconding(Encoding.UTF8);

            IRestResponse response = await this.restClient.ExecuteRequestAsync(request);

            if (response.HttpStatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException(response.HttpResponseMessage.ToString());
            }
            return response;
        }

        /// <summary>
        /// Perform DELETE request in the async manner with serialised response.
        /// </summary>
        /// <returns>The serialized request response.</returns>
        /// <param name="relativePath">Relative path of the request (will be merged with base address).</param>
        /// <typeparam name="TResponseContent">Type used for response serialisation.</typeparam>
        protected async Task<IRestResponse<TResponseContent>> DeleteAsync<TResponseContent>(string relativePath) where TResponseContent : class
        {
            IRestRequest request = PrepareRequest(relativePath)
                                        .WithHttpMethod(HttpMethod.Delete);

            IRestResponse<TResponseContent> response = await this.restClient.ExecuteRequestAsync<TResponseContent>(request);

            if (response.HttpStatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException(response.HttpResponseMessage.ToString());
            }
            return response;
        }

        /// <summary>
        /// Prepares the request object.
        /// </summary>
        /// <returns>The request object.</returns>
        /// <param name="relativePath">Relative path of the request.</param>
        private IRestRequest PrepareRequest(string relativePath)
        {
            this.restClient.SetBaseAddress(ApiBaseAddress);
            IRestRequest restRequest = this.restClient.GetDefaultRequest(relativePath);
            return restRequest;
        }
    }
}
