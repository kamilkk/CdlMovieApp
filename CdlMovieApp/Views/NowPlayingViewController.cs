using System;
using CdlMovieCore.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CdlMovieApp.Views
{
    public partial class NowPlayingViewController : BaseViewController<NowPlayingViewModel>
    {
        public NowPlayingViewController() : base("NowPlayingViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new MvxSimpleTableViewSource(tableView, MovieCell.Key, MovieCell.Key);

            var set = this.CreateBindingSet<NowPlayingViewController, NowPlayingViewModel>();

            set.Bind(source).To(vm => vm.Movies);
            set.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);

            set.Apply();

            tableView.Source = source;

            ViewModel.LoadData();
        }

        protected override void LoadSubViews()
        {
            base.LoadSubViews();
            tableView.TableFooterView = new UIView();

            HudView.LabelText = "Loading...";
        }
    }
}

