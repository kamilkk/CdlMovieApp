using System.Linq;

using UIKit;

namespace CdlMovieApp.iOSPlatform
{
    public static class Application
    {
        public static UIViewController TopViewController
        {
            get
            {
                var viewController = UIApplication.SharedApplication.Delegate.GetWindow().RootViewController;
                var navController = viewController as UINavigationController;
                return navController != null ? navController.ViewControllers.LastOrDefault() : viewController;
            }
        }
    }
}
