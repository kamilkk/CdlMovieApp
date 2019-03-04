using System;
using UIKit;

namespace CdlMovieApp.Extensions
{
    public static class UIAlertControllerExtension
    {
        public static void ShowAlert(this UIAlertController controller)
        {
            controller.Present(true, null);
        }

        public static void Present(this UIAlertController controller, bool animated, Action completion)
        {
            var topController = iOSPlatform.Application.TopViewController;
            PresentFromController(topController, controller, animated, completion);
        }

        private static void PresentFromController(UIViewController presenter, UIAlertController alertController, bool animated, Action completion)
        {
            var navController = presenter as UINavigationController;
            if ((navController != null) && (navController.VisibleViewController != null))
            {
                PresentFromController(navController.VisibleViewController, alertController, animated, completion);
            }
            else if ((presenter as UITabBarController) != null && ((UITabBarController)presenter).SelectedViewController != null)
            {
                PresentFromController(((UITabBarController)presenter).SelectedViewController, alertController, animated, completion);
            }
            else
            {
                presenter.PresentViewController(alertController, animated, completion);     
            }    
        }
    }
}
