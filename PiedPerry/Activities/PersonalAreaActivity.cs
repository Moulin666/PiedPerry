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

            var getPrefs = Application.Context.GetSharedPreferences("PiedPerry", FileCreationMode.Private);
            string userString = getPrefs.GetString("UserInfo", "");

            if (userString == "")
            {
                Finish();
                return;
            }

            userInfo = JsonConvert.DeserializeObject<UserMap>(userString);

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

        private void SetUpDrawerContent(NavigationView navView)
        {
            //IMenuItem nameViewHeader = navView.Menu.GetItem(Resource.Id.nameViewHeader);
            //nameViewHeader.SetTitle(string.Format("{0} {1} {2}", userInfo.LastName, userInfo.Name, userInfo.MiddleName));

            navView.NavigationItemSelected += (object sender, NavigationView.NavigationItemSelectedEventArgs eventArgs) =>
            {
                eventArgs.MenuItem.SetChecked(true);
                drawerLayout.CloseDrawers();

                switch (eventArgs.MenuItem.ItemId)
                {
                    case Resource.Id.nav_personalArea:
                        PersonalAreaButton_Click(sender, eventArgs);
                        break;

                    case Resource.Id.nav_moreButton1:
                        MoreButton_Click(sender, eventArgs);
                        break;

                    case Resource.Id.nav_moreButton2:
                        View anchor2 = sender as View;
                        Snackbar.Make(anchor2,
                                      string.Format("{0} {1} {2}", userInfo.LastName, userInfo.Name, userInfo.MiddleName),
                                      Snackbar.LengthLong).Show();
                        break;

                    case Resource.Id.nav_moreButton3:
                        View anchor3 = sender as View;
                        Snackbar.Make(anchor3, "и ещооо", Snackbar.LengthLong).Show();
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

        private void MoreButton_Click(object sender, NavigationView.NavigationItemSelectedEventArgs eventArgs)
        {
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
            StartActivity(typeof(MainActivity));
            Finish();
        }
    }
}
