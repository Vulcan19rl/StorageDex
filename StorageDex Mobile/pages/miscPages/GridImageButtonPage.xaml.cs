using Rg.Plugins.Popup.Pages;
using StorageDex_Mobile.lib;
using StorageDex_Mobile.views;
using StorageDexLib;
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
    public partial class GridImageButtonPage : ContentPage
    {
        GridImageButtonView contentView;
        public GridImageButtonPage() 
        {
            contentView = new GridImageButtonView();
            this.Content = contentView;
        }

        public void AddImageButtonEquivalent(View buttonIn)
        {
            contentView.AddImageButtonEquivalent(buttonIn);
        }

        public void AddImageButton(ImageButton buttonIn)
        {
            contentView.AddImageButton(buttonIn);
        }

        public void Clear()
        {
            contentView.Clear();
        }
    }
}