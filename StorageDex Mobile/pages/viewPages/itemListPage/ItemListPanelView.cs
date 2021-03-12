using Rg.Plugins.Popup.Services;
using StorageDex_Mobile.elements;
using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.interfaces;
using StorageDex_Mobile.lib.interfaces.toolbar;
using StorageDex_Mobile.lib.wrappers;
using StorageDex_Mobile.pages.miscPages;
using StorageDex_Mobile.pages.popupPages;
using StorageDex_Mobile.pages.popupPages.notification;
using StorageDex_Mobile.views;
using StorageDexLib;
using StorageDexLib.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.pages.viewPages.itemListPage
{
    class ItemListPanelView : ContentView, IRefreshable, IToolbarSearch, IToolbarDelete, IToolbarMenuDots
    {
        List<Item> items;
        StorageContainer container;
        ScrollView mainScrollLayout = new ScrollView();
        StackLayout mainLayout = new StackLayout();
        List<HighlightedItemPanelView> currentButtons = new List<HighlightedItemPanelView>(); 

        public ItemListPanelView(List<Item> itemsIn, StorageContainer container)
        {
            this.items = itemsIn;
            this.container = container;
            mainScrollLayout.Content = mainLayout;
            this.Content = mainScrollLayout;

            //set colors
            this.BackgroundColor = PageColors.secondaryColor;

            loadPanels();
        }

        private void loadPanels()
        {

            mainLayout.Children.Clear();
            items = container.GetItems();

            foreach (Item item in items)
            {

                Action onTap = () =>
                {
                    Navigation.PushAsync(new ItemDetailsPage(item));
                };

                HighlightedItemPanelView newPanel = new HighlightedItemPanelView(item, LocationsView.LocationImageWidth, LocationsView.LocationImageWidth, onTap);
                currentButtons.Add(newPanel);

                mainLayout.Children.Add(newPanel);


            }
        }

        //refreshes the view
        public void Refresh()
        {
            loadPanels();
        }

        //searches for a certain item
        public void Search()
        {
            Navigation.PushAsync(new SearchPage(items));
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


            //move container menu button
            Grid moveContainerGrid = menu.AddLabelAndImage("Move Container", "move_to_container");
            TapGestureRecognizer moveContainerGesture = new TapGestureRecognizer();

            ContainerWrapper containerWrapper = new ContainerWrapper();

            //to call when the container is selected
            Action callOnMoveSelected = () =>
            {
                if (containerWrapper.Container != null)
                {
                    Console.WriteLine("calling move");
                    MoveHighlightedItems(containerWrapper.Container);
                    PopupNavigation.Instance.PushAsync(new PopupTextNotification("Items moved"));
                    MasterNavigationPage.current.Refresh();
                }
            };


            moveContainerGesture.Tapped += (sen, e) =>
            {
                PopupNavigation.Instance.PopAsync();
                if (container.GetLocation().GetContainers().Count <= 1) //if there arent enough locations to move, display an error
                {
                    PopupNavigation.Instance.PushAsync(new PopupErrorNotification("You need more containers to do that"));
                }
                else
                {
                    Navigation.PushAsync(new ContainerSelectPage(containerWrapper, callOnMoveSelected, container.GetLocation().GetContainers(), new List<int>() { container.GetId() }));
                }


            };
            moveContainerGrid.GestureRecognizers.Add(moveContainerGesture);


            //delete item menu button
            Grid deleteItemGrid = menu.AddLabelAndImage("Delete Items", "trash");
            TapGestureRecognizer deleteItemGesture = new TapGestureRecognizer();

            deleteItemGesture.Tapped += (sen, e) =>
            {

                if (CanDelete()) //if can delete - delete, else give error messxage
                {
                    Delete();
                    PopupNavigation.Instance.PopAsync();

                }
                else
                {
                    PopupNavigation.Instance.PushAsync(new PopupErrorNotification("Unable to delete"));
                }


            };
            deleteItemGrid.GestureRecognizers.Add(deleteItemGesture);

            return menu;
        }

        //moves all the hightlighted items to the given container
        private void MoveHighlightedItems(StorageContainer con)
        {
            foreach (HighlightedItemPanelView button in currentButtons)
            {
                if (button.IsHighlighted()) //delete if highlighted
                {
                    Console.WriteLine("moving");
                    DatabaseHandler.GetDatabase().MoveItem(button.item.GetId(), con.GetId());

                }
            }

            Refresh();
            DataTools.Save();
        }


        //whether or not the menu can be opened
        public bool CanOpenMenu()
        {
            return IsAnyButtonHightlighted();
        }

        //if the page can delete anything - if anything is highlighted
        public bool CanDelete()
        {
            return IsAnyButtonHightlighted();
        }

        private bool IsAnyButtonHightlighted()
        {
            foreach (HighlightedItemPanelView button in currentButtons)
            {
                if (button.IsHighlighted())
                {
                    return true;
                }
            }

            return false;
        }




        //deletes the highlighted buttons
        public async void Delete()
        {
            bool choice = await MasterNavigationPage.current.CurrentPage.DisplayAlert("Are you sure?", "Deleting will remove all of the data forever", "Delete", "Cancel");
            if (choice)
            {
                foreach (HighlightedItemPanelView button in currentButtons)
                {
                    if (button.IsHighlighted()) //delete if highlighted
                    {
                        DatabaseHandler.GetDatabase().DeleteItem(button.item.GetId());

                    }
                }
                PopupNavigation.Instance.PushAsync(new PopupTextNotification("Item's deleted"));
                Refresh();
            }
        }


    }
}
