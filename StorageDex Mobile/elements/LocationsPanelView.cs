using StorageDex_Mobile.lib;
using StorageDexLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StorageDex_Mobile.elements
{

    class LocationsPanelView : StackLayout
    {


        public LocationsPanelView(StorageLocation location, int width, int height, Action onTap)
        {
            LocationPanelViewButton button = new LocationPanelViewButton(location, width, height, onTap);
            LocationsPanelViewText textView = new LocationsPanelViewText(location);
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

    class LocationsPanelViewText : StackLayout
    {
        public LocationsPanelViewText(StorageLocation location)
        {
            this.Children.Add(MakeTextLabel(location.GetName()));
            string address = location.address;
            if (address.Trim() != "")
            {
                this.Children.Add(MakeTextLabel(address));
            }

            Console.WriteLine("getting containers");
            List<StorageContainer> containers = location.GetContainers();
            Console.WriteLine("printing containers");
            foreach(StorageContainer container in containers)
            {
                Console.WriteLine("container");
            }
            //Label textLabel = (MakeTextLabel(numOfItems + " Items"));
            //this.Children.Add(textLabel);
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

    class LocationPanelViewButton : ImageButton
    {
        private StorageLocation location;
        public LocationPanelViewButton(StorageLocation location, int width, int height, Action onTap)
        {


            this.location = location;
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
            if (App.IsEnterprise())
            {
                Source = "Camera";
            }
            if (ImageTools.HasImage(location))
            {
                if (ImageBaseHandler.current.isLocationCached(location.GetId()))
                {
                    this.Source = ImageBaseHandler.current.GetLocationImageSource(location.GetId());
                }
                else
                {
                    this.Source = ImageTools.LoadImage(location);
                }

            }
            else
            {
                Source = "camera";
            }
        }
    }
}

