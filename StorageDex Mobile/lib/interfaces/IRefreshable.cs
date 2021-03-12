using System;
using System.Collections.Generic;
using System.Text;

namespace StorageDex_Mobile.lib.interfaces
{
    interface IRefreshable
{
        /**
         * anything with this interface can be refreshed using it's Refresh method
         */

        void Refresh();
}
}
