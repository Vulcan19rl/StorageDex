using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using StorageDex_Mobile.Droid.lib.renderers;
using StorageDex_Mobile.elements;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NoCapsButton), typeof(NoCapsButtonRenderer))]
namespace StorageDex_Mobile.Droid.lib.renderers
{
    class NoCapsButtonRenderer : ButtonRenderer
    {
        public NoCapsButtonRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> b)
        {
            base.OnElementChanged(b);
            var button = this.Control;
            button.SetAllCaps(false);

        }
    }


}