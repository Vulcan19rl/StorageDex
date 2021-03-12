using FFImageLoading.Forms;
using StorageDex_Mobile.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StorageDex_Mobile.pages.miscPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        Task loadingTask;

        public LoadingPage(Task toLoad, Action onLoad)
        {
            InitializeComponent();
            this.BackgroundColor = PageColors.secondaryColor;
            CachedImage loadingImage = new CachedImage()
            {
                Source = "loading"
            };
            this.Content = loadingImage;

            loadingTask = new Task(() => {
                toLoad.Start();
                toLoad.Wait();
                Dispatcher.BeginInvokeOnMainThread(onLoad);

            });
            

        }

        public void StartLoading()
        {
            loadingTask.Start();
        }
    }
}