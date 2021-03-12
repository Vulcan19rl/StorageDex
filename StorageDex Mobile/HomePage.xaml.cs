
using StorageDex_Mobile.elements;
using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.interfaces;
using StorageDex_Mobile.lib.interfaces.toolbar;
using StorageDex_Mobile.pages.miscPages;
using StorageDex_Mobile.pages.newPages;
using StorageDex_Mobile.views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StorageDex_Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage, IToolbarSearch, IRefreshable, IToolbarAdd, IToolbarSettings
    {
        public HomePage()
        {
            InitializeComponent();

            
            FirstTimeInit();

            //set colors
            this.BackgroundColor = PageColors.secondaryColor;
            this.Appearing += (sen, e) => Refresh();
           

        }

        //first time initialization
        public void FirstTimeInit()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                RefreshableStackLayout newContent = new RefreshableStackLayout();
                newContent.Children.Add(new DashboardView());
                newContent.Children.Add(new AdMobView());
                this.Content = newContent;


            }
            else
            {
                this.Content = new DashboardView();
            }
            
        }

        //refreshes the current view
        public void Refresh()
        {
            if (typeof(IRefreshable).IsInstanceOfType(this.Content)) //if current page can be refreshed, refresh it
            {

                ((IRefreshable)this.Content).Refresh();
            }
        }

        //opens new location page
        public void Add()
        {
            Navigation.PushAsync(new NewLocationPage());
        }

        public bool CanAdd()
        {
            return true;
        }


        //opens a new search page
        public async void Search()
        {
            await Navigation.PushAsync(new SearchPage(DatabaseHandler.GetDatabase().GetLocations()));
        }

        //opens a settings page
        public async void OpenSettings()
        {
            await Navigation.PushAsync(new SettingsPage());
        }


    }
}
