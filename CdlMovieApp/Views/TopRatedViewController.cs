using UIKit;

using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;

using CdlMovieCore.ViewModels;

namespace CdlMovieApp.Views
{
    public partial class TopRatedViewController : BaseViewController<TopRatedViewModel>
    {
        public TopRatedViewController()
            : base("TopRatedViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new MvxSimpleTableViewSource(tableView, MovieCell.Key, MovieCell.Key);

            var set = this.CreateBindingSet<TopRatedViewController, TopRatedViewModel>();

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

