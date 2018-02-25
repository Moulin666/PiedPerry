using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using PiedPerry.DataBase.Map;

namespace PiedPerry.Activities
{
    [Activity(Label = "Регистрация")]
    public class RegisterActivity : Activity
    {
        private Button registerButton { get; set; }

        private EditText nameInput { get; set; }
        private EditText lastNameInput { get; set; }
        private EditText middleInput { get; set; }

        private EditText aboutInput { get; set; }

        private EditText emailRegisterInput { get; set; }
        private EditText passwordRegisterInput { get; set; }
        private EditText confirmPasswordInput { get; set; }

        private RadioGroup sexGroup { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Register);

            InitComponents();
            Initialize();
        }

        private void InitComponents()
        {
            registerButton = FindViewById<Button>(Resource.Id.registerButton);

            nameInput = FindViewById<EditText>(Resource.Id.nameInput);
            lastNameInput = FindViewById<EditText>(Resource.Id.lastNameInput);
            middleInput = FindViewById<EditText>(Resource.Id.middleNameInput);
            aboutInput = FindViewById<EditText>(Resource.Id.aboutInput);
            emailRegisterInput = FindViewById<EditText>(Resource.Id.emailRegisterInput);
            passwordRegisterInput = FindViewById<EditText>(Resource.Id.passwordRegisterInput);
            confirmPasswordInput = FindViewById<EditText>(Resource.Id.confirmPasswordInput);

            registerButton.Click += RegisterButton_Click;
        }

        private void Initialize()
        {
            Typeface typeface = Typeface.CreateFromAsset(Assets, "StolzlLight.otf");

            registerButton.SetTypeface(typeface, TypefaceStyle.Normal);

            nameInput.SetTypeface(typeface, TypefaceStyle.Normal);
            lastNameInput.SetTypeface(typeface, TypefaceStyle.Normal);
            middleInput.SetTypeface(typeface, TypefaceStyle.Normal);
            aboutInput.SetTypeface(typeface, TypefaceStyle.Normal);
            emailRegisterInput.SetTypeface(typeface, TypefaceStyle.Normal);
            passwordRegisterInput.SetTypeface(typeface, TypefaceStyle.Normal);
            confirmPasswordInput.SetTypeface(typeface, TypefaceStyle.Normal);
        }

        private void RegisterButton_Click(object sender, EventArgs eventArgs)
        {
            // Register request to the server

            var user = new UserMap();
            user.Name = "Михаил";
            user.LastName = "Степной";
            user.MiddleName = "Иванович";
            user.Sex = "Мужской";
            user.About = "Король мира владеющий HTML!";
            user.Tags = "HTML";
            user.Rating = 25;
            user.BirthDate = DateTime.Now;

            string userString = JsonConvert.SerializeObject(user);

            var setPrefs = Application.Context.GetSharedPreferences("PiedPerry", FileCreationMode.Private);
            var prefEditor = setPrefs.Edit();
            prefEditor.PutBoolean("isLogin", true);
            prefEditor.PutString("UserInfo", userString);
            prefEditor.Commit();

            Intent intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.NewTask);
            intent.AddFlags(ActivityFlags.ClearTask);
            StartActivity(intent);
        }
    }
}
