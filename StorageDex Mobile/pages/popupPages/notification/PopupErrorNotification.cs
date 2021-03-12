
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.pages.popupPages.notification
{
    class PopupErrorNotification : PopupTextNotification
    {
        public PopupErrorNotification(string textIn) : base(textIn)
        {
            base.textLabel.TextColor = Color.Red;
        }
    }
}
