using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CdlMovieCore.Extensions;
using CdlMovieCore.Models;
using CdlMovieCore.Rest;

namespace CdlMovieCore.Api
{
    /// <summary>
    /// The JSON based API calls for the movies.
    /// </summary>
    public class MoviesCalls : BaseApiCall, IMoviesCalls
    {
        /// <summary>
        /// The most popular now playing movies API call path.
        /// </summary>
        private const string NowPlayingPath = "/now_playing?api_key={0}&sort_by=popularity.des";

        /// <summary>
        /// The most popular top rated movies API call path.
        /// </summary>
        private const string TopRatedPath = "/top_rated?api_key={0}&sort_by=popularity.des";

        /// <summary>
        /// The most popular movies API call path.
        /// </summary>
        private const string PopularPath = "/popular?api_key={0}&sort_by=popularity.des";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CdlMovieCore.Api.MoviesCalls"/> class.
        /// </summary>
        /// <param name="restClient">Rest client instance.</param>
        public MoviesCalls(IRestClient restClient)
            : base(restClient)
        {
        }

        /// <summary>
        /// Gets the top page of the most popular now playing movies.
        /// </summary>
        /// <returns>The list of now playing <see cref="Movie"/>s.</returns>
        public async Task<List<Movie>> NowPlaying()
        {
            return await MovieRequest(string.Format(NowPlayingPath, Consts.ApiKey));
        }

        /// <summary>
        /// Gets the top page of the most popular now playing movies.
        /// </summary>
        /// <returns>The list of now playing <see cref="Movie"/>s.</returns>
        public async Task<List<Movie>> Popular()
        {
            return await MovieRequest(string.Format(PopularPath, Consts.ApiKey));
        }

        /// <summary>
        /// Gets the top page of the most popular movies.
        /// </summary>
        /// <returns>The list of popular <see cref="Movie"/>s.</returns>
        public async Task<List<Movie>> TopRated()
        {
            return await MovieRequest(string.Format(TopRatedPath, Consts.ApiKey));
        }

        /// <summary>
        /// Movies related the request.
        /// </summary>
        /// <returns>The serialized request result.</returns>
        /// <param name="path">Movie request path.</param>
        private async Task<List<Movie>> MovieRequest(string path)
        {
        	IRestResponse<MoviesResult> response = await GetAsync<MoviesResult>(path);

        	if (response.Exception != null)
        	{
        		throw response.Exception;
        	}

            var movies = response.Content.Results;
            movies.ForEach(x =>
			{
                x.PosterThumbUrl = string.Format(Consts.ThumbPosterBaseAddress, x.PosterPath);
                x.PosterUrl = string.Format(Consts.PosterBaseAddress, x.PosterPath);
	        });
            return movies;
        }
    }
}
