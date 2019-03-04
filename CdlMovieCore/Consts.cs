namespace CdlMovieCore
{
    /// <summary>
    /// Basic consts values for the core and application layers
    /// </summary>
    public static class Consts
    {
        /// <summary>
        /// The Movie DB API key
        /// </summary>
        public const string ApiKey = "please add your own the Movie DB key here";

        /// <summary>
        /// The Movie DB base API URI address
        /// </summary>
        public const string RestApiBaseAddress = "http://api.themoviedb.org/3/movie";

        public const string ThumbPosterBaseAddress = "https://image.tmdb.org/t/p/w342{0}";

        public const string PosterBaseAddress = "https://image.tmdb.org/t/p/w500{0}";
    }
}
