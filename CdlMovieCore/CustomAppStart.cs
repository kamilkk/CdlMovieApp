using System;
using CdlMovieCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CdlMovieCore
{
    /// <summary>
    /// This class is used to customize how the application starts
    /// and which view is loaded on start.
    /// </summary>    
    internal class CustomAppStart : MvxNavigatingObject, IMvxAppStart
    {
        /// <summary>
        /// Hint can take command-line startup parameters, and then pass them to the called view models.
        /// </summary>
        public void Start(object hint = null)
        {
            ShowViewModel<MainViewModel>();
        }
    }
}
