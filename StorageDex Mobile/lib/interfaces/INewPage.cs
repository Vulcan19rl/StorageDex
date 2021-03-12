using System;
using System.Collections.Generic;
using System.Text;

namespace StorageDex_Mobile.lib.interfaces
{
    interface INewPage
{
        /**
         * a new page is a page for creating a new instance of something
         */

        //handles taking a photo
        void TakePhoto();

        //handles attaching a photo
        void AttachPhoto();
}
}
