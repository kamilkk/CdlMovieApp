using System;

using Newtonsoft.Json;
using SQLite;

namespace CdlMovieCore.Models
{
    /// <summary>
    /// Represents the Movie.
    /// </summary>
    [Table(nameof(Movie))]
    public class Movie
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")]
        [PrimaryKey] 
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the poster path.
        /// </summary>
        /// <value>The poster path.</value>
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }  

        /// <summary>
        /// Gets or sets the poster URL.
        /// </summary>
        /// <value>The poster URL.</value>
        public string PosterUrl { get; set; }

        /// <summary>
        /// Gets or sets the poster thumb URL.
        /// </summary>
        /// <value>The poster thumb URL.</value>
        public string PosterThumbUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:CdlMovieCore.Models.Movie"/> is  for adults.
        /// </summary>
        /// <value><c>true</c> if adult; otherwise, <c>false</c>.</value>
        [JsonProperty("adult")]
        public bool Adult { get; set; }

        /// <summary>
        /// Gets or sets the overview (description) of the movie.
        /// </summary>
        /// <value>The overview.</value>
        [JsonProperty("overview")]
        public string Overview { get; set; }

        /// <summary>
        /// Gets or sets the release date of the movie.
        /// </summary>
        /// <value>The release date.</value>
        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the original title, in the original language.
        /// </summary>
        /// <value>The original title.</value>
        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        /// <summary>
        /// Gets or sets the original language.
        /// </summary>
        /// <value>The original language.</value>
        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the popularity.
        /// </summary>
        /// <value>The popularity.</value>
        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        /// <summary>
        /// Gets or sets the votes count.
        /// </summary>
        /// <value>The vote count.</value>
        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:CdlMovieCore.Models.Movie"/> has video avaialble.
        /// </summary>
        /// <value><c>true</c> if video; otherwise, <c>false</c>.</value>
        [JsonProperty("video")]
        public bool Video { get; set; }

        /// <summary>
        /// Gets or sets the votes average value.
        /// </summary>
        /// <value>The vote average.</value>
        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }
    }
}
