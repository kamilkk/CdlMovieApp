using System;
using System.Net.Http;
using System.Threading.Tasks;

using CdlMovieCore.Rest;
using CdlMovieCore.UnitTests.Rest.Model;

using Xunit;

namespace CdlMovieCore.UnitTests.Rest
{
	public class DeleteRequestTests
	{
		[Fact]
		public async Task ShouldDeleteAPost()
		{
			try
			{
				IRestClient restionClient = new RestClient().SetBaseAddress("http://jsonplaceholder.typicode.com");

				IRestRequest restionRequest = new RestRequest("/posts/1").WithHttpMethod(HttpMethod.Delete);

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
