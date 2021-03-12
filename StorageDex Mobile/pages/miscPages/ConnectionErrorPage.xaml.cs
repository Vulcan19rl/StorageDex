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
    public partial class ConnectionErrorPage : ContentPage
    {
        public ConnectionErrorPage()
        {

            InitializeComponent();
            this.BackgroundColor = PageColors.secondaryColor;

            //init page
            this.title.FontSize = FontSizes.titleFont;
            this.title.TextColor = PageColors.textColorInverted;
            this.desc.FontSize = FontSizes.defaultSizeFont;
            this.desc.TextColor = PageColors.textColorInverted;
            this.exitButton.FontSize = FontSizes.defaultSizeFont;
            this.exitButton.TextColor = PageColors.textColorInverted;
            this.exitButton.BackgroundColor = Color.Transparent;
            this.reconnectButton.FontSize = FontSizes.defaultSizeFont;
            this.reconnectButton.TextColor = PageColors.textColorInverted;
            this.reconnectButton.BackgroundColor = Color.Transparent;
        }


    }
}