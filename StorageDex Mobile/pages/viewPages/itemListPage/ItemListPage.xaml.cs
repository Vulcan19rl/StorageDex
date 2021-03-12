using StorageDex_Mobile.lib.interfaces;
using StorageDex_Mobile.lib.interfaces.toolbar;
using StorageDex_Mobile.pages.newPages;
using StorageDexLib;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StorageDex_Mobile.pages.viewPages.itemListPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemListPage : ContentPage, IToolbarAdd, IRefreshable, IToolbarSearch, IToolbarMenuDots, IToolbarSwitchView
    {

        StorageContainer container;
        View currentView;
        string viewMode = "grid";

        public ItemListPage(StorageContainer containerIn)
        {
            this.Title = containerIn.GetName() + "'s Items";
            this.container = containerIn;
            InitializeComponent();
            Init();

            this.Appearing += (sen, e) => Refresh();
        }


        //initializes the page
        private void Init()
        {
            SetToGridMode();
        }

        public void Refresh()
        {
            ((IRefreshable)currentView).Refresh();
        }

        //toggles the view mode from grid to list and back
        private void ToggleView()
        {
            if (viewMode == "grid")
            {
                viewMode = "panel";
                SetToPanelMode();
            }
            else
            {
                viewMode = "grid";
                SetToGridMode();
            }
        }

        //returns the item list from the parent storage type
        private List<Item> GetItems()
        {
            return container.GetItems();
        }

        //sets the view mode to grid
        private void SetToGridMode()
        {

            currentView = new ItemListGridView(GetItems(), container);
            this.Content = currentView;
        }

        //sets the panel to grid
        private void SetToPanelMode()
        {
            currentView = new ItemListPanelView(GetItems(), container);
            this.Content = currentView;

        }

        //searches for an item
        public void Search()
        {
            ((IToolbarSearch)currentView).Search();
        }


        public void Add()
        {
            Navigation.PushAsync(new NewItemPage(container));
        }

        public bool CanAdd()
        {
            return true;
        }

        public bool CanDelete()
        {

            return ((IToolbarDelete)currentView).CanDelete();

        }

        public void Delete()
        {
            ((IToolbarDelete)currentView).Delete();
        }

        public void OpenMenu()
        {
            ((IToolbarMenuDots)currentView).OpenMenu();
        }

        public bool CanOpenMenu()
        {
            return ((IToolbarMenuDots)currentView).CanOpenMenu();
        }

        //switches the current view
        public void SwitchView()
        {
            ToggleView();
            MasterNavigationPage.current.RefreshButtons();
        }
    }
}