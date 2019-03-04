using UIKit;

using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;

using CdlMovieCore.ViewModels;

namespace CdlMovieApp.Views
{
    public partial class MovieDetailsViewController : BaseViewController<MovieDetailsViewModel>
    {
        private MvxImageViewLoader imageViewLoader;

        public MovieDetailsViewController()
            : base("MovieDetailsViewController", null)
        {
            imageViewLoader = new MvxImageViewLoader(() => this.PosterImage);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ViewModel.FavoriteChanged += VmFavoriteChanged;

            var set = this.CreateBindingSet<MovieDetailsViewController, MovieDetailsViewModel>();

            set.Bind(imageViewLoader).To(vm => vm.MovieItem.PosterUrl);

            set.Bind(TitleLabel).To(vm => vm.MovieItem.Title);

            set.Bind(YearLabel).To(vm => vm.MovieItem.ReleaseDate.Year);
            set.Bind(VotesLabel).To(vm => vm.MovieItem.VoteCount);
            set.Bind(PopularityLabel).To(vm => vm.MovieItem.Popularity);

            set.Bind(OverviewLabel).To(vm => vm.MovieItem.Overview);

            set.Bind(FavoritesButton).To(vm => vm.FavoriteCommand);

            set.Apply();
        }

        public override void ViewDidUnload()
        {
            base.ViewDidUnload();

            if (ViewModel != null)
            {
                ViewModel.FavoriteChanged -= VmFavoriteChanged;
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ViewModel.CheckFavorite();
        }

        private void VmFavoriteChanged(object sender, bool isFavorite)
        {
            if (isFavorite)
            {
                UIApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    FavoritesButton.SetImage(UIImage.FromBundle("ic_favorite_in"), UIControlState.Normal);
                });
            }
            else
            {
                UIApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    FavoritesButton.SetImage(UIImage.FromBundle("ic_favorite"), UIControlState.Normal);
                });
            }
        }
    }
}

