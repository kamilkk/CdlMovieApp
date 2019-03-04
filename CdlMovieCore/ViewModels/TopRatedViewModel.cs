using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CdlMovieCore.Api;
using CdlMovieCore.Models;

namespace CdlMovieCore.ViewModels
{
    public class TopRatedViewModel : ListBaseViewModel
    {
        private readonly IMoviesCalls movieCalls;

        public TopRatedViewModel(IMoviesCalls moviesCalls)
        {
            this.movieCalls = moviesCalls;
        }

        public override void LoadData()
        {
            OnStartLoading();
            Task.Run(async () =>
            {
                var result = await PerformDataLoading();
                OnStopLoading(result);
            });
        }

        private async Task<LoadingResult> PerformDataLoading()
        {
            try
            {
                var topMovies = await movieCalls.TopRated();
                if (topMovies != null)
                {
                    Movies = new List<Movie>(topMovies);
                }
            }
            catch (Exception ex)
            {
                return new LoadingResult { Success = false, ErrorMessage = ex.Message };
            }
            return new LoadingResult { Success = true, ErrorMessage = string.Empty };
        }
    }
}
