using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using CdlMovieCore.Rest;
using CdlMovieCore.UnitTests.Rest.Model;

using Xunit;

namespace CdlMovieCore.UnitTests.Rest
{
	public class PostRequestTests
	{
		[Fact]
		public async Task ShouldPostAPost()
		{
			try
			{
				IRestClient restionClient = new RestClient().SetBaseAddress("http://jsonplaceholder.typicode.com");
				IRestRequest restionRequest = new RestRequest("/posts/")
										 .WithHttpMethod(HttpMethod.Post)
										 .WithContent(new Post()
										 {
											 Body = "Teste",
											 Title = "Teste",
											 UserId = 1
										 })
										 .WithContentMediaType(MediaTypes.ApplicationJson)
										 .WithContentEnconding(Encoding.UTF8);

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

		[Fact]
		public async Task ShouldPutAPost()
		{
			try
			{
				IRestClient restionClient = new RestClient().SetBaseAddress("http://jsonplaceholder.typicode.com");
				IRestRequest restionRequest = new RestRequest("/posts/1")
										 .WithHttpMethod(HttpMethod.Put)
										 .WithContent(new Post()
										 {
											 Id = 1,
											 Body = "TestePut",
											 Title = "TestePut",
											 UserId = 1
										 })
										 .WithContentMediaType(MediaTypes.ApplicationJson)
										 .WithContentEnconding(Encoding.UTF8);

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
