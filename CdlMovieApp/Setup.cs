using UIKit;

using MvvmCross.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform.Platform;

using CdlMovieApp.Interfaces;
using CdlMovieApp.Presenter;
using CdlMovieApp.Utils;
using CdlMovieCore.Interfaces;
using CdlMovieApp.Services;
using CdlMovieCore;

namespace CdlMovieApp
{
    public class Setup : MvxIosSetup
    {
        /// <summary>Initializes a new instance of the <see cref="Setup"/> class.</summary>
        /// <param name="applicationDelegate">The application delegate.</param>
        /// <param name="window">The window.</param>
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
                    : base(applicationDelegate, window)
        {
        }

        /// <summary>Creates the application.</summary>
        /// <returns>The IMvxApplication <see langword="object"/></returns>
        protected override IMvxApplication CreateApp()
        {
            var dbConnection = FileAccessHelper.GetLocalFilePath("movies.db3");
            Mvx.RegisterSingleton(new Repository(dbConnection));
            
            return new CdlMovieCore.App();
        }

        /// <summary>Creates the debug trace.</summary>
        /// <returns>The IMvxTrace <see langword="object"/></returns>
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IMvxIosViewPresenter CreatePresenter()
        {
            var mvxIosViewPresenter = new MvxTabPresenter((MvxApplicationDelegate)ApplicationDelegate, Window);
            Mvx.RegisterSingleton<ITabBarPresenterHost>(mvxIosViewPresenter);
            return mvxIosViewPresenter;
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterSingleton<IDialogService>(() => new DialogService());
        }
    }
}
