using StorageDexLib;
using StorageDexLib.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StorageDex_Mobile.lib
{
    public class DatabaseHandler
    {
        /**
         * stores and handles the database
         */

        private static IStorageDatabase Database;
        private static string testIp = "192.168.0.17";
        private static int testPort = 19032;

        //initializes the current database into a static field
        //run on app startup
        public  static void InitDatabase()
        {
            if (App.IsEnterprise())
            {
                Console.WriteLine("Connecting to remote database");
                Database = new RemoteStorageDatabase(testIp, testPort);
                Console.WriteLine("Remote database connected");
            }
            else
            {
                Database = new StorageDatabase();
            }
            
            
 
        }

        //gets the database
        public static IStorageDatabase GetDatabase()
        {
            return Database;
        }

        //imports the data
        public static void Import(List<string> data)
        {
            //if enterprise don't run
            if (App.IsEnterprise())
            {
                return;
            }

            ((StorageDatabase)DatabaseHandler.GetDatabase()).Import(data);
        }
    }
}
