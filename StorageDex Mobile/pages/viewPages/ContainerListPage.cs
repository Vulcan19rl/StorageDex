

using Android.Widget;
using MultiGestureViewPlugin;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using StorageDex_Mobile.elements;
using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.interfaces;
using StorageDex_Mobile.lib.interfaces.toolbar;
using StorageDex_Mobile.lib.wrappers;
using StorageDex_Mobile.pages.miscPages;
using StorageDex_Mobile.pages.newPages;
using StorageDex_Mobile.pages.popupPages;
using StorageDex_Mobile.pages.popupPages.notification;
using StorageDexLib;
using StorageDexLib.interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Markup;

namespace StorageDex_Mobile.pages.viewPages
{
    partial class ContainerListPage : GridImageButtonPage, IToolbarAdd, IRefreshable, IToolbarSearch, IToolbarMenuDots
    {
        /**
        * a page that displays all the containers within a location
        * displays them in a grid image pattern
        */


        StorageLocation location;
        List<HighlightedContainerImageButton> currentButtons;



        public ContainerListPage(StorageLocation location) : base()
        {
            this.location = location;
            this.Title = "Containers";


          
            this.Appearing += (sen, e) => Refresh();
            
            LoadImages();

        }

        //add a  new container to the location
        public void Add()
        {
            base.Navigation.PushAsync(new NewContainerPage(location));
        }

        //loads the images onto the page
        private void LoadImages()
        {

            base.Clear();

            currentButtons = new List<HighlightedContainerImageButton>();


            foreach (StorageContainer container in location.GetContainers())
            {

 
                HighlightedContainerImageButton buttonToAdd;

                if (ImageTools.HasImage(container))
                {
                    ImageSource source;
                    if (ImageBaseHandler.current.isContainerCached(container.GetId()))
                    {
                        source = ImageBaseHandler.current.GetContainerImageSource(container.GetId());
                    }
                    else
                    {
                        source = ImageTools.LoadImage(container);
                    }

                    buttonToAdd = new HighlightedContainerImageButton(container, source);

                }
                else
                {
                    buttonToAdd = new HighlightedContainerImageButton(container);

                }

                buttonToAdd.BackgroundColor = Color.Transparent;
                buttonToAdd.Margin = 5;
                buttonToAdd.Tapped += (sen, e) =>
                {

                    Navigation.PushAsync(new ContainerPage(container));
                };



                buttonToAdd.LongPressed += (sen, e) =>
                {


                    MasterNavigationPage.current.RefreshButtons(); //force a button refresh
                };

                currentButtons.Add(buttonToAdd);
                base.AddImageButtonEquivalent(buttonToAdd);









            }
        }



        //refreshes the page
        public void Refresh()
        {
            LoadImages(); //loads all the images again
        }

        //opens a new search page for the containers in the location
        public void Search()
        {
            Navigation.PushAsync(new SearchPage(location.GetContainers()));
        }

        //returns true if there is a highlighted button
        private bool IsAnyButtonHightlighted()
        {
            foreach (HightlightedImageButton button in currentButtons)
            {
                if (button.IsHighlighted())
                {
                    return true;
                }
            }

            return false;
        }

        //opens the menu - allows for mass editing and movement of containers
        public void OpenMenu()
        {
            PopupNavigation.Instance.PushAsync(MakeMenuPage());
        }


        //makes the menu page to open
        private PopupPageMenu MakeMenuPage()
        {
            PopupPageMenu menu = new PopupPageMenu();


            //move location menu button
            Grid moveLocationGrid = menu.AddLabelAndImage("Move Location", "move_to_location");
            TapGestureRecognizer moveLocationGesture = new TapGestureRecognizer();

            LocationWrapper locationWrapper = new LocationWrapper();

            //to call when the location is selected
            Action callOnMoveSelected = () =>
            {
                if (locationWrapper.Location != null)
                {
                    MoveHighlightedContainers(locationWrapper.Location);
                    PopupNavigation.Instance.PushAsync(new PopupTextNotification("Containers moved"));
                    location = DatabaseHandler.GetDatabase().GetLocation(location.GetId());
                    MasterNavigationPage.current.Refresh();
                }
            };


            moveLocationGesture.Tapped += (sen, e) =>
            {
                PopupNavigation.Instance.PopAsync();
                if (DatabaseHandler.GetDatabase().GetLocations().Count <= 1) //if there arent enough locations to move, display an error
                {
                    PopupNavigation.Instance.PushAsync(new PopupErrorNotification("You need more locations to do that"));
                }
                else
                {
                    Navigation.PushAsync(new LocationSelectorPage(locationWrapper, callOnMoveSelected, new List<int>() { location.GetId() }));
                }


            };
            moveLocationGrid.GestureRecognizers.Add(moveLocationGesture);


            //delete item menu button
            Grid deleteContainerGrid = menu.AddLabelAndImage("Delete Containers", "trash");
            TapGestureRecognizer deleteContainerGesture = new TapGestureRecognizer();

            deleteContainerGesture.Tapped += (sen, e) =>
            {

                if (CanDelete()) //if can delete - delete, else give error messxage
                {
                    Delete();
                    PopupNavigation.Instance.PopAsync();
                    PopupNavigation.Instance.PushAsync(new PopupTextNotification("Containers's deleted"));
                }
                else
                {
                    PopupNavigation.Instance.PushAsync(new PopupErrorNotification("Unable to delete"));
                }


            };
            deleteContainerGrid.GestureRecognizers.Add(deleteContainerGesture);


            return menu;
        }


        //whether or not the menu can be opened
        public bool CanOpenMenu()
        {
            return IsAnyButtonHightlighted();
        }

        //moves all the hightlighted containers to the given location
        private void MoveHighlightedContainers(StorageLocation location)
        {
            foreach (HighlightedContainerImageButton button in currentButtons)
            {
                if (button.IsHighlighted()) //delete if highlighted
                {
                    DatabaseHandler.GetDatabase().MoveContainer(button.Container.GetId(), location.GetId());

                }
            }

            Refresh();
            DataTools.Save();
        }

        //if the page can delete anything - if anything is highlighted
        public bool CanDelete()
        {
            return IsAnyButtonHightlighted();
        }



        //deletes the highlighted buttons
        public async void Delete()
        {
            bool choice = await DisplayAlert("Are you sure?", "Deleting this container will delete all the items inside of the location", "Delete", "Cancel");
            if (choice)
            {
                foreach (HighlightedContainerImageButton button in currentButtons)
                {
                    if (button.IsHighlighted()) //delete if highlighted
                    {
                        DatabaseHandler.GetDatabase().DeleteContainer(button.Container.GetId());

                    }
                }

                Refresh();
            }


        }


        public bool CanAdd()
        {
            return true;
        }


    }



}
