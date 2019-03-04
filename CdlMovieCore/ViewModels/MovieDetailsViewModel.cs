using System.Windows.Input;

using MvvmCross.Core.ViewModels;

using CdlMovieCore.Models;
using System.Threading.Tasks;
using System;
using MvvmCross.Platform;

namespace CdlMovieCore.ViewModels
{
    public class MovieDetailsViewModel : BaseViewModel
    {
        private Repository repository;

        public event EventHandler<bool> FavoriteChanged;

        private Movie movieItem;

        public Movie MovieItem
        {
            get { return movieItem; }
            set
            {
                movieItem = value;
                RaisePropertyChanged(() => MovieItem);
            }
        }

        public bool IsFavorite { get; set; }

        public ICommand FavoriteCommand
        {
            get
            {
                return new MvxCommand(async () => await PerformFavoriteAction());
            }
        }

        public void Init(Movie item)
        {
            MovieItem = item;
            repository = Mvx.Resolve<Repository>();
        }

        public void CheckFavorite()
        {
            Task.Run(async () =>
            {
                OnStartLoading();
                var saved = await repository.GetMovie(MovieItem.Id);
                IsFavorite = saved != null;
                FavoriteChanged?.Invoke(this, IsFavorite);

                OnStopLoading(new LoadingResult { Success = true, ErrorMessage = string.Empty });
            });
        }

        private async Task PerformFavoriteAction()
        {
            var result = false;
            OnStartLoading();
            if (IsFavorite)
            {
                result = await repository.DeleteMovie(movieItem.Id);
                if (result)
                {
                    IsFavorite = false;
                }
            }
            else
            {
                result = await repository.AddMovie(movieItem);
                IsFavorite = (result == true);
            }

            FavoriteChanged?.Invoke(this, IsFavorite);
            OnStopLoading(new LoadingResult { Success = result, ErrorMessage = repository.StatusMessage });
        }
    }
}
