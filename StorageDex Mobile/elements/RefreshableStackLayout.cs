using StorageDex_Mobile.lib.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.elements
{
    class RefreshableStackLayout : StackLayout, IRefreshable
{

        public void Refresh()
        {
            foreach(View child in this.Children)
            {
                if (typeof(IRefreshable).IsInstanceOfType(child))
                {
                    ((IRefreshable)child).Refresh();
                }
            }
        }
}
}
