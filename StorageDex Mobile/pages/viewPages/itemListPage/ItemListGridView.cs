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
using System;
using System.Collections.Generic;

using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.pages.viewPages.itemListPage
{
    class ItemListGridView : GridImageButtonView, IToolbarDelete, IRefreshable, IToolbarSearch, IToolbarMenuDots
    {
        List<Item> items;
        List<HighlightedItemImageButton> currentButtons;
        StorageContainer container;
       


        public ItemListGridView(List<Item> itemsIn, StorageContainer container) : base()
        {
            this.items = itemsIn;
            this.container = container;
           


            LoadButtons();
        }




        private void LoadButtons()
        {

            base.Clear();
            currentButtons = new List<HighlightedItemImageButton>();
            items = container.GetItems();
            foreach (Item item in items)
            {

                HighlightedItemImageButton buttonToAdd;

                if (ImageTools.HasImage(item))
                {
                    ImageSource source;
                    if (ImageBaseHandler.current.isItemCached(item.GetId()))
                    {
                        source = ImageBaseHandler.current.GetItemImageSource(item.GetId());
                    }
                    else
                    {
                        source = ImageTools.LoadImage(item);
                    }

                    buttonToAdd = new HighlightedItemImageButton(item, source);

                }
                else
                {
                    buttonToAdd = new HighlightedItemImageButton(item);

                }

                buttonToAdd.BackgroundColor = Color.Transparent;
                buttonToAdd.Margin = 5;
                buttonToAdd.Tapped += (sen, e) =>
                {

                    Navigation.PushAsync(new ItemDetailsPage(item));
                };



                buttonToAdd.LongPressed += (sen, e) =>
                {


                    MasterNavigationPage.current.RefreshButtons(); //force a button refresh
                };

                currentButtons.Add(buttonToAdd);
                base.AddImageButtonEquivalent(buttonToAdd);
            }


        }

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


            //move container menu button
            Grid moveContainerGrid = menu.AddLabelAndImage("Move Container", "move_to_container");
            TapGestureRecognizer moveContainerGesture = new TapGestureRecognizer();

            ContainerWrapper containerWrapper = new ContainerWrapper();

            //to call when the container is selected
            Action callOnMoveSelected = () =>
            {
                if (containerWrapper.Container != null)
                {
                    MoveHighlightedItems(containerWrapper.Container);
                    PopupNavigation.Instance.PushAsync(new PopupTextNotification("Items moved"));
                    container = DatabaseHandler.GetDatabase().GetContainer(container.GetId(), container.GetLocation());
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
            foreach (HighlightedItemImageButton button in currentButtons)
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

        //refreshes the container
        public void Refresh()
        {
            LoadButtons(); //loads all the images again
        }




        //deletes the highlighted buttons
        public async void Delete()
        {
            bool choice = await MasterNavigationPage.current.CurrentPage.DisplayAlert("Are you sure?", "Deleting will remove all of the data forever", "Delete", "Cancel");
            if (choice)
            {
                foreach (HighlightedItemImageButton button in currentButtons)
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

        //searches for a certain item
        public void Search()
        {
            Navigation.PushAsync(new SearchPage(items));
        }
    }
}
