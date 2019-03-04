using UIKit;

using CdlMovieApp.Extensions;
using CdlMovieCore.Interfaces;

namespace CdlMovieApp.Services
{
    public class DialogService : IDialogService
    {
        public void Alert(string message, string title, string okbtnText)
        {
            var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create(okbtnText, UIAlertActionStyle.Default, null));
            alertController.ShowAlert();
        }
    }
}
