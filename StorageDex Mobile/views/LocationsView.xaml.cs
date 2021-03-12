using StorageDex_Mobile.elements;
using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.interfaces;
using StorageDex_Mobile.pages.newPages;
using StorageDex_Mobile.pages.viewPages;
using StorageDexLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace StorageDex_Mobile.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationsView : ContentView, IRefreshable
    {
        public static readonly int LocationImageWidth = 120; //the default width of a location image
        public static readonly int LocationImageHeight = 120; //the default height of a location image
        public LocationsView()
        {
            InitializeComponent();

            //set colors
            title.TextColor = PageColors.textColor;
            locationCount.TextColor = PageColors.textColor;
            this.BackgroundColor = Color.Transparent;

            title.FontSize = FontSizes.subTitleFont;
            locationCount.FontSize = FontSizes.subTitleFont;

            InitContent();

         
        }

        //adds/refreshes all the locations in the view
        private void RefreshLocations()
        {
            this.contentLayout.Children.Clear();

            AddLocations();

        }

        //initializes content
        private void InitContent()
        {
            Refresh();
        }

        //adds all the location buttons
        private void AddLocations()
        {
            List<StorageLocation> locations = DatabaseHandler.GetDatabase().GetLocations();
            Console.WriteLine("Total locations: " + locations.Count());
            foreach (StorageLocation location in locations)
            {
                Action tapAction = () =>
                {
                    LocationPage newPage = new LocationPage(location);
                    MasterNavigationPage.ChangePage(newPage);

                };
               this.contentLayout.Children.Add(new LocationsPanelView(location, LocationImageWidth, LocationImageHeight, tapAction));
               this.contentLayout.Children.Add(new DividerLine());
            }

        }

        //refreshes the location count
        public void RefreshLocationCount()
        {
            locationCount.Text = "" + DatabaseHandler.GetDatabase().GetTotalLocations();
        }

        //refreshes the view
        public void Refresh()
        {
            RefreshLocations();
            RefreshLocationCount();
        }
    }
}