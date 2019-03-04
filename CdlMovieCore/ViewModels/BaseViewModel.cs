using System;
using MvvmCross.Core.ViewModels;

namespace CdlMovieCore.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        public event EventHandler StartLoading;

        public event EventHandler<LoadingResult> StopLoading;

        protected BaseViewModel()
        {
        }

        public virtual void LoadData()    
        { 
        }

        protected void OnStartLoading()
        {
            StartLoading?.Invoke(this, new EventArgs());
        }

        protected void OnStopLoading(LoadingResult loadingResult)
        {
            StopLoading?.Invoke(this, loadingResult);
        }
    }
}
