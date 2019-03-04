using UIKit;

using MvvmCross.iOS.Views.Presenters;

using CdlMovieApp.Interfaces;

namespace CdlMovieApp.Presenter
{
    public class MvxTabPresenter : MvxIosViewPresenter, ITabBarPresenterHost
    {
        public ITabBarPresenter TabBarPresenter { get; set; }

        public MvxTabPresenter(IUIApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        protected override UINavigationController CreateNavigationController(UIViewController viewController)
        {
            var toReturn = base.CreateNavigationController(viewController);
            toReturn.NavigationBarHidden = true;
            return toReturn;
        }

        public override void Show(MvvmCross.iOS.Views.IMvxIosView view)
        {
            if (view.Request.PresentationValues != null)
            {
                var values = view.Request.PresentationValues;
                if (values.ContainsKey("NavigationMode") && values["NavigationMode"] == "BackNavigation")
                {
                    MasterNavigationController.SetNavigationBarHidden(false, false);
                }
            }
            
            base.Show(view);
        }
    }
}
