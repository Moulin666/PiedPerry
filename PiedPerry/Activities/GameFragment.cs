using Android.OS;
using Android.Views;
using Android.App;
using Android.Support.V7.Widget;
using Android.Util;
using System.Collections.Generic;
using Android.Widget;
using Android.Content;
using Android.Content.Res;

namespace PiedPerry.Activities
{
    internal class GameAdapter : RecyclerView.Adapter
    {
        private readonly TypedValue typedValue = new TypedValue();
        private int background;
        private List<GameBlock> values;
        private Resources resources;

        public override int ItemCount => values.Count;

        public GameAdapter(Context context, Resources resources, List<GameBlock> values)
        {
            context.Theme.ResolveAttribute(Resource.Attribute.selectableItemBackground, typedValue, true);
            background = typedValue.ResourceId;
            this.values = values;
            this.resources = resources;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var gameHolder = holder as GameViewHolder;

            gameHolder.gameTitleText.Text = values[position].Title;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Game_list_item, parent, false);
            view.SetBackgroundResource(background);

            return new GameViewHolder(view);
        }
    }

    public class GameViewHolder : RecyclerView.ViewHolder
    {
        public readonly View view;
        public readonly TextView gameTitleText;
        public readonly AppCompatButton gameStartButton;

        public GameViewHolder(View view) : base(view)
        {
            this.view = view;
            gameTitleText = view.FindViewById<TextView>(Resource.Id.gameTitleText);
            gameStartButton = view.FindViewById<AppCompatButton>(Resource.Id.gameStartButton);

            gameStartButton.Click += (sender, e) => 
            {
                Context context = view.Context;
                Intent intent = new Intent(context, typeof(GameActivity));
                intent.PutExtra("GameType", gameTitleText.Text);

                context.StartActivity(intent);
            };
        }
    }

    public class GameBlock
    {
        public readonly string Title;

        public GameBlock(string title)
        {
            Title = title;
        }
    }

    public class GameFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RecyclerView rView = inflater.Inflate(Resource.Layout.fragment_game, container, false) as RecyclerView;

            SetUpRecyclerView(rView);

            return rView;
        }

        private void SetUpRecyclerView(RecyclerView rView)
        {
            // get here news from data base
            List<GameBlock> values = new List<GameBlock>()
            {
                new GameBlock("Текстовый хоррор квест"),
                new GameBlock("Текстовый квест на выживание")
            };

            rView.SetLayoutManager(new LinearLayoutManager(rView.Context));
            rView.SetAdapter(new GameAdapter(rView.Context, Activity.Resources, values));
        }
    }
}
