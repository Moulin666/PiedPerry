using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Views;

using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportActionBar = Android.Support.V7.App.ActionBar;
using Android.Widget;
using PiedPerry.DataBase.Map;
using Newtonsoft.Json;
using PiedPerry.DataBase;
using System.Threading.Tasks;
using System;

namespace PiedPerry.Activities
{
    [Activity(Label = "Личный кабинет", Theme = "@style/Theme.PiedPerry")]
    public class PersonalAreaActivity : AppCompatActivity
    {
        private DrawerLayout drawerLayout { get; set; }

        private Button exitFromAccButton { get; set; }

        private UserMap userInfo { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PersonalArea);

            Task<UserMap> taskUserInfo = Task.Run(async () =>
            {
                UserMap ui = await GetUserInfo();
                return ui;
            });

            userInfo = taskUserInfo.Result;

            InitComponents();
        }

        private void InitComponents()
        {
            SupportToolbar toolbar = FindViewById<SupportToolbar>(Resource.Id.toolBar);
            SetSupportActionBar(toolbar);

            SupportActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);

            NavigationView navView = FindViewById<NavigationView>(Resource.Id.navView);
            if (navView != null)
                SetUpDrawerContent(navView);
        }

        private async Task<UserMap> GetUserInfo()
        {
            var getPrefs = Application.Context.GetSharedPreferences("PiedPerry", FileCreationMode.Private);
            var email = getPrefs.GetString("AccountEmail", "e");
            var pass = getPrefs.GetString("AccountPassword", "1");

            string url = string.Format(
                "http://whisperq.ru/Api?key=ee85d34b-8443-4c8d-9369-0cfb04c2d79d&target=authorization&email={0}&password={1}",
                email, pass);

            string jsonResponse = "";

            try
            {
                FetchHelper fetchHelper = new FetchHelper();
                jsonResponse = await fetchHelper.FetchObject(url);
            }
            catch (Exception ex)
            {
                ex.ToString();

                var setPrefs1 = Application.Context.GetSharedPreferences("PiedPerry", FileCreationMode.Private);
                var prefEditor1 = setPrefs1.Edit();
                prefEditor1.Clear();

                StartActivity(typeof(MainActivity));
                Finish();

                return new UserMap();
            }

            Response Response = JsonConvert.DeserializeObject<Response>(jsonResponse);

            if (Response.responseCode.code != "OK")
            {
                var setPrefs1 = Application.Context.GetSharedPreferences("PiedPerry", FileCreationMode.Private);
                var prefEditor1 = setPrefs1.Edit();
                prefEditor1.Clear();

                StartActivity(typeof(MainActivity));
                Finish();

                return new UserMap();
            }

            return Response.userMap;
        }

        private void SetUpDrawerContent(NavigationView navView)
        {
            navView.NavigationItemSelected += (object sender, NavigationView.NavigationItemSelectedEventArgs eventArgs) =>
            {
                eventArgs.MenuItem.SetChecked(true);
                drawerLayout.CloseDrawers();

                switch (eventArgs.MenuItem.ItemId)
                {
                    case Resource.Id.nav_personalArea:
                        PersonalAreaButton_Click(sender, eventArgs);
                        break;

                    case Resource.Id.nav_resumeButton:
                        ResumeButton_Click(sender, eventArgs);
                        break;

                    case Resource.Id.nav_findJobButton:
                        FindJobButton_Click(sender, eventArgs);
                        break;

                    case Resource.Id.nav_testingButton:
                        TestingButton_Click(sender, eventArgs);
                        break;

                    case Resource.Id.nav_exitButton:
                        ExitFromAccButton_Click(sender, eventArgs);
                        break;
                }
            };
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer((int)GravityFlags.Left);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void ResumeButton_Click(object sender, NavigationView.NavigationItemSelectedEventArgs eventArgs)
        {
            View anchor = sender as View;
            Snackbar.Make(anchor, "Резюме", Snackbar.LengthLong).Show();
        }

        private void FindJobButton_Click(object sender, NavigationView.NavigationItemSelectedEventArgs eventArgs)
        {
            View anchor = sender as View;
            Snackbar.Make(anchor, "Поиск работы", Snackbar.LengthLong).Show();
        }

        private void TestingButton_Click(object sender, NavigationView.NavigationItemSelectedEventArgs eventArgs)
        {
            View anchor = sender as View;
            Snackbar.Make(anchor,
                          string.Format("{0} {1} {2}", userInfo.last_name, userInfo.first_name, userInfo.middle_name),
                          Snackbar.LengthLong).Show();
        }

        private void ExitFromAccButton_Click(object sender, NavigationView.NavigationItemSelectedEventArgs eventArgs)
        {
            var setPrefs = Application.Context.GetSharedPreferences("PiedPerry", FileCreationMode.Private);
            var prefEditor = setPrefs.Edit();
            prefEditor.Clear();
            prefEditor.Commit();

            StartActivity(typeof(MainActivity));
            Finish();
        }

        private void PersonalAreaButton_Click(object sender, NavigationView.NavigationItemSelectedEventArgs eventArgs)
        {
            View anchor = sender as View;
            Snackbar.Make(anchor,
                          string.Format("{0} {1} {2}", userInfo.last_name, userInfo.first_name, userInfo.middle_name),
                          Snackbar.LengthLong).Show();
            
            StartActivity(typeof(MainActivity));
            Finish();
        }
    }
}
