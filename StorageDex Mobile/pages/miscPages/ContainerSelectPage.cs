using Android.Locations;
using StorageDex_Mobile.elements;
using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.wrappers;
using StorageDex_Mobile.views;
using StorageDexLib;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.pages.miscPages
{
    // page to select a container
    //when a container is selected it will be put inside the given container wrapper

    class ContainerSelectPage : ContentPage
    {

        ScrollView mainScrollLayout = new ScrollView();
        StackLayout mainLayout = new StackLayout();

        public  ContainerSelectPage(ContainerWrapper containerWrapper, Action callOnFinished, List<StorageContainer> containers, List<int> idsToSkip = null)
        {
            this.BackgroundColor = PageColors.secondaryColor;
            this.Content = mainScrollLayout;
            mainScrollLayout.Content = mainLayout;

            this.Title = "Choose a location";




            foreach (StorageContainer container in containers)
            { 
                if (idsToSkip != null && idsToSkip.Contains(container.GetId()))
                {

                }
                else
                {
                    Action onTap = () =>
                    {
                        containerWrapper.Container = container;
                        callOnFinished.Invoke();
                        Navigation.PopAsync();
                     
                    };
                    mainLayout.Children.Add(new ContainerPanelView(container, LocationsView.LocationImageWidth, LocationsView.LocationImageWidth, onTap));
                }

            }
        }
    }
}
