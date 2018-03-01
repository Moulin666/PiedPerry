using Android.OS;
using Android.Views;
using Android.App;

namespace PiedPerry.Activities
{
    public class ResumeFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_resume, container, false);
        }
    }
}
