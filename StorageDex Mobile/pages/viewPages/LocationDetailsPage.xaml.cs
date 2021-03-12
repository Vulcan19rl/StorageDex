using Android.Provider;
using StorageDex_Mobile.elements;
using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.interfaces;
using StorageDex_Mobile.lib.interfaces.toolbar;
using StorageDex_Mobile.pages.newPages;
using StorageDexLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StorageDex_Mobile.pages.viewPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationDetailsPage : ContentPage, IToolbarEdit, IRefreshable
    {
        StorageLocation location;
        int id; //the location id
        Image img = new Image();


        
        public LocationDetailsPage(StorageLocation location)
        {

            this.location = location;
            this.id = location.GetId();


            InitializeComponent();
            this.Appearing += (sen, e) => Refresh();
            this.imageFrame.Content = img;
            img.VerticalOptions = LayoutOptions.FillAndExpand;
            img.HorizontalOptions = LayoutOptions.FillAndExpand;
            img.Aspect = Aspect.AspectFit;

            //set colors
            this.addressCityAndCountry.TextColor = PageColors.textColor;
            this.notesLabel.TextColor = PageColors.textColor;


            //set font size
            this.addressCityAndCountry.FontSize = FontSizes.subTitleFont;

            Refresh();


        }

        

        //sets image
        private void SetImage(ImageSource source)
        {
            img.Source = source;


        }

        //sets the image on the page
        private void SetImage(string source)
        {

            img.Source = source;


        }

        //sets the address text
        private void SetAddress()
        {
            //set address text
            List<string> addressTextParts = new List<string>();
            if (location.address != "")
            {
                addressTextParts.Add(location.address);
            }
            if (location.city != "")
            {
                addressTextParts.Add(location.city);
            }
            if (location.country != "")
            {
                addressTextParts.Add(location.country);
            }
            string addressText = String.Join(", ", addressTextParts);
            this.addressCityAndCountry.Text = addressText;
        }

        //sets the notes text
        private void SetNotes()
        {
            this.notesLabel.Text = location.GetNotes();
        }

        public async void Edit()
        {

            NewLocationPage editPage = new NewLocationPage(location.GetName(), location.address, location.city, location.country, location.GetNotes(), location.GetTags(), location.GetId(), location);
            await Navigation.PushAsync(editPage);
        }

        public bool CanEdit()
        {
            return true; //can always edit location
        }

        public void Refresh()
        {
            location = DatabaseHandler.GetDatabase().GetLocation(id);

            //set title
            this.Title = location.GetName();


            //set proportions
            this.imageFrame.HeightRequest = MasterNavigationPage.current.Height / 2.0;
            addressCityAndCountry.Margin = new Thickness(5);



            //set text
            SetAddress();
            SetNotes();


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
                Console.WriteLine("no image");
                SetImage("camera");
            }

            //init tags
            tagDisplay.ClearTags();
            if (location.GetTags().Count == 0 || (location.GetTags().Count == 0 && location.GetTags()[0] == ""))
            {
                
            }
            else
            {
                tagDisplay.AddTagToBatchWithoutClick(location.GetTags());
            }


            //remove uneeded lines
            if (location.GetNotes().Trim() == "")
            {
                notesLabel.Hide();
                notesDivider.Hide();
            }
            else
            {
                notesLabel.Show();
                notesDivider.Show();
            }

            if (location.address.Trim() == "" && location.city.Trim() == "" && location.country.Trim() == "")
            {
                addressCityAndCountry.Hide();
                addressDivider.Hide();
            }
            else
            {
                addressCityAndCountry.Show();
                addressDivider.Show();
            }



            //set colors
            this.BackgroundColor = PageColors.secondaryColor;
        }


    }

}