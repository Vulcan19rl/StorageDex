using StorageDex_Mobile.lib;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.elements
{
    public class AdMobView : View
    {
        public enum Sizes { Standardbanner, LargeBanner, MediumRectangle, FullBanner, Leaderboard, SmartBannerPortrait }
        public Sizes Size { get; set; }
        public AdMobView()
        {
            this.BackgroundColor = PageColors.secondaryColor;
        }
    }
}
