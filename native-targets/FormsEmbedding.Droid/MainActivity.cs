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
using Embedding.Models;
using Android.Support.V7.Widget;
using System.Linq;

namespace FormsEmbedding.Droid
{

    [Activity(Label = "Embedding.Droid", Icon = "@mipmap/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : AppCompatActivity, INavigationService
    {
        private Android.Support.V7.Widget.Toolbar toolbar;

        public Task<bool> GoBackAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> NavigateAsync(string key, IDictionary<string, string> parameters = null)
        {
            switch (key)
            {
                case "default":
                    {
                        var page = new EmbeddingPage
                        {
                            BindingContext = new EmbeddingPageViewModel(this)
                        };
                        var fragment = page.CreateSupportFragment(this);

                        var tx = SupportFragmentManager.BeginTransaction();
                        tx.Replace(Resource.Id.pageContainer, fragment);
                        tx.Commit();
                    }
                    return Task.FromResult(true);

                case "end_of_walkthrough":
                    {
                        var fragment = new SessionsFragment();
                        var tx = SupportFragmentManager.BeginTransaction();
                        tx.Replace(Resource.Id.pageContainer, fragment);
                        tx.CommitNow();

                        toolbar.Visibility = Android.Views.ViewStates.Visible;
                        UpdatePageTitle();
                    }
                    return Task.FromResult(true);
                case "view_session_details":
                    {
                        var item = new SessionDetailModel
                        {
                            Title = parameters[nameof(SessionDetailModel.Title)],
                            Description = parameters[nameof(SessionDetailModel.Description)],
                            ImageUrl = parameters[nameof(SessionDetailModel.ImageUrl)]
                        };
                        var detailPage = new SessionsDetailsPage
                        {
                            BindingContext = new SessionDetailsViewModel(item)
                        };
                        var detailFragment = detailPage.CreateSupportFragment(this);

                        var tx = SupportFragmentManager.BeginTransaction();
                        tx.Add(Resource.Id.pageContainer, detailFragment, key);
                        tx.CommitNow();

                        SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                        UpdatePageTitle();
                    }

                    return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            PreserveCustomControls();
            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.my_toolbar);
            toolbar.SetTitleTextColor(Android.Graphics.Color.White);
            toolbar.Visibility = Android.Views.ViewStates.Gone;
            SetSupportActionBar(toolbar);
            SupportActionBar.SetHomeButtonEnabled(true);

            NavigateAsync("default");
        }

        void PreserveCustomControls()
        {
            FFImageLoading.Forms.Droid.CachedImageRenderer.Init(true);
            CarouselView.FormsPlugin.Android.CarouselViewRenderer.Init();
        }

        public override void OnBackPressed()
        {
            if (SupportFragmentManager.Fragments.Count > 1)
            {
                MoveUp();

                return;
            }

            base.OnBackPressed();
        }

        public override bool OnSupportNavigateUp()
        {
            if (SupportFragmentManager.Fragments.Count > 1)
            {
                MoveUp();

                return true;
            }

            return base.OnSupportNavigateUp();
        }

        void MoveUp()
        {
            var tx = SupportFragmentManager.BeginTransaction();
            tx.Remove(SupportFragmentManager.Fragments.Last());
            tx.CommitNow();
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            UpdatePageTitle();
        }

        void UpdatePageTitle()
        {
            SupportActionBar.Title = SupportFragmentManager.Fragments.Count > 1
                ? "Session Details"
                : "Techconf#3 - CMC";
        }
    }
}

