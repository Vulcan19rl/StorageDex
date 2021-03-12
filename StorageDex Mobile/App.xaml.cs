using Plugin.Connectivity;
using Plugin.Media;

using StorageDex_Mobile.lib;
using StorageDex_Mobile.pages.miscPages;
using StorageDexLib;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StorageDex_Mobile
{
    public partial class App : Application
    {

        public HomePage homePage;
        public MasterNavigationPage navPage;
        private static bool enterprise = false;


    
        

        public App()
        {
           
            InitializeComponent();
            LoadingPage loadingPage;

            Task load = new Task(() =>
            {
                PreInit();
                DataTools.Load();
                if (enterprise)
                {
             
                    ((RemoteStorageDatabase)DatabaseHandler.GetDatabase()).RunOnConnectionError(() =>
                    {

                        this.MainPage = new ConnectionErrorPage();
                    });
                }
                homePage = new HomePage();
                homePage.Refresh();

                
                
            });
            Action finishedLoading = () =>
            {
                navPage = new MasterNavigationPage(homePage);
                this.MainPage = navPage;
            };

         

            loadingPage = new LoadingPage(load, finishedLoading);
           

            this.MainPage = loadingPage;


            loadingPage.StartLoading();


            if (CrossConnectivity.Current.IsConnected)
            {
                Console.WriteLine("connected");
            }
            else
            {
                Console.WriteLine("not connected");
            }


        }


        //pre initialization
        private void PreInit()
        {

            //init the database
            DatabaseHandler.InitDatabase();
            ImageBaseHandler.InitImageBaseHanlder();

           


        }

        //returns whether or not the app is the enterprise version
        public static bool IsEnterprise()
        {
            return enterprise;
        }

   

        protected override void OnStart()
        {

            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
