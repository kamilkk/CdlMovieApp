using System;
using CdlMovieCore.Models;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CdlMovieApp.Views
{
    public partial class MovieCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("MovieCell");
        public static readonly UINib Nib = UINib.FromName("MovieCell", NSBundle.MainBundle);

        private readonly MvxImageViewLoader imageViewLoader;

        public MovieCell(IntPtr handle) : base(handle)
        {
            imageViewLoader = new MvxImageViewLoader(() => this.PosterImage);

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<MovieCell, Movie>();
                set.Bind(TitleLabel).To(movie => movie.Title);

                set.Bind(imageViewLoader).To(movie => movie.PosterThumbUrl);
                set.Apply();
            });
        }

        public static MovieCell Create()
        {
            return (MovieCell)Nib.Instantiate(null, null)[0];
        }
    }
}
