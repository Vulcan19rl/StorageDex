using Microcharts;
using SkiaSharp;
using StorageDex_Mobile.lib;
using StorageDex_Mobile.lib.interfaces;
using StorageDex_Mobile.pages.newPages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace StorageDex_Mobile.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardView : ContentView, IRefreshable
    {
        LocationsView locationsView = null;
        public DashboardView()
        {
            Console.WriteLine("dash init");
            InitializeComponent();

            PostInit();


            //set colors
            title.TextColor = PageColors.textColor;
            itemsCount.TextColor = PageColors.textColor;
            titleFrame.BackgroundColor = PageColors.lighterSecondary;

            title.FontSize = FontSizes.subTitleFont;
            itemsCount.FontSize = FontSizes.titleFont;

            titleLayout.Margin = new Thickness(0, 10, 0, 90);


            Console.WriteLine("dash inited");
        }

        //post initialization
        private void PostInit()
        {
            
            RefreshTotalItemsCount();

            Console.WriteLine("locations init");
            //add locations view
            locationsView = new LocationsView();
            Console.WriteLine("locations inited");
            contentPanel.Children.Add(locationsView);




        }

        //refreshes the view
        public void Refresh()
        {
            RefreshTotalItemsCount();
            locationsView.Refresh();
            RefreshGraph();
        }

        //refreshes the graph
        public void RefreshGraph()
        {


            //setup chart
            List<Microcharts.ChartEntry> entries = new List<Microcharts.ChartEntry>();
            List<int> values = DatabaseHandler.GetDatabase().GetCurrentTracker().GetItemTracker().GetArray().ToList();
            Console.WriteLine("Values: ");
            foreach (int val in values)
            {
                Console.WriteLine(val);
                Microcharts.ChartEntry newEntry = new Microcharts.ChartEntry(val)
                {
                    Color = new SKColor(PageColors.primaryColor.R, PageColors.primaryColor.G, PageColors.primaryColor.B, PageColors.primaryColor.A)


                };

                entries.Add(newEntry);
            }

            lineChart.Chart = new LineChart()
            {
                Entries = entries,
                BackgroundColor = SKColors.Transparent,
                Margin = 0,
                
                
                

            };


     







        }

        //refreshes the total number of items displayer
        public void RefreshTotalItemsCount()
        {
     
            itemsCount.Text = DatabaseHandler.GetDatabase().GetTotalStoredItems().ToString(); //get the total number of items from the database and set the text
           
        }



    }
}