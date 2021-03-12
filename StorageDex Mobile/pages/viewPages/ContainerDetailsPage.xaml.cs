using StorageDex_Mobile.elements;
using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.interfaces;
using StorageDex_Mobile.lib.interfaces.toolbar;
using StorageDex_Mobile.pages.newPages;
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
public partial class ContainerDetailsPage : ContentPage, IToolbarEdit, IRefreshable
{
        StorageContainer container;
        int id;
        Image img = new Image();



        public ContainerDetailsPage(StorageContainer container)
        {
            this.container = container;
            this.id = container.GetId();

       
       


            InitializeComponent();
            this.Appearing += (sen, e) => Refresh();
            this.imageFrame.Content = img;
            img.VerticalOptions = LayoutOptions.FillAndExpand;
            img.HorizontalOptions = LayoutOptions.FillAndExpand;
            img.Aspect = Aspect.AspectFit;

            //set colors
            this.notesLabel.TextColor = PageColors.textColor;




            Refresh();
        }


        //sets image
        private void SetImage(ImageSource source)
        {
            img.Source = source;


        }

        //sets the image on the page
        private void SetImage(string source)
        {

            img.Source = source;


        }



        //sets the notes text
        private void SetNotes()
        {
            this.notesLabel.Text = container.GetNotes();
        }

        public async void Edit()
        {

            NewContainerPage editPage = new NewContainerPage(container.GetName(), container.GetNotes(), container.GetTags(), container.GetId(), container, container.GetLocation());
            await Navigation.PushAsync(editPage);
        }

        public bool CanEdit()
        {
            return true; //can always edit location
        }

        public void Refresh()
        {
            container = DatabaseHandler.GetDatabase().GetContainer(id, container.GetLocation());

            //set title
            this.Title = container.GetName();


            //set proportions
            this.imageFrame.HeightRequest = MasterNavigationPage.current.Height / 2.0;



            //set text
            SetNotes();


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
                Console.WriteLine("no image");
                SetImage("camera");
            }

            //init tags
            tagDisplay.ClearTags();
            if (container.GetTags().Count != 0 && !(container.GetTags().Count == 1 && container.GetTags()[0] == ""))
            { tagDisplay.AddTagToBatchWithoutClick(container.GetTags()); }

            //remove uneeded lines
            if(container.GetNotes().Trim() == "")
            {
                notesLabel.Hide();
                notesDivider.Hide();
            }
            else
            {
                notesLabel.Show();
                notesDivider.Show();
            }






            //set colors
            this.BackgroundColor = PageColors.secondaryColor;
        }


    }
}