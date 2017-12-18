using System;
using Foundation;
using UIKit;
using Embedding.Models;
using Embedding;
using Embedding.ViewModels;
using Xamarin.Forms;

namespace FormsEmbedding.iOs
{
    public partial class ViewController : UIViewController
    {
        const string CELL_REUSE_IDENTIFIER = "cell";

        UITableView table;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            Title = "TechConf#3 - CMC";

            NavigationController.NavigationBarHidden = false;

            table = new UITableView(View.Bounds);
            table.DataSource = this;
            table.Delegate = this;
            table.RegisterClassForCellReuse(typeof(UITableViewCell), CELL_REUSE_IDENTIFIER);
            table.RowHeight = UITableView.AutomaticDimension;
            table.EstimatedRowHeight = 100;

            View.AddSubview(table);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }

    partial class ViewController : IUITableViewDataSource
    {
        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = table.DequeueReusableCell(CELL_REUSE_IDENTIFIER, indexPath);

            cell.TextLabel.Text = items[indexPath.Row].Title;
            cell.TextLabel.TextColor = UIColor.FromRGB(0x35 / 255f, 0x74 / 255f, 0xFA / 255f);
            cell.TextLabel.Font = UIFont.SystemFontOfSize(32, UIFontWeight.Light);

            return cell;
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return items.Length;
        }
    }

    partial class ViewController : IUITableViewDelegate
    {
        [Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var sessisonDetailsController = new SessionsDetailsPage
            {
                BindingContext = new SessionDetailsViewModel(items[indexPath.Row])
            }.CreateViewController();

            NavigationController.PushViewController(sessisonDetailsController, true);

            tableView.DeselectRow(indexPath, true);
        }
    }

    partial class ViewController
    {
        SessionDetailModel[] items = {
            new SessionDetailModel {
                Title = "Xamarin",
                Description = "Deliver native Android, iOS, and Windows apps, using existing skills, teams, and code.",
                ImageUrl = "https://www.xamarin.com/content/images/pages/platform/code-sharing@2x.png",
            },
            new SessionDetailModel {
                Title = "Xamarin.Forms",
                Description = "Build native UIs for iOS, Android and Windows from a single, shared C# codebase.",
                ImageUrl = "https://www.xamstatic.com/dist/images/pages/forms/crm-app@2x-gS9Gn7Ma.png",
            },
            new SessionDetailModel {
                Title = "Xamarin Latest",
                Description = "Xamarin Live Player, Xamarin.Forms Embedding, Embeddinator-40000",
                ImageUrl = "https://montemagno.com/content/images/2017/10/image8.png",
            }
        };
    }
}
