using Foundation;
using UIKit;

using MvvmCross.iOS.Views;

using CdlMovieCore.ViewModels;
using System;
using MBProgressHUD;

namespace CdlMovieApp.Views
{
    /// <summary>
    /// A base view controller 
    /// </summary>
    public class BaseViewController<TViewModel> : MvxViewController where TViewModel : BaseViewModel
    {
        protected MTMBProgressHUD HudView;

        protected bool NavigationBarEnabled = false;

        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected BaseViewController(string nibName, NSBundle bundle)
            : base(nibName, bundle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidLoad()
        {
            EdgesForExtendedLayout = UIRectEdge.None;
            View.BackgroundColor = UIColor.White;

            base.ViewDidLoad();
            LoadSubViews();
            ViewModel.StartLoading += VmStartLoading;
            ViewModel.StopLoading += VmStopLoading;
        }

        public override void ViewDidUnload()
        {
            base.ViewDidUnload();

            if (ViewModel != null)
            {
                ViewModel.StartLoading -= VmStartLoading;
                ViewModel.StopLoading -= VmStopLoading;
            }
        }

        protected virtual void LoadSubViews()
        {
            HudView = new MTMBProgressHUD(View);
            View.AddSubview(HudView);
        }

        protected virtual void VmStartLoading(object sender, System.EventArgs e)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() => HudView.Show(true));
        }

        protected virtual void VmStopLoading(object sender, LoadingResult e)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                HudView.Hide(true);
                if (!e.Success)
                {
                    ShowAlert(e.ErrorMessage, "Error", "OK");
                }
            });
        }

        protected void ShowAlert(string message, string title, string okbtnText, Action completion = null)
        {
            var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create(okbtnText, UIAlertActionStyle.Default, null));
            PresentViewController(alertController, true, completion);
        }
    }
}
