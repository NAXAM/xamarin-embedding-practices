using Android.App;
using Android.Widget;
using Android.OS;
using Embedding;
using Embedding.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.Android;
using Android.Support.V7.App;
using Android.Content.PM;

namespace FormsEmbedding.Droid
{

    [Activity(Label = "Embedding.Droid", Icon = "@mipmap/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : AppCompatActivity, INavigationService
    {
        public Task<bool> GoBackAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> NavigateAsync(string key, IDictionary<string, string> parameters = null)
        {
            var fragment = new SessionsFragment();
            var tx = SupportFragmentManager.BeginTransaction();
            tx.Replace(Resource.Id.pageContainer, fragment);
            tx.Commit();

            return Task.FromResult(true);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            PreserveCustomControls();
            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var page = new EmbeddingPage
            {
                BindingContext = new EmbeddingPageViewModel(this)
            };
            var fragment = page.CreateSupportFragment(this);

            var tx = SupportFragmentManager.BeginTransaction();
            tx.Replace(Resource.Id.pageContainer, fragment);
            tx.Commit();
        }

        void PreserveCustomControls()
        {
            FFImageLoading.Forms.Droid.CachedImageRenderer.Init(true);
            CarouselView.FormsPlugin.Android.CarouselViewRenderer.Init();
        }
    }
}

