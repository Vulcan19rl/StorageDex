using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.elements
{
    /**
     * a label that is able to be hidden
     */
    class HideableLabel : Label
{
        bool isVisible = true; //whether or not the line is vissible
        double prevHight = 0;

        //shows the label
        public void Show()
        {
            if(prevHight != 0)
            {
                this.HeightRequest = prevHight;
            }
            
            isVisible = true;
        }

        //hides the label
        public void Hide()
        {
            if (this.Height != 0)
            {
                prevHight = this.Height;
                this.HeightRequest = 0;
            }

            isVisible = false;
        }
}
}
