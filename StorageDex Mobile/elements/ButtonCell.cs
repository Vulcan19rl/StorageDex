using StorageDex_Mobile.lib;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.elements
{
    class ButtonCell : ViewCell
{
        /**
         * a cell for a tableview or list view that is also a button
         */

        protected Label textLabel = new Label();
        StackLayout content = new StackLayout() { Orientation = StackOrientation.Horizontal, VerticalOptions = LayoutOptions.Center};

        public ButtonCell() : base()
        {


            this.View = content;

            //set text color and font size
            textLabel.TextColor = PageColors.textColor;
            

            content.Children.Add(textLabel);
            
        }


        //sets the text on the button
        public void SetText(string text)
        {
            textLabel.Text = text;
        }
}
}
