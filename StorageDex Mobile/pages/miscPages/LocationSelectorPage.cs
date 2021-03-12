using Android.Telecom;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using StorageDex_Mobile.elements;
using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.wrappers;
using StorageDex_Mobile.views;
using StorageDexLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.pages.popupPages
{

    // page to select a location
    //when a location is selected it will be put inside the given location wrapper
    class LocationSelectorPage : ContentPage
    {
        ScrollView mainScrollLayout = new ScrollView();
        StackLayout mainLayout = new StackLayout();

        public LocationSelectorPage(LocationWrapper locationWrapper, Action callOnFinished, List<int> idsToSkip = null)
        {
            this.BackgroundColor = PageColors.secondaryColor;
            this.Content = mainScrollLayout;
            mainScrollLayout.Content = mainLayout;

            this.Title = "Choose a location";




            foreach (StorageLocation location in DatabaseHandler.GetDatabase().GetLocations().ToList())
            {
                if(idsToSkip != null && idsToSkip.Contains(location.GetId()))
                {

                }
                else
                {
                    Action onTap = () =>
                    {
                        locationWrapper.Location = location;
                        Navigation.PopAsync();
                        callOnFinished.Invoke();
                    };
                    mainLayout.Children.Add(new LocationsPanelView(location, LocationsView.LocationImageWidth, LocationsView.LocationImageWidth, onTap));
                }
               
            }
        }

 
    }
}
