using Java.Sql;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using StorageDex_Mobile.lib;
using StorageDexLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StorageDex_Mobile.pages.miscPages
{
    /**
     * a page that allows settings to be changed and viewed
     */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            //set colors
            this.BackgroundColor = PageColors.secondaryColor;

            //initialize the buttons
            exportDataButtonCell.SetText("Export Data");
            exportDataButtonCell.Tapped += (sen, e) => ExportData();

            importDataButtonCell.SetText("Import Data");
            importDataButtonCell.Tapped += (sen, e) => ImportData();
        }

        //gets called when the export data button is pressed. Exports the saved data to wherever the user chooses
        private async void ExportData()
        {
            //if enterprise don't run
            if (App.IsEnterprise())
            {
                return;
            }

            //share the data
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = String.Join("\n", ((StorageDatabase)DatabaseHandler.GetDatabase()).Export()),
                Title = "Share Text"
            });
        }

        //gets called when the import data button is pressed. imports the data in the selected file to the app
        private async void ImportData()
        {

            //if enterprise don't run
            if (App.IsEnterprise())
            {
                return;
            }


            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking

                string filePath = fileData.FilePath;
                string contents = System.Text.Encoding.UTF8.GetString(fileData.DataArray);

                Console.WriteLine(filePath);
                List<string> data = File.ReadAllLines(filePath).ToList();
                DatabaseHandler.InitDatabase(); //create a fresh database
                DatabaseHandler.Import(data); //import the data
                DataTools.Save(); //saving data
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }

    }


  
}