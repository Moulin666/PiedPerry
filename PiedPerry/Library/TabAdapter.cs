using System.Collections.Generic;
using Android.Support.V4.App;
using Java.Lang;

using SupportFragment = Android.Support.V4.App.Fragment;
using SupportFragmentManager = Android.Support.V4.App.FragmentManager;

namespace PiedPerry.Library
{
    public class TabAdapter : FragmentPagerAdapter
    {
        public List<SupportFragment> Fragments { get; set; }
        public List<string> FragmentNames { get; set; }

        public TabAdapter(SupportFragmentManager supportFragmentManager) : base (supportFragmentManager)
        {
            Fragment = new List<SupportFragment>();
            FragmentNames = new List<string>();
        }

        public void AddFragment(SupportFragment fragment, string name)
        {
            Fragments.Add(fragment);
            FragmentNames.Add(name);
        }

        public override int Count => Fragments.Count;

        public override SupportFragment GetItem(int position) => Fragments[position];

        public ICharSequence GetPageTitleFormatted(int position) => new Java.Lang.String(FragmentNames[position]);
    }
}
