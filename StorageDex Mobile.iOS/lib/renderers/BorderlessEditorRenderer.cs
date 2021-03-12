using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using StorageDex_Mobile.iOS.lib.renderers;
using StorageDex_Mobile.lib;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Editor), typeof(BorderlessEditorRenderer))]
namespace StorageDex_Mobile.iOS.lib.renderers
{
    class BorderlessEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            // Use tint color to change the cursor's color
            Control.TintColor = PageColors.primaryColor.ToPlatformColor();
        }
    }
}