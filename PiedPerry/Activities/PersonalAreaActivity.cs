using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace PiedPerry.Activities
{
    [Activity(Label = "Личный кабинет")]
    public class PersonalAreaActivity : Activity
    {
        private Button exitFromAccButton { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PersonalArea);

            InitComponents();
        }

        private void InitComponents()
        {
            exitFromAccButton = FindViewById<Button>(Resource.Id.exitFromAccButton);

            exitFromAccButton.Click += ExitFromAccButton_Click;
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
