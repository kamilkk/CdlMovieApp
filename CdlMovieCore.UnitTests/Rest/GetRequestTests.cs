using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CdlMovieCore.Rest;
using CdlMovieCore.UnitTests.Rest.Model;

using Xunit;

namespace CdlMovieCore.UnitTests.Rest
{
	public class GetRequestTests
	{
		[Fact]
		public async Task GetShouldReturnAllPosts()
		{
			try
			{
				IRestClient restClient = new RestClient().SetBaseAddress("http://jsonplaceholder.typicode.com");
				IRestRequest restRequest = new RestRequest("/posts/");
				IRestResponse<List<Post>> response = await restClient.ExecuteRequestAsync<List<Post>>(restRequest);

				if (response != null)
				{
					if (response.Exception != null)
					{
						Assert.True(false, response.Exception.Message);
					}
					else
					{
						Assert.True(response.Content != null);
						Assert.True(response.Content.Count == 100);
					}
				}
			}
			catch (Exception ex)
			{
				Assert.True(false, ex.Message);
			}
		}

		[Fact]
		public async Task GetShouldReturnOnePost()
		{
			try
			{
				IRestClient restionClient = new RestClient().SetBaseAddress("http://jsonplaceholder.typicode.com");
				IRestRequest restionRequest = new RestRequest("/posts/1");
				IRestResponse<Post> response = await restionClient.ExecuteRequestAsync<Post>(restionRequest);

				if (response != null)
				{
					if (response.Exception != null)
					{
						Assert.True(false, response.Exception.Message);
					}
					else
					{
						Assert.True(response.Content != null);
					}
				}
			}
			catch (Exception ex)
			{
				Assert.True(false, ex.Message);
			}
		}

	}
}
