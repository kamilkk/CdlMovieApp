using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CdlMovieCore.Api;
using CdlMovieCore.Models;

namespace CdlMovieCore.ViewModels
{
    public class PopularViewModel : ListBaseViewModel
    {
        private readonly IMoviesCalls movieCalls;

        public PopularViewModel(IMoviesCalls moviesCalls)
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
                var popMovies = await movieCalls.Popular();
                if (popMovies != null)
                {
                    Movies = new List<Movie>(popMovies);
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
