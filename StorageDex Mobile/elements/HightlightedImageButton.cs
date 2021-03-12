using FFImageLoading.Forms;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using MultiGestureViewPlugin;
using StorageDex_Mobile.lib;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StorageDex_Mobile.elements
{
    /**
     * a highlightable image button via long press
     */
    class HightlightedImageButton : MultiGestureView
    {
        private bool isHightlighed = false;
        CachedImage imageContent;
        Color hightlightColor = PageColors.Highlighted.WithAlpha(130);
        TintTransformation highlight = new TintTransformation();
        bool isUsingDefaultImage = false;

        public HightlightedImageButton(Xamarin.Forms.ImageSource imgSource) : base()
        {
            imageContent = new CachedImage();
            imageContent.Source = imgSource;

            this.Content = imageContent;

            this.LongPressed += (sen, e) => Highlight();


            InitHighlight();
        }

        public HightlightedImageButton(Xamarin.Forms.ImageSource imgSource, bool isUsingDefaultImage) : base()
        {
            this.isUsingDefaultImage = isUsingDefaultImage;
            imageContent = new CachedImage();
            imageContent.Source = imgSource;

            this.Content = imageContent;

            this.LongPressed += (sen, e) => Highlight();


            InitHighlight();
        }

        //initializes the highlight color
        private void InitHighlight()
        {
            highlight.EnableSolidColor = true;
            highlight.HexColor = hightlightColor.ToHex();

            imageContent.Transformations = new List<ITransformation> { highlight };
            imageContent.Transformations = new List<ITransformation>();
        }

        //whether or not the button is hihglighted
        public bool IsHighlighted()
        {
            return isHightlighed;
        }

        //highlights the button
        public void Highlight()
        {
            if (IsHighlighted())
            {
                isHightlighed = false;


                imageContent.Transformations = new List<ITransformation>();

                if (isUsingDefaultImage)
                {
                    this.BackgroundColor = Color.Transparent;
                    this.imageContent.Source = "camera";
                }

            }
            else
            {
                isHightlighed = true;
                //apply the hightlight transformation
                imageContent.Transformations = new List<ITransformation> { highlight };

                if (isUsingDefaultImage)
                {
                    this.BackgroundColor = hightlightColor;
                    this.imageContent.Source = "camera_inverted";
                }


            }
        }
    }
}
