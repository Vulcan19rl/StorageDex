using Android.Content.Res;
using Android.Gestures;
using Android.Locations;
using Android.Widget;
using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.interfaces;
using StorageDex_Mobile.lib.interfaces.toolbar;
using StorageDex_Mobile.pages.miscPages;
using StorageDex_Mobile.pages.newPages;
using StorageDexLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StorageDex_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterNavigationPage : NavigationPage
    {
        public static MasterNavigationPage current;
        public MasterNavigationPage(Page rootPage) : base(rootPage)
        {
            InitializeComponent();
            current = this;

            //set colors
            this.BarBackgroundColor = PageColors.primaryColor;

            Refresh();
            this.Popped += (sen, e) => Refresh();
            this.Pushed += (sen, e) => Refresh();
            this.PoppedToRoot += (sen, e) => Refresh();


       
           









        }

        public async void PopWithRefresh()
        {
            List<Page> stack = Navigation.NavigationStack.ToList();
            if (stack.Count >= 2)
            {
                Page toRefresh = stack[stack.Count - 2];
                if (typeof(IRefreshable).IsInstanceOfType(toRefresh)) //if current page can be refreshed, refresh it
                {

                    ((IRefreshable)toRefresh).Refresh();
                }

                await Navigation.PopAsync();
                
            }
        }




        public async void PopToRootWithRefresh()
        {
            List<Page> stack = Navigation.NavigationStack.ToList();
            if (stack.Count >= 2)
            {
                Page toRefresh = stack[0];
                if (typeof(IRefreshable).IsInstanceOfType(toRefresh)) //if current page can be refreshed, refresh it
                {

                    ((IRefreshable)toRefresh).Refresh();
                }

                await Navigation.PopToRootAsync();

            }
        }








        //refreshes everything on the page and navigation bar
        public void Refresh()
        {

            RefreshPage();
            RefreshButtons();

        }

        public void RefreshPage()
        {

            if (typeof(IRefreshable).IsInstanceOfType(this.CurrentPage)) //if current page can be refreshed, refresh it
            {

                ((IRefreshable)this.CurrentPage).Refresh();
            }
        }





        public void RefreshButtons()
        {
            ToolbarItems.Clear();

  


            if (typeof(IToolbarEdit).IsInstanceOfType(this.CurrentPage))
            {
                if (((IToolbarEdit)this.CurrentPage).CanEdit())
                {
                    ToolbarItems.Add(new ToolbarItem("Edit", "edit", () =>
                    {
                        ((IToolbarEdit)this.CurrentPage).Edit();
                    }));
                }

            }


            if (typeof(IToolbarAdd).IsInstanceOfType(this.CurrentPage) && ((IToolbarAdd)this.CurrentPage).CanAdd())
            {
                ToolbarItems.Add(new ToolbarItem("Add", "add", () =>
                {
                    ((IToolbarAdd)this.CurrentPage).Add();
                }));
            }


            if (typeof(IToolbarSettings).IsInstanceOfType(this.CurrentPage))
            {
                ToolbarItems.Add(new ToolbarItem("Settings", "settings", () =>
                {
                    ((IToolbarSettings)this.CurrentPage).OpenSettings();
                }));

            }

            if (typeof(IToolbarSwitchView).IsInstanceOfType(this.CurrentPage))
            {
                ToolbarItems.Add(new ToolbarItem("Switch View", "switch_view", () =>
                {
                    ((IToolbarSwitchView)this.CurrentPage).SwitchView();


                }));
            }



            if (typeof(IToolbarSearch).IsInstanceOfType(this.CurrentPage))
            {
                ToolbarItems.Add(new ToolbarItem("Search", "search", () =>
                {
                    ((IToolbarSearch)this.CurrentPage).Search();


                }));
            }



            if (typeof(IToolbarDelete).IsInstanceOfType(this.CurrentPage) && ((IToolbarDelete)this.CurrentPage).CanDelete())
            {
                ToolbarItems.Add(new ToolbarItem("Trash", "trash", () =>
                {
                    IToolbarDelete currentPageDeletable = (IToolbarDelete)this.CurrentPage;
                    if (currentPageDeletable.CanDelete()) //keep trying to delete until everything is deleted
                    {
                        currentPageDeletable.Delete();
                    }
                    MasterNavigationPage.current.Refresh(); //then refresh
                }));
            }

            if (typeof(INewPage).IsInstanceOfType(this.CurrentPage))
            {
                //if its a INewPage give it the camera and attach buttons

                ToolbarItems.Add(new ToolbarItem("Camera", "camera", () =>
                {
                    //changes the current page to the search page
                    ((INewPage)this.CurrentPage).TakePhoto();


                }));

                ToolbarItems.Add(new ToolbarItem("Attach", "attach", () =>
                {
                    ((INewPage)this.CurrentPage).AttachPhoto();


                }));
            }

            if (typeof(IConfirmable).IsInstanceOfType(this.CurrentPage))
            {
                ToolbarItems.Add(new ToolbarItem("Checkmark", "checkmark", () =>
                {

                    ((IConfirmable)this.CurrentPage).Confirm();


                }));
            }

            if (typeof(IToolbarMenuDots).IsInstanceOfType(this.CurrentPage) && ((IToolbarMenuDots)this.CurrentPage).CanOpenMenu())
            {
                ToolbarItems.Add(new ToolbarItem("Open Menu", "menu_dots", () =>
                {
                    ((IToolbarMenuDots)this.CurrentPage).OpenMenu();
                }));
            }



        }

        //changes the current navigation page
        public static async void ChangePage(Page pageIn)
        {
            await current.Navigation.PushAsync(pageIn);
        }








    }


}