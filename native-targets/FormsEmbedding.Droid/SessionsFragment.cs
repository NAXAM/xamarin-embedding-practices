using Android.Support.V4.App;
using Android.Widget;
using Embedding.Models;
using System.Linq;
using Embedding.ViewModels;
using System.Collections.Generic;

namespace FormsEmbedding.Droid
{
    public partial class SessionsFragment : Fragment
    {
        ListView lstSessions;

        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_sessions, container, false);

            return view;
        }

        public override void OnViewCreated(Android.Views.View view, Android.OS.Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            lstSessions = view.FindViewById<ListView>(Resource.Id.lstSessions);
            lstSessions.Adapter = new ArrayAdapter(
                Context,
                Resource.Layout.fragment_sessions_item,
                Resource.Id.txtTitle,
                items.Select(x => x.Title).ToArray()
            );

            lstSessions.ItemClick += HandleSessionSelected;
        }
    }

    partial class SessionsFragment
    {
        public override void OnDestroyView()
        {
            lstSessions.ItemClick -= HandleSessionSelected;

            base.OnDestroyView();
        }

        void HandleSessionSelected(object sender, AdapterView.ItemClickEventArgs e)
        {
            (Activity as INavigationService)
                .NavigateAsync(
                    "view_session_details",
                    new Dictionary<string, string>{
                    {nameof(SessionDetailModel.Title), items[e.Position].Title},
                    {nameof(SessionDetailModel.Description), items[e.Position].Description},
                    {nameof(SessionDetailModel.ImageUrl), items[e.Position].ImageUrl},
                });
        }
    }

    partial class SessionsFragment
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
