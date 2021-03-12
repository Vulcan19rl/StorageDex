using Java.Lang;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Animations.Base;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Services;
using StorageDex_Mobile.lib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StorageDex_Mobile.pages.popupPages.notification
{
    class PopupTextNotification : Rg.Plugins.Popup.Pages.PopupPage
{
        protected Label textLabel = new Label() { TextColor = PageColors.primaryColor, FontSize = FontSizes.defaultSizeFont };
        public PopupTextNotification(string notificationText)
        {

            StackLayout mainLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(20),
                Padding = new Thickness(10),
                BackgroundColor = StorageDex_Mobile.lib.PageColors.lighterSecondary,
                WidthRequest = MasterNavigationPage.current.Width
            };


            textLabel.Text = notificationText;
            mainLayout.Children.Add(textLabel);

            this.Content = mainLayout;
            this.BackgroundColor = Color.Transparent;

            Task collapseTask = new Task(() =>
            {

                Thread.Sleep(2000);
                PopupNavigation.Instance.PopAsync();
            });

            collapseTask.Start();

        }
}
}
