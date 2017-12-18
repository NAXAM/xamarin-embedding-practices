
using Foundation;
using UIKit;

namespace Embedding.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            PreserveCustomControls();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        void PreserveCustomControls()
        {
            FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
            CarouselView.FormsPlugin.iOS.CarouselViewRenderer.Init();
        }
    }
}
