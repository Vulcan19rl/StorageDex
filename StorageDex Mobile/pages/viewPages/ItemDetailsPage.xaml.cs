using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.interfaces;
using StorageDex_Mobile.lib.interfaces.toolbar;
using StorageDex_Mobile.pages.newPages;
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
    public partial class ItemDetailsPage : ContentPage, IToolbarEdit, IRefreshable
    {

        Item item;
        int id;
        Image img = new Image();

        public ItemDetailsPage(Item item)
        {
            this.item = item;
            this.id = item.GetId();





            InitializeComponent();

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
            this.notesLabel.Text = item.GetNotes();
        }

        public async void Edit()
        {

            NewItemPage editPage = new NewItemPage(item.GetName(), item.GetNotes(), item.GetTags(), item.GetId(), item, item.GetContainer());
            await Navigation.PushAsync(editPage);
        }

        public bool CanEdit()
        {
            return true; //can always edit location
        }

        public void Refresh()
        {
            item = DatabaseHandler.GetDatabase().GetItem(id);

            //set title
            this.Title = item.GetName();


            //set proportions
            this.imageFrame.HeightRequest = MasterNavigationPage.current.Height / 2.0;



            //set text
            SetNotes();

            if(item.GetBarcode() != "")
            {
                this.codeDivider.Show();
                this.codeLabel.Show();
                this.codeLabel.TextColor = PageColors.textColor;
                this.codeLabel.Text = item.GetBarcode();
            }
            else
            {
                this.codeDivider.Hide();
                this.codeLabel.Hide();
            }
           


            //init image

            if (ImageTools.HasImage(item))
            {

                
                if (ImageBaseHandler.current.isItemCached(item.GetId()))
                {
                    SetImage(ImageBaseHandler.current.GetItemImageSource(item.GetId()));
                }
                else
                {
                    SetImage(ImageTools.LoadImage(item));
                }

            }
            else //if no image give it the place holder image
            {
                Console.WriteLine("no image");
               
                SetImage("camera");
            }

            //init tags
            tagDisplay.ClearTags();

            if (item.GetTags().Count != 0 && !(item.GetTags().Count == 1 && item.GetTags()[0] == ""))
            { tagDisplay.AddTagToBatchWithoutClick(item.GetTags()); }


            //remove uneeded lines
            if (item.GetNotes().Trim() == "")
            {
                notesLabel.Hide();
                notesDivider.Hide();
                notesDiverTop.Hide();
            }
            else
            {
                notesLabel.Show();
                notesDivider.Show();
                notesDiverTop.Show();
            }




            //set colors
            this.BackgroundColor = PageColors.secondaryColor;
        }

    }
}