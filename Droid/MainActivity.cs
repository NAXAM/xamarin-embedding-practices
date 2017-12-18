
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Embedding.Droid
{
    [Activity(Label = "Embedding", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            PreserveCustomControls();
            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        void PreserveCustomControls()
        {
            FFImageLoading.Forms.Droid.CachedImageRenderer.Init(true);
            CarouselView.FormsPlugin.Android.CarouselViewRenderer.Init();
        }
    }
}
