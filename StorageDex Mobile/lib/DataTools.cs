using PCLStorage;
using StorageDexLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageDex_Mobile.lib
{
    class DataTools
{
        //responsible for saving and loading data
        public static async void Save()
        {
            //if enterprise don't run
            if (App.IsEnterprise())
            {
                return;
            }
            Console.WriteLine("[DataTools] Saving");
            //get write data

            List<string> data = ((StorageDatabase)DatabaseHandler.GetDatabase()).Export();
            foreach(string s in data)
            {
                Console.WriteLine(s);
            }

            string dataString = String.Join("\n", data);

            //write file
            IFolder rootDirectory = FileSystem.Current.LocalStorage;
            IFile dataFile = await rootDirectory.CreateFileAsync(Directories.dataPath, CreationCollisionOption.OpenIfExists);
            await dataFile.WriteAllTextAsync(dataString);
            Console.WriteLine("[DataTools] Saved");

        }

        //Loads the data from file 
        public static async void Load()
        {
            //if enterprise don't run
            if (App.IsEnterprise())
            {
                return;
            }
            Console.WriteLine("[DataTools] Loading");
            //read file
            IFolder rootDirectory = FileSystem.Current.LocalStorage;
            IFile dataFile = await rootDirectory.CreateFileAsync(Directories.dataPath, CreationCollisionOption.OpenIfExists);
            string dataString = await dataFile.ReadAllTextAsync();
            List<string> data = dataString.Split('\n').ToList();
            DatabaseHandler.Import(data);
            Console.WriteLine("[DataTools] Loaded");
        }
}
}
