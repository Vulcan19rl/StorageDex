using StorageDex_Mobile.lib;
using StorageDexLib;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.elements
{
    class ContainerPanelView : StackLayout
    {
        public ContainerPanelView(StorageContainer container, int width, int height, Action onTap)
        {
            ContainerPanelViewButton button = new ContainerPanelViewButton(container, width, height, onTap);
            ContainerPanelViewText textView = new ContainerPanelViewText(container);
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

    class ContainerPanelViewText : StackLayout
    {
        public ContainerPanelViewText(StorageContainer container)
        {
            this.Children.Add(MakeTextLabel(container.GetName()));
            this.Children.Add(MakeTextLabel(container.GetNumberOfItems().ToString() + " Items"));
        }

        //makes a text label for information about the location
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



    class ContainerPanelViewButton : ImageButton
    {
        private StorageContainer container;
        public ContainerPanelViewButton(StorageContainer container, int width, int height, Action onTap)
        {


            this.container = container;
            this.HorizontalOptions = LayoutOptions.Start;
            this.Padding = 4;
            this.HeightRequest = height;
            this.WidthRequest = width;
            this.BackgroundColor = Color.Transparent;
            this.Clicked += (sen, e) => onTap.Invoke();
            SetImage();
        }

        private void SetImage()
        {
            if (ImageTools.HasImage(container))
            {
                if (ImageBaseHandler.current.isContainerCached(container.GetId()))
                {
                    this.Source = ImageBaseHandler.current.GetContainerImageSource(container.GetId());
                }
                else
                {
                    this.Source = ImageTools.LoadImage(container);
                }

            }
            else
            {
                Source = "camera";
            }
        }
    }
}
