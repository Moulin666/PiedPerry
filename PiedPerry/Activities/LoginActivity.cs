using System;
using System.IO;
using System.Json;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;
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
            //string url = "http://api.geonames.org/findNearByWeatherJSON?lat=" +
            //    emailInput.Text + "&lng=" + passwordInput.Text + "&username=demo";

            //JsonValue jsonResponse = await FetchUser(url);
            //JsonValue requestInfo = jsonResponse["request_Info"];

            //if (requestInfo["code"] != "OK")
            //{
            //    TextInputLayout passwordInputLayout =
            //        FindViewById<TextInputLayout>(Resource.Id.passwordInputLayout);

            //    passwordInputLayout.Error = "Email или пароль введены не верно.";

            //    return;
            //}

            //JsonValue userInfo = jsonResponse["send_data"];

            //var user = new UserMap();
            //user.Name = userInfo["first_name"];
            //user.LastName = userInfo["last_name"];
            //user.MiddleName = userInfo["middle_name"];
            //user.Sex = userInfo["UserGender"];
            //user.About = userInfo["about_me"];
            //user.Tags = userInfo["tags"];
            //user.Rating = userInfo["rating"];
            //user.BirthDate = userInfo["birthday_date"];

            JsonValue jsonFile;

            var assembly = typeof(LoadResourceText).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("WorkingWithFiles.PCLTextResource.txt");
            using (StreamReader r = new StreamReader())
            {
                jsonFile = r.ReadToEnd();
            }

            JsonValue userInfo = jsonFile["send_data"];

            var user = new UserMap();
            user.Name = userInfo["first_name"];
            user.LastName = userInfo["last_name"];
            user.MiddleName = userInfo["middle_name"];
            user.Sex = userInfo["UserGender"];
            user.About = userInfo["about_me"];
            user.Tags = userInfo["tags"];
            user.Rating = userInfo["rating"];
            user.BirthDate = userInfo["birthday_date"];

            string userString = JsonConvert.SerializeObject(user);

            var setPrefs = Application.Context.GetSharedPreferences("PiedPerry", FileCreationMode.Private);
            var prefEditor = setPrefs.Edit();
            prefEditor.PutBoolean("isLogin", true);
            prefEditor.PutString("UserInfo", userString);
            prefEditor.Commit();

            StartActivity(typeof(MainActivity));
            Finish();
        }

        private async Task<JsonValue> FetchUser(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    JsonValue json = await Task.Run(() => JsonValue.Load(stream));
                    return json;
                }
            }
        }

        private void ToRegisterButton_Click(object sender, EventArgs eventArgs)
        {
            StartActivity(typeof(RegisterActivity));
        }
    }
}
