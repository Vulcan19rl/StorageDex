using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.interfaces;
using StorageDex_Mobile.lib.interfaces.toolbar;
using StorageDex_Mobile.pages.viewPages.itemListPage;
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
    public partial class ContainerPage : ContentPage, IRefreshable, IToolbarDelete
    {
        private int id;
        private StorageContainer container;
        private bool deleted = false; //whether or the page's container has been deleted

        public ContainerPage(StorageContainer container)
        {
            this.container = container;
            this.Title = container.GetName();
            this.id = container.GetId();

            InitializeComponent();

            this.optionsTable.Margin = new Thickness(15, 5, 15, 5);
            InitButtons();
            Refresh(); //the resfresh method also initializes the page



        }


        //initializes the button
        public void InitButtons()
        {
            detailsButton.Tapped += (sen, e) =>
            {

                Navigation.PushAsync(new ContainerDetailsPage(container));
            };

            itemsButton.Tapped += (sen, e) =>
            {

                Navigation.PushAsync(new ItemListPage(container));
            };


            detailsButton.SetText("Details");
            itemsButton.SetText("Items");
        }



        public void Refresh()
        {

            container = DatabaseHandler.GetDatabase().GetContainer(id, container.GetLocation());

            ;
            //set title
            this.Title = container.GetName();


            //set proportions
            this.imageFrame.HeightRequest = MasterNavigationPage.current.Height / 2.0;



            //init image
            if (ImageTools.HasImage(container))
            {
                if (ImageBaseHandler.current.isContainerCached(container.GetId()))
                {
                    SetImage(ImageBaseHandler.current.GetContainerImageSource(container.GetId()));
                }
                else
                {
                    SetImage(ImageTools.LoadImage(container));
                }

            }
            else //if no image give it the place holder image
            {

                SetImage("camera");
            }





            //set colors
            this.BackgroundColor = PageColors.secondaryColor;
        }


        //sets the image on the page
        private void SetImage(ImageSource source)
        {

            Image img = new Image() { Source = source };
            Grid.SetColumn(img, 0);
            img.VerticalOptions = LayoutOptions.FillAndExpand;
            img.HorizontalOptions = LayoutOptions.FillAndExpand;
            img.Aspect = Aspect.AspectFit;
            this.imageFrame.Content = img;
        }

        //sets the image on the page
        private void SetImage(string source)
        {
            Image img = new Image() { Source = source };
            Grid.SetColumn(img, 0);
            img.VerticalOptions = LayoutOptions.FillAndExpand;
            img.HorizontalOptions = LayoutOptions.FillAndExpand;
            img.Aspect = Aspect.AspectFit;
            this.imageFrame.Content = img;
        }

        //whether or not the location can be deleted
        public bool CanDelete()
        {
            return !deleted;
        }

        public async void Delete()
        {


            //displays a pop up asking if the user is sure
            bool choice = await DisplayAlert("Are you sure?", "Deleting this container will delete all the items inside of the container", "Delete", "Cancel");
            if (choice)
            {
                DatabaseHandler.GetDatabase().DeleteContainer(container.GetId());
                DataTools.Save();
                await Navigation.PopAsync();
                deleted = true;
            }
            else
            {
                deleted = false;
            }








        }

    }
}