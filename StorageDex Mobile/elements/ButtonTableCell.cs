using StorageDex_Mobile.lib;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.elements
{
    /**
     * an extension of the button cell but designed for tables
     */
    class ButtonTableCell : ButtonCell
    {
        public ButtonTableCell() : base() {

            base.textLabel.FontSize = FontSizes.smallFontSize;
            base.textLabel.Padding = new Thickness(15, 0);
        }

}
}
