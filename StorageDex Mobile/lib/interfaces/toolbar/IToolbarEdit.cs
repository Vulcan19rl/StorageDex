using System;
using System.Collections.Generic;
using System.Text;

namespace StorageDex_Mobile.lib.interfaces.toolbar
{
    interface IToolbarEdit
{

        //something with this interface will have the ability to be edited. A page with this interface will have the edit toolbar button

        void Edit();

        bool CanEdit();
}
}
