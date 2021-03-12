using FFImageLoading.Forms;
using StorageDex_Mobile.lib;
using StorageDexLib;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.elements
{
    class ItemPanelView : StackLayout
    {
        public ItemPanelViewButton button;
        public ItemPanelViewText textView;
        public ItemPanelView(Item item, int width, int height, Action onTap)
        {
            button = new ItemPanelViewButton(item, width, height, onTap);
            textView = new ItemPanelViewText(item);
            this.Children.Add(button);
            this.Children.Add(textView);
            this.Orientation = StackOrientation.Horizontal;
            this.BackgroundColor = Color.Transparent;
            TapGestureRecognizer newGesture = new TapGestureRecognizer();
            newGesture.Tapped += (sen, e) =>
            {
                onTap.Invoke();
            };
            this.GestureRecognizers.Add(newGesture);
        }
    }

    class ItemPanelViewText : StackLayout
    {
        public Label textLabel;
        public ItemPanelViewText(Item item)
        {
            textLabel = MakeTextLabel(item.GetName());
            this.Children.Add(textLabel);

        }

        //makes a text label for information about the item
        private Label MakeTextLabel(string textIn)
        {
            Label returnLabel = new Label();
            returnLabel.TextColor = PageColors.textColor;
            returnLabel.FontSize = FontSizes.subTitleFont;
            returnLabel.Text = textIn;
            returnLabel.Padding = new Thickness(2, 4);
            return returnLabel;
        }
    }

    class ItemPanelViewButton : CachedImage
    {
        private Item item;
        public bool isUsingDefaultImage = false;
        public ItemPanelViewButton(Item item, int width, int height, Action onTap)
        {


            this.item = item;
            this.HorizontalOptions = LayoutOptions.Start;
            this.Margin = 4;
            this.HeightRequest = height;
            this.WidthRequest = width;
            this.BackgroundColor = Color.Transparent;
            TapGestureRecognizer gesture = new TapGestureRecognizer();
            gesture.Tapped += (sen, e) =>
            {
                onTap.Invoke();
            };

            SetImage();
        }

        public void SetImage(ImageSource source)
        {
            this.Source = source;
        }

        public void SetImage()
        {
            if (ImageTools.HasImage(item))
            {
                if (ImageBaseHandler.current.isItemCached(item.GetId()))
                {
                    this.Source = ImageBaseHandler.current.GetItemImageSource(item.GetId());
                }
                else
                {
                    this.Source = ImageTools.LoadImage(item);
                }

            }
            else
            {
                Source = "camera";
                isUsingDefaultImage = true;
            }
        }
    }

}
