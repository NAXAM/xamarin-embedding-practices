using Foundation;
using UIKit;
using Embedding;
using Embedding.ViewModels;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormsEmbedding.iOs
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate, INavigationService
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            PreserveCustomControls();
            Xamarin.Forms.Forms.Init();

            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var walkthroughPage = new EmbeddingPage
            {
                BindingContext = new EmbeddingPageViewModel(this)
            };
            var walkthroughController = walkthroughPage.CreateViewController();
            var root = new UINavigationController(walkthroughController);
            walkthroughController.NavigationController.NavigationBarHidden = true;

            Window.RootViewController = root;

            Window.MakeKeyAndVisible();

            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }

        void PreserveCustomControls()
        {
            UINavigationBar.Appearance.Translucent = false;
            UINavigationBar.Appearance.TintColor = UIColor.FromRGB(0x35 / 255f, 0x74 / 255f, 0xFA / 255f);
            UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes
            {
                ForegroundColor = UIColor.FromRGB(0x35 / 255f, 0x74 / 255f, 0xFA / 255f)
            };

            FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
            CarouselView.FormsPlugin.iOS.CarouselViewRenderer.Init();
        }

        public Task<bool> GoBackAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> NavigateAsync(string key, IDictionary<string, string> parameters = null)
        {
            if (key == "end_of_walkthrough")
            {
                var nativeRoot = UIStoryboard.FromName("Main", NSBundle.MainBundle).InstantiateInitialViewController();
                if (Window.RootViewController is UINavigationController nav)
                {
                    nav.SetViewControllers(new[] { nativeRoot }, true);
                }
                else
                {
                    Window.RootViewController = nativeRoot;
                }

                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}

