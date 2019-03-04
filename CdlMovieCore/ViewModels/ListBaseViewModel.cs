using System.Collections.Generic;

using CdlMovieCore.Models;
using MvvmCross.Core.ViewModels;

namespace CdlMovieCore.ViewModels
{
    public class ListBaseViewModel : BaseViewModel
    {
        private List<Movie> movies;

        public List<Movie> Movies
        {
            get { return movies; }
            set
            {
                movies = value;
                RaisePropertyChanged(() => Movies);
            }
        }

        private MvxCommand<Movie> itemSelectedCommand;

        public System.Windows.Input.ICommand ItemSelectedCommand
        {
            get
            {
                itemSelectedCommand = itemSelectedCommand ?? new MvxCommand<Movie>(DoSelectItem);
                return itemSelectedCommand;
            }
        }

        private void DoSelectItem(Movie item)
        {
            var presentationBundle = new MvxBundle(new Dictionary<string, string> { { "NavigationMode", "BackNavigation" } });
            ShowViewModel<MovieDetailsViewModel>(item, presentationBundle);
        }
    }
}
