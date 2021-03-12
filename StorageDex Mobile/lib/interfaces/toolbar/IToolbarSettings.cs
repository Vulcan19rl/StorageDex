using System;
using System.Collections.Generic;
using System.Text;

namespace StorageDex_Mobile.lib.interfaces.toolbar
{
    /**
     * a page or view with this interface will have a settings button in the toolbar supplied to it
     * when the button is clicked the OpenSettings method will trigger.
     */
    interface IToolbarSettings
    {
        void OpenSettings();
    }
}
