using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Support.V4.View;

using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportActionBar = Android.Support.V7.App.ActionBar;

namespace PiedPerry.Activities
{
    [Activity(Label = "Личный кабинет", Theme = "@style/Theme.PiedPerry")]
    public class PersonalAreaActivity : AppCompatActivity
    {
        private DrawerLayout drawerLayout { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PersonalArea);

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

            TabLayout tabs = FindViewById<TabLayout>(Resource.Id.tabs);

            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            SetUpViewePager(viewPager);

            tabs.SetupWithViewPager(viewPager);
        }

        private void SetUpDrawerContent(NavigationView navView)
        {

        }

        private void SetUpViewePager(ViewPager viewPager)
        {
            
        }

        private void ExitFromAccButton_Click(object sender, EventArgs eventArgs)
        {
            // clear all

            var setPrefs = Application.Context.GetSharedPreferences("PiedPerry", FileCreationMode.Private);
            var prefEditor = setPrefs.Edit();
            prefEditor.PutBoolean("isLogin", false);
            prefEditor.Commit();

            StartActivity(typeof(MainActivity));
            Finish();
        }
    }
}
