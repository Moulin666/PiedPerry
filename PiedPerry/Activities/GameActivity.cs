using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace PiedPerry.Activities
{
    [Activity(Label = "Хоррор", Theme = "@style/Theme.PiedPerry")]
    public class GameActivity : AppCompatActivity
    {
        private TextView questionText { get; set; }

        private Button firstButton { get; set; }
        private Button secondButton { get; set; }

        private string GameType { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GameArea);

            GameType = Intent.GetStringExtra("GameType");

            FrameLayout frameLayout = FindViewById<FrameLayout>(Resource.Id.gameFrame);
            frameLayout.SetBackgroundResource(Resource.Drawable.background_horror);

            questionText = FindViewById<TextView>(Resource.Id.questionText);

            questionText.Text = 
                "Вы очнулись в темном помещении. Пахнет сыростью, плесенью и ржавчиной. Вы лежите на холодном каменном полу, Ваши ноги сованы цепью, в углу куча тряпок. Вы пытаетесь сесть, голова кружится. Внезапно, куча тряпок зашевелилась. Рукой вы нащупываете на полу осколок камня и сжимаете его. Куча садится, и Вы видите женщину с лицом волчицы, она начинает скалиться. Ваши действия:";
        }

        private void Initialize()
        {
            Typeface typeface = Typeface.CreateFromAsset(Assets, "StolzlLight.otf");

            questionText.SetTypeface(typeface, TypefaceStyle.Italic);
        }
    }
}
