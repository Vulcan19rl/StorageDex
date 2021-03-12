
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StorageDex_Mobile.lib
{
    class Directories
{
        /**
         * definitions for working directories
         */
        public static string dataPath = "data.txt";

        public static string imageDirectory = "images";
        public static string locationImageDirectory = Path.Combine(imageDirectory + "locations");
        public static string containerImageDirectory = Path.Combine(imageDirectory + "containers");
        public static string itemImageDirectory = Path.Combine(imageDirectory + "items");
    }
}
