using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.interfaces;
using StorageDex_Mobile.lib.interfaces.toolbar;
using StorageDex_Mobile.pages.viewPages.itemListPage;
using StorageDexLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StorageDex_Mobile.pages.viewPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationPage : ContentPage, IRefreshable, IToolbarDelete
    {
        /***
         * it the top level view for a location page
         * displays options to open the details of the location, it's items and containers
         * also lets you search for certain things
         */

        StorageLocation location; //the storage location the page is based upon
        int id; // the id of the location the page is based upon
        private bool deleted = false; //if this is true the page has been deleted

        public LocationPage(StorageLocation location)
        {
            InitializeComponent();
            this.location = location;
            this.Title = location.GetName();
            this.id = location.GetId();
       

            this.optionsTable.Margin = new Thickness(15, 5, 15, 5);
            InitButtons();
            Refresh(); //the resfresh method also initializes the page
            this.Appearing += (sen, e) => Refresh();
        }

        //initializes the button
        public void InitButtons()
        {
            detailsButton.Tapped += (sen, e) =>
            {

                Navigation.PushAsync(new LocationDetailsPage(location));
            };

            containersButton.Tapped += (sen, e) =>
            {
                foreach (int cid in location.GetContainerIds()) {
                    Console.WriteLine(cid);


                }
                Navigation.PushAsync(new ContainerListPage(location));
            };



            detailsButton.SetText("Details");
            containersButton.SetText("Containers");
        }

        public void Refresh()
        {

            location = DatabaseHandler.GetDatabase().GetLocation(id);


            //set title
            this.Title = location.GetName();


            //set proportions
            this.imageFrame.HeightRequest = MasterNavigationPage.current.Height / 2.0;


  
            //init image
            if (ImageTools.HasImage(location))
            {
                if (ImageBaseHandler.current.isLocationCached(location.GetId()))
                {
                    SetImage(ImageBaseHandler.current.GetLocationImageSource(location.GetId()));
                }
                else
                {
                    SetImage(ImageTools.LoadImage(location));
                }

            }
            else //if no image give it the place holder image
            {
            
                SetImage("camera");
            }





            //set colors
            this.BackgroundColor = PageColors.secondaryColor;
        }

        //sets the image on the page
        private void SetImage(ImageSource source)
        {
     
            Image img = new Image() { Source = source };
            Grid.SetColumn(img, 0);
            img.VerticalOptions = LayoutOptions.FillAndExpand;
            img.HorizontalOptions = LayoutOptions.FillAndExpand;
            img.Aspect = Aspect.AspectFit;
            this.imageFrame.Content = img;
        }

        //sets the image on the page
        private void SetImage(string source)
        {
            Image img = new Image() { Source = source };
            Grid.SetColumn(img, 0);
            img.VerticalOptions = LayoutOptions.FillAndExpand;
            img.HorizontalOptions = LayoutOptions.FillAndExpand;
            img.Aspect = Aspect.AspectFit;
            this.imageFrame.Content = img;
        }

        //whether or not the location can be deleted
        public bool CanDelete()
        {
            return !deleted;
        }

        public async void Delete()
        {


            //displays a pop up asking if the user is sure
            bool choice = await DisplayAlert("Are you sure?", "Deleting this location will delete all the container and items inside of the location", "Delete", "Cancel");
            if (choice)
            {
                DatabaseHandler.GetDatabase().DeleteLocation(location.GetId());
                DataTools.Save();
                await Navigation.PopAsync();
                deleted = true;
            }
            else
            {
                deleted = false;
            }








        }
    }
}