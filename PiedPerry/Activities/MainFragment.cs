using Android.OS;
using Android.Views;
using Android.App;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Android.Content;
using Android.Content.Res;
using Android.Util;
using Android.Widget;

namespace PiedPerry.Activities
{
    internal class NewsAdapter : RecyclerView.Adapter
    {
        private readonly TypedValue typedValue = new TypedValue();
        private int background;
        private List<NewsBlock> values;
        private Resources resources;

        public override int ItemCount => values.Count;

        public NewsAdapter(Context context, Resources resources, List<NewsBlock> values)
        {
            context.Theme.ResolveAttribute(Resource.Attribute.selectableItemBackground, typedValue, true);
            background = typedValue.ResourceId;
            this.values = values;
            this.resources = resources;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var newsHolder = holder as NewsViewHolder;

            newsHolder.newsTitleText.Text = values[position].Title;
            newsHolder.newsInfoText.Text = values[position].Info;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Menu_list_item, parent, false);
            view.SetBackgroundResource(background);

            return new NewsViewHolder(view);
        }
    }

    public class NewsViewHolder : RecyclerView.ViewHolder
    {
        public readonly View view;
        public readonly TextView newsTitleText;
        public readonly TextView newsInfoText;

        public NewsViewHolder(View view) : base(view)
        {
            this.view = view;
            newsTitleText = view.FindViewById<AppCompatTextView>(Resource.Id.newsTitleText);
            newsInfoText = view.FindViewById<AppCompatTextView>(Resource.Id.newsInfoText);
        }
    }

    public class NewsBlock
    {
        public readonly string Title;
        public readonly string Info;

        public NewsBlock(string title, string info)
        {
            Title = title;
            Info = info;
        }
    }

    public class MainFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RecyclerView rView = inflater.Inflate(Resource.Layout.fragment_main, container, false) as RecyclerView;

            SetUpRecyclerView(rView);

            return rView;
        }

        private void SetUpRecyclerView(RecyclerView rView)
        {
            // get here news from data base
            List<NewsBlock> values = new List<NewsBlock>()
            {
                new NewsBlock("Запущен сайт сервиса", "1 марта 2018 года был запущен сайт данного сервиса."),
                new NewsBlock("Запущено мобильное приложение", "1 марта 2018 года было запущенно мобильное приложение данного сервиса."),
                new NewsBlock("Создано это меню", "1 марта 2018 года было созданно это меню.")
            };

            rView.SetLayoutManager(new LinearLayoutManager(rView.Context));
            rView.SetAdapter(new NewsAdapter(rView.Context, Activity.Resources, values));
        }
    }
}
