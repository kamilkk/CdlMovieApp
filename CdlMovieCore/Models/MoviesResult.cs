using System.Collections.Generic;
using Newtonsoft.Json;

namespace CdlMovieCore.Models
{
    public class MoviesResult
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("results")]
        public List<Movie> Results { get; set; }
    }
}
