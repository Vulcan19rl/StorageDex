using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using StorageDex_Mobile.Droid.lib.renderers;
using StorageDex_Mobile.elements;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(AdMobView), typeof(AdMobViewRenderer))]
namespace StorageDex_Mobile.Droid.lib.renderers
{
    public class AdMobViewRenderer : ViewRenderer<AdMobView, AdView>
    {
        public AdMobViewRenderer(Context context) : base(context) { }

        private AdView CreateAdView()
        {
            var adView = new AdView(Context)
            {


                AdUnitId = "ca-app-pub-6494346638793326/8642833278",
                LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent)
            };
            switch ((Element as AdMobView).Size)
            {
                case AdMobView.Sizes.Standardbanner:
                    adView.AdSize = AdSize.Banner;
                    break;
                case AdMobView.Sizes.LargeBanner:
                    adView.AdSize = AdSize.LargeBanner;
                    break;
                case AdMobView.Sizes.MediumRectangle:
                    adView.AdSize = AdSize.MediumRectangle;
                    break;
                case AdMobView.Sizes.FullBanner:
                    adView.AdSize = AdSize.FullBanner;
                    break;
                case AdMobView.Sizes.Leaderboard:
                    adView.AdSize = AdSize.Leaderboard;
                    break;
                case AdMobView.Sizes.SmartBannerPortrait:
                    adView.AdSize = AdSize.SmartBanner;
                    break;
                default:
                    adView.AdSize = AdSize.Banner;
                    break;
            }

            adView.LoadAd(new AdRequest.Builder().Build());

            return adView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && Control == null)
            {
                SetNativeControl(CreateAdView());
            }
        }
    }
}