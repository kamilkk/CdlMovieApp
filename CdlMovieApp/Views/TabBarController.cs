using UIKit;

using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using MvvmCross.Platform;

using CdlMovieApp.Interfaces;
using CdlMovieCore.ViewModels;

namespace CdlMovieApp.Views
{
    public class TabBarController : MvxTabBarViewController, ITabBarPresenter
    {
        private int createdSoFarCount = 0;

        public TabBarController()
        {
            Mvx.Resolve<ITabBarPresenterHost>().TabBarPresenter = this;

            ViewDidLoad();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController?.SetNavigationBarHidden(true, false);
        }

        public bool ShowView(IMvxIosView view)
        {
            if (TryShowViewInCurrentTab(view))
            {
                return true;
            }
            return false;
        }

        public new MainViewModel ViewModel
        {
            get { return (MainViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public bool GoBack()
        {
            var subNavigation = this.SelectedViewController as UINavigationController;
            if (subNavigation == null)
            {
                return false;
            }

            if (subNavigation.ViewControllers.Length <= 1)
            {
                return false;
            }
            subNavigation.PopViewController(true);
            return true;
        }

        private void SetupView()
        {
            // first time around this will be null, second time it will be OK
            if (ViewModel == null)
            {
                return;
            }

            PopularViewModel popularViewModel = (PopularViewModel)Mvx.IocConstruct(typeof(PopularViewModel));
            TopRatedViewModel topRatedViewModel = (TopRatedViewModel)Mvx.IocConstruct(typeof(TopRatedViewModel));
            NowPlayingViewModel nowPlayingViewModel = (NowPlayingViewModel)Mvx.IocConstruct(typeof(NowPlayingViewModel));
            FavoritesViewModel favoritesViewModel = (FavoritesViewModel)Mvx.IocConstruct(typeof(FavoritesViewModel));
            var viewControllers = new[]
            {
                CreateTabFor("Popular", "ic_movie", popularViewModel),
                CreateTabFor("Top rated", "ic_movie_top", topRatedViewModel),
                CreateTabFor("Now playing", "ic_movie_now", nowPlayingViewModel),
                CreateTabFor("Favorites", "ic_favorite", favoritesViewModel),
            };
            ViewControllers = viewControllers;
            CustomizableViewControllers = new UIViewController[] { };
            SelectedViewController = ViewControllers[0];
        }

        private UIViewController CreateTabFor(string title, string imageName, IMvxViewModel viewModel)
        {
            var controller = new UINavigationController();
            controller.NavigationBar.TintColor = UIColor.Black;
            var screen = this.CreateViewControllerFor(viewModel) as UIViewController;
            SetTitleAndTabBarItem(screen, title, imageName);
            controller.PushViewController(screen, false);
            return controller;
        }

        private void SetTitleAndTabBarItem(UIViewController screen, string title, string imageName)
        {
            screen.Title = title;
            screen.TabBarItem = new UITabBarItem(title, UIImage.FromBundle(imageName), createdSoFarCount);
            createdSoFarCount++;
        }

        private bool TryShowViewInCurrentTab(IMvxIosView view)
        {
            var navigationController = (UINavigationController)this.SelectedViewController;
            navigationController.PushViewController((UIViewController)view, true);
            return true;
        }
    }
}
