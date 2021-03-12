using System;
using System.Collections.Generic;
using System.Text;

namespace StorageDex_Mobile.lib.interfaces.toolbar
{
    interface IToolbarDelete
{
        /**
         * anything with this interface must make use of the delete button
         */

        //if true something can be deleted which usually means a delete button will be shown
        bool CanDelete();

        //the method delete
        void Delete();
}
}
