using System.Collections.Generic;
using System.Threading.Tasks;

using CdlMovieCore.Models;

namespace CdlMovieCore.Api
{
    /// <summary>
    /// The interface for JSON based API calls for the movies.
    /// </summary>
    public interface IMoviesCalls
    {
        /// <summary>
        /// Gets the top page of the most popular now playing movies.
        /// </summary>
        /// <returns>The list of now playing <see cref="Movie"/>s.</returns>
        Task<List<Movie>> NowPlaying();

        /// <summary>
        /// Gets the top page of the most popular top rated movies.
        /// </summary>
        /// <returns>The list of top rated <see cref="Movie"/>s.</returns>
        Task<List<Movie>> TopRated();

        /// <summary>
        /// Gets the top page of the most popular movies.
        /// </summary>
        /// <returns>The list of now playing <see cref="Movie"/>s.</returns>
        Task<List<Movie>> Popular();
    }
}
