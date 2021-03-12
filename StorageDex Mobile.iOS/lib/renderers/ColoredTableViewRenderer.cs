using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Foundation;
using StorageDex_Mobile.elements;
using StorageDex_Mobile.iOS.lib.renderers;
using StorageDex_Mobile.lib;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ColoredTableView), typeof(ColoredTableViewRenderer))]
namespace StorageDex_Mobile.iOS.lib.renderers
{
    public class ColoredTableViewRenderer : TableViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                return;

            var tableView = Control as UITableView;
            var coloredTableView = Element as ColoredTableView;
            tableView.SeparatorColor = ColorExtensions.ToUIColor(PageColors.DividerColor);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "SeparatorColor")
            {
                var tableView = Control as UITableView;
                var coloredTableView = Element as ColoredTableView;

                tableView.SeparatorColor = ColorExtensions.ToUIColor(PageColors.DividerColor);
            }
        }
    }
}