using System;
using System.Json;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;
using PiedPerry.DataBase;
using PiedPerry.DataBase.Map;

namespace PiedPerry.Activities
{
    [Activity(Label = "Авторизация", Theme = "@style/Theme.PiedPerry")]
    public class LoginActivity : AppCompatActivity
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

        private async void LoginButton_Click(object sender, EventArgs eventArgs)
        {
            string url = string.Format("{0} {1}",
                emailInput.Text, passwordInput.Text);

            FetchHelper fetchHelper = new FetchHelper();
            JsonValue jsonResponse = await fetchHelper.FetchObject(url);
            JsonValue requestInfo = jsonResponse["request_Info"];

            if (requestInfo["code"] != "OK")
            {
                TextInputLayout passwordInputLayout =
                    FindViewById<TextInputLayout>(Resource.Id.passwordInputLayout);

                passwordInputLayout.Error = "Email или пароль введены не верно.";

                return;
            }

            string userString = jsonResponse["send_data"];

            UserMap user = JsonConvert.DeserializeObject<UserMap>(userString);

            string userInfo = JsonConvert.SerializeObject(user); // or just take userString
            string accountEmail = emailInput.Text;
            string accountPassword = passwordInput.Text;

            var setPrefs = Application.Context.GetSharedPreferences("PiedPerry", FileCreationMode.Private);
            var prefEditor = setPrefs.Edit();
            prefEditor.PutBoolean("isLogin", true);
            prefEditor.PutString("UserInfo", userInfo);
            prefEditor.PutString("AccountEmail", accountEmail);
            prefEditor.PutString("AccountPassword", accountPassword);
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
