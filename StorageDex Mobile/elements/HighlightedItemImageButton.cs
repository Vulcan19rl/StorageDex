using StorageDexLib;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.elements
{
    class HighlightedItemImageButton : HightlightedImageButton
    {

        public Item item { get; }
        public HighlightedItemImageButton(Item item, ImageSource source) : base(source)
        {
            this.item = item;
        }

        public HighlightedItemImageButton(Item item) : base("camera", true)
        {
            this.item = item;
        }
    }
}
