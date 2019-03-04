using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

using CdlMovieCore.Rest;
using CdlMovieCore.Api;

namespace CdlMovieCore
{
    /// <summary>
    /// Main App class inherits from MvxApplication
    /// - Registers the interfaces and implementations the app uses.
    /// - Registers which ViewModel the App will show when it starts.
    /// - Controls how ViewModels are locate.
    /// </summary>
    public class App : MvxApplication
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CdlMovieCore.App"/> class.
        /// Setup IoC registrations.
        /// </summary>
        public override void Initialize()
        {
            Mvx.RegisterType<IRestClient, RestClient>();
            Mvx.RegisterType<IMoviesCalls, MoviesCalls>();

            // Construct custom application start object
            Mvx.ConstructAndRegisterSingleton<IMvxAppStart, CustomAppStart>();

            // request a reference to the constructed appstart object 
            var appStart = Mvx.Resolve<IMvxAppStart>();

			// register the appstart object
			RegisterAppStart(appStart);
        }
    }
}
