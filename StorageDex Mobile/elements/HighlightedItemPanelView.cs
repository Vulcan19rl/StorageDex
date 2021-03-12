using Android.Widget;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using MultiGestureViewPlugin;
using StorageDex_Mobile.elements;
using StorageDex_Mobile.lib;
using StorageDexLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;

namespace StorageDex_Mobile.elements
{
    class HighlightedItemPanelView : Grid
    {

        bool isHighlighted = false;
        Color highlightColor = PageColors.Highlighted.WithAlpha(40);
        TintTransformation highlight = new TintTransformation();
        MultiGestureView cover = new MultiGestureView() { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.Transparent };
        ItemPanelView itemPanelBg;
        public Item item;


        public HighlightedItemPanelView(Item item, int width, int height, Action onTap)
        {
            this.item = item;
            itemPanelBg = new ItemPanelView(item, width, height, onTap);
            InitHighlight();

            this.Children.Add(itemPanelBg);
            this.Children.Add(cover);

            cover.Tapped += (sen, e) => onTap.Invoke();

            cover.LongPressed += (sen, e) =>
            {
                ToggleHighlight();
            };
        }


        //initializes the highlight color
        private void InitHighlight()
        {
            highlight.EnableSolidColor = true;
            highlight.HexColor = highlightColor.ToHex();


        }


        public bool IsHighlighted()
        {
            return isHighlighted;
        }

        public void ToggleHighlight()
        {

            if (IsHighlighted())
            {
                itemPanelBg.BackgroundColor = Color.Transparent;
                itemPanelBg.button.Transformations = new List<ITransformation>();

                //toggle text color
                itemPanelBg.textView.textLabel.TextColor = PageColors.textColor;

                //change camera color
                if (itemPanelBg.button.isUsingDefaultImage)
                {
                    itemPanelBg.button.SetImage("camera");
                }
            }
            else
            {
                itemPanelBg.BackgroundColor = PageColors.Highlighted;
                itemPanelBg.button.Transformations = new List<ITransformation>();
                itemPanelBg.button.Transformations = new List<ITransformation>() { highlight };

                //toggle text color
                itemPanelBg.textView.textLabel.TextColor = PageColors.textColorInverted;

                //change camera color
                if (itemPanelBg.button.isUsingDefaultImage)
                {
                    itemPanelBg.button.SetImage("camera_inverted");
                }
            }

            isHighlighted = !isHighlighted;

        }
    }
}
