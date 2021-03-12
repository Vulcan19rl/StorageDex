using StorageDex_Mobile.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StorageDex_Mobile.pages.miscPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagePage : ContentPage
    {
        /**
         * an image page is a page that just displays an image
         */
        public ImagePage(ImageSource imageSourceIn)
        {
            InitializeComponent();
            mainImage.Source = imageSourceIn;

            //set colors
            this.BackgroundColor = PageColors.secondaryColor;
        }
    }
}