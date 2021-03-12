using StorageDex_Mobile.lib;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.elements
{
    class DividerLine : BoxView
{
        //a divier line element
        //divides two elements

        bool isVisible = true; //whether or not the line is vissible

        public DividerLine()
        {
            this.Color = PageColors.DividerColor;
            this.WidthRequest = 100;
            this.HeightRequest = 1;
        }

        //makes the lines visible
        public void Show()
        {
            isVisible = true;
            this.HeightRequest = 1;
        }

        //hides the lines
        public void Hide()
        {
            isVisible = false;
            this.HeightRequest = 0;
        }
}
}
