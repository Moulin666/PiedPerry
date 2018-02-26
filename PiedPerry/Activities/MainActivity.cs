using Android.App;
using Android.OS;
using Android.Content;

namespace PiedPerry.Activities
{
    [Activity(Label = "PiedPerry", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var getPrefs = Application.Context.GetSharedPreferences("PiedPerry", FileCreationMode.Private);

            var isLogin = getPrefs.GetBoolean("isLogin", false);

            if (!isLogin)
                StartActivity(typeof(LoginActivity));
            else
                StartActivity(typeof(PersonalAreaActivity));

            Finish();
        }
    }
}

