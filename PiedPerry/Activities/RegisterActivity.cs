using System;
using System.Json;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using PiedPerry.DataBase;
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
        private EditText birthDateInput { get; set; }

        private EditText aboutInput { get; set; }

        private EditText emailRegisterInput { get; set; }
        private EditText passwordRegisterInput { get; set; }
        private EditText confirmPasswordInput { get; set; }

        private RadioGroup sexGroup { get; set; }

        private string sex = "Мужской";

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
            birthDateInput = FindViewById<EditText>(Resource.Id.birthDateInput);
            aboutInput = FindViewById<EditText>(Resource.Id.aboutInput);
            emailRegisterInput = FindViewById<EditText>(Resource.Id.emailRegisterInput);
            passwordRegisterInput = FindViewById<EditText>(Resource.Id.passwordRegisterInput);
            confirmPasswordInput = FindViewById<EditText>(Resource.Id.confirmPasswordInput);
            sexGroup = FindViewById<RadioGroup>(Resource.Id.sexGroup);

            registerButton.Click += RegisterButton_Click;

            sexGroup.CheckedChange += (sender, e) => 
            {
                if (sexGroup.CheckedRadioButtonId == Resource.Id.maleRadioButton)
                    sex = "Мужской";
                else if (sexGroup.CheckedRadioButtonId == Resource.Id.femaleRadioButton)
                    sex = "Женский";

                lastNameInput.Text = sex;
            };
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

        private async void RegisterButton_Click(object sender, EventArgs eventArgs)
        {
            var userSend = new UserMap();
            userSend.first_name = nameInput.Text;
            userSend.last_name = lastNameInput.Text;
            userSend.middle_name = middleInput.Text;
            userSend.UserGender = sex;
            userSend.about_me = aboutInput.Text;
            userSend.tags = "";
            userSend.rating = 0;
            userSend.birthday_date = birthDateInput.Text;

            var registerSend = new RegisterMap();
            registerSend.userMap = userSend;
            registerSend.password = passwordRegisterInput.Text;
            registerSend.email = emailRegisterInput.Text;

            string registerInfo = JsonConvert.SerializeObject(registerSend);

            string url = string.Format(
                "http://whisperq.ru/Api?key=ee85d34b-8443-4c8d-9369-0cfb04c2d79d&target=authorization&registerInfo={0}",
                registerInfo);

            string jsonResponse = "";

            try
            {
                FetchHelper fetchHelper = new FetchHelper();
                jsonResponse = await fetchHelper.FetchObject(url);
            }
            catch (Exception ex) { ex.ToString(); return; }

            Response Response = JsonConvert.DeserializeObject<Response>(jsonResponse);

            if (Response.responseCode.code != "200")
            {
                View anchor = sender as View;
                Snackbar.Make(anchor, "Информация задана не корректно.", Snackbar.LengthLong).Show();

                return;
            }

            string accountEmail = emailRegisterInput.Text;
            string accountPassword = passwordRegisterInput.Text;

            var setPrefs = Application.Context.GetSharedPreferences("PiedPerry", FileCreationMode.Private);
            var prefEditor = setPrefs.Edit();
            prefEditor.PutBoolean("isLogin", true);
            prefEditor.PutString("AccountEmail", accountEmail);
            prefEditor.PutString("AccountPassword", accountPassword);
            prefEditor.Commit();

            Intent intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.NewTask);
            intent.AddFlags(ActivityFlags.ClearTask);
            StartActivity(intent);
        }
    }
}
