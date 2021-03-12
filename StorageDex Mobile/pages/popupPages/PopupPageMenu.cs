using Android.Graphics.Drawables;
using Android.Text;
using Android.Widget;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using StorageDex_Mobile.lib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Markup;

namespace StorageDex_Mobile.pages.popupPages
{
    public partial class PopupPageMenu : Rg.Plugins.Popup.Pages.PopupPage
    {
         StackLayout mainLayout = new StackLayout()
        {
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.Center,
            Padding = new Thickness(20, 10),
            BackgroundColor = StorageDex_Mobile.lib.PageColors.secondaryColor,
            WidthRequest = MasterNavigationPage.current.Width
        };

        MoveAnimation entrance = new MoveAnimation()
        {
            PositionIn = MoveAnimationOptions.Bottom,
            PositionOut = MoveAnimationOptions.Bottom
        };

        public PopupPageMenu()
        {

            this.Animation = entrance; //set animation
            this.Content  = mainLayout; //set content
        }

        public Label AddLabel(string text)
        {
            Label returnLabel = new Label()
            {
                Text = text,
                TextColor = PageColors.textColor,
                FontSize = FontSizes.subTitleFont
            };
            mainLayout.Children.Add(returnLabel);
            return returnLabel;
        }

        public Grid AddLabelAndImage(string text, ImageSource src)
        {
            Grid returnGrid = new Grid();
            returnGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.9, GridUnitType.Star) });
            returnGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.1, GridUnitType.Star) });
            returnGrid.VerticalOptions = LayoutOptions.CenterAndExpand;
            returnGrid.HorizontalOptions = LayoutOptions.CenterAndExpand;
            returnGrid.WidthRequest = MasterNavigationPage.current.Width;


            Label returnLabel = new Label()
            {
                Text = text,
                TextColor = PageColors.textColor,
                FontSize = FontSizes.subTitleFont,
                Padding = new Thickness(0, 5),
                HorizontalTextAlignment = TextAlignment.Start
                
            };

            Image iconImage = new Image() { Source = src };
       
            Grid.SetColumn(iconImage, 1);

            returnGrid.Children.Add(returnLabel);
            returnGrid.Children.Add(iconImage);

            mainLayout.Children.Add(returnGrid);
            return returnGrid;
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }
    }
}
