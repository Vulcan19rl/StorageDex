using System;
using System.Collections.Generic;
using System.Text;

namespace StorageDex_Mobile.lib.interfaces.toolbar
{
    //a page with this interface will be given a dots menu icon in the toolbar
    interface IToolbarMenuDots
    {
        //opens the menu
        void OpenMenu();

        //whether or not the menu can be opened
        bool CanOpenMenu();
    }
}
