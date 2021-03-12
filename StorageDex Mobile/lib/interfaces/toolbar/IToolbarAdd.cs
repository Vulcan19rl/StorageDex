using System;
using System.Collections.Generic;
using System.Text;

namespace StorageDex_Mobile.lib.interfaces.toolbar
{
    interface IToolbarAdd
{
        //anything with this interface has the add method. Any page with this interface will have the add button added to the toolbar.

        void Add();

        bool CanAdd();
}
}
