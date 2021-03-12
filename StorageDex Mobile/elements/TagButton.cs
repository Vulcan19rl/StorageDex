using Java.Lang;
using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.interfaces.toolbar;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.elements
{
    class TagButton : Button
    {
        //a button for tags

        private string buttonText;
        private bool highlighted = false; //whether or not the button is highlighted

       
        public TagButton(string buttonText)
        {
            this.buttonText = buttonText;
            this.Text = buttonText;
            this.HorizontalOptions = LayoutOptions.Fill;
            this.BackgroundColor = PageColors.panelBackgroundColor;

            this.Clicked += (sen, e) =>
            {
                ToggleHightlight();
                MasterNavigationPage.current.RefreshButtons();
            };
        }

        public TagButton(string buttonText, bool onClickActivate)
        {
            this.buttonText = buttonText;
            this.Text = buttonText;
            this.HorizontalOptions = LayoutOptions.Fill;
            this.BackgroundColor = PageColors.panelBackgroundColor;
            if (onClickActivate)
            {
                this.Clicked += (sen, e) =>
                {
                    ToggleHightlight();
                    MasterNavigationPage.current.RefreshButtons();
                };
            }
        }


      
        //calculates a button width without having to render it
        public int CalculateButtonWidth()
        {
            int numberOfChars = buttonText.Length;
            if (numberOfChars <= 8)
            {
                return 88;
            }
            else
            {
                return numberOfChars * 11;
            }
        }

        //returns the button text
        public string GetButtonText()
        {
            return buttonText;
        }

        //toggles the highlight of the button
        private void ToggleHightlight()
        {
            highlighted = !highlighted;
            if (highlighted)
            {
                this.BackgroundColor = PageColors.Highlighted;
            }
            else
            {
                this.BackgroundColor = PageColors.panelBackgroundColor;
            }

            //then updates the current page from the master navigation page to see if there are any changes
            MasterNavigationPage.current.Refresh();

        }

        //returns true if the button ishighlighted
        public bool isHighlighted()
        {
            return highlighted;
        }



    }
}
