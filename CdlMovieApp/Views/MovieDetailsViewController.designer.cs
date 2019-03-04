// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace CdlMovieApp.Views
{
    [Register ("MovieDetailsViewController")]
    partial class MovieDetailsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton FavoritesButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel OverviewLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel PopularityLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView PosterImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TitleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel VotesLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel YearLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FavoritesButton != null) {
                FavoritesButton.Dispose ();
                FavoritesButton = null;
            }

            if (OverviewLabel != null) {
                OverviewLabel.Dispose ();
                OverviewLabel = null;
            }

            if (PopularityLabel != null) {
                PopularityLabel.Dispose ();
                PopularityLabel = null;
            }

            if (PosterImage != null) {
                PosterImage.Dispose ();
                PosterImage = null;
            }

            if (TitleLabel != null) {
                TitleLabel.Dispose ();
                TitleLabel = null;
            }

            if (VotesLabel != null) {
                VotesLabel.Dispose ();
                VotesLabel = null;
            }

            if (YearLabel != null) {
                YearLabel.Dispose ();
                YearLabel = null;
            }
        }
    }
}