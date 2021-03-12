using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using StorageDex_Mobile.iOS.renderers;
using StorageDex_Mobile.lib;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(BorderlessEntryRenderer))]
namespace StorageDex_Mobile.iOS.renderers
{
    class BorderlessEntryRenderer : EntryRenderer
    
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            // Use tint color to change the cursor's color
            Control.TintColor = PageColors.primaryColor.ToPlatformColor();
        }
    }

}