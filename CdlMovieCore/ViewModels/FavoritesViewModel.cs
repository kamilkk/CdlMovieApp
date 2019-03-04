using System.Collections.Generic;
using System.Threading.Tasks;

using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

using CdlMovieCore.Models;

namespace CdlMovieCore.ViewModels
{
    public class FavoritesViewModel : ListBaseViewModel
    {
        private readonly Repository repository;

        public FavoritesViewModel()
        {
            repository = Mvx.Resolve<Repository>();
        }

        public override void LoadData()
        {
            OnStartLoading();
            Task.Run(async () =>
            {
                repository.ResetStatus();
                var favMovies = await repository.GetAllMovies();
                if (favMovies != null)
                {
                    Movies = new List<Movie>(favMovies);
                }
                var result = new LoadingResult { Success = string.IsNullOrWhiteSpace(repository.StatusMessage), ErrorMessage = repository.StatusMessage };
                OnStopLoading(result);
            });
        }
    }
}
