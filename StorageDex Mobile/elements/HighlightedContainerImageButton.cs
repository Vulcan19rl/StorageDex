
using StorageDexLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.elements
{
    /// <summary>
    /// a highlighted image button for containers
    /// </summary>
    class HighlightedContainerImageButton : HightlightedImageButton
    {
        public StorageContainer Container { get;} 
        public HighlightedContainerImageButton(StorageContainer container, ImageSource source) : base(source)
        {
            Container = container;
        }

        public HighlightedContainerImageButton(StorageContainer container) : base("camera", true)
        {
            Container = container;
        }
    }
}
