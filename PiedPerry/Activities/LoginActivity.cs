using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;

namespace PiedPerry.Activities
{
    [Activity(Label = "Авторизация")]
    public class LoginActivity : Activity
    {
        private Button loginButton { get; set; }
        private Button toRegisterButton { get; set; }

        private EditText emailInput { get; set; }
        private EditText passwordInput { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);

            InitComponents();
            Initialize();
        }

        private void InitComponents()
        {
            loginButton = FindViewById<Button>(Resource.Id.loginButton);
            toRegisterButton = FindViewById<Button>(Resource.Id.toRegisterButton);

            emailInput = FindViewById<EditText>(Resource.Id.emailInput);
            passwordInput = FindViewById<EditText>(Resource.Id.passwordInput);

            loginButton.Click += LoginButton_Click;
            toRegisterButton.Click += ToRegisterButton_Click;
        }

        private void Initialize()
        {
            Typeface typeface = Typeface.CreateFromAsset(Assets, "StolzlLight.otf");

            loginButton.SetTypeface(typeface, TypefaceStyle.Normal);
            toRegisterButton.SetTypeface(typeface, TypefaceStyle.Normal);

            emailInput.SetTypeface(typeface, TypefaceStyle.Normal);
            passwordInput.SetTypeface(typeface, TypefaceStyle.Normal);
        }

        private void LoginButton_Click(object sender, EventArgs eventArgs)
        {
            // Login request to the server

            //Toast.MakeText(this, "txt", ToastLength.Short).Show();

            // Handle login response
            var setPrefs = Application.Context.GetSharedPreferences("PiedPerry", FileCreationMode.Private);
            var prefEditor = setPrefs.Edit();
            prefEditor.PutBoolean("isLogin", true);
            prefEditor.Commit();

            StartActivity(typeof(MainActivity));
            Finish();
        }

        private void ToRegisterButton_Click(object sender, EventArgs eventArgs)
        {
            StartActivity(typeof(RegisterActivity));
        }
    }
}
