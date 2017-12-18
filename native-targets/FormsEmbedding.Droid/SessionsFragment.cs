using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
namespace FormsEmbedding.Droid
{
    public class SessionsFragment : Fragment
    {
        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_sessions, container, false);

            var toolbar = view.FindViewById<Toolbar>(Resource.Id.my_toolbar);

            (Activity as AppCompatActivity).SetSupportActionBar(toolbar);
            toolbar.Title = "Techconf#3 - CMC";

            return view;
        }
    }
}
