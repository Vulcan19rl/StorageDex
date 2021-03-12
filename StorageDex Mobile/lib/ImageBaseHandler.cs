using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StorageDex_Mobile.lib
{
    class ImageBaseHandler
{
        /**
         * handles caching of images
         */
        public static ImageBaseHandler current;
        public static void InitImageBaseHanlder()
        {
            current = new ImageBaseHandler();
        }

        private Dictionary<int, ImageSource> locationImageBase = new Dictionary<int, ImageSource>(); //the image database - caches loaded images
        private Dictionary<int, ImageSource> containerImageBase = new Dictionary<int, ImageSource>(); //the image database - caches loaded images
        private Dictionary<int, ImageSource> itemImageBase = new Dictionary<int, ImageSource>(); //the image database - caches loaded images

        public void AddLocationImage(int id, ImageSource source)
        {
            locationImageBase[id] = source;
        }

        public ImageSource GetLocationImageSource(int id)
        {
            if (locationImageBase.ContainsKey(id))
            {
                return locationImageBase[id];
            }
            else
            {
                return null;
            }
        }

        //returns true if the image is cached
        public bool isLocationCached(int id)
        {
            if (App.IsEnterprise())
            {
                return false;
            }
            return locationImageBase.ContainsKey(id);
        }

        public void AddContainerImage(int id, ImageSource source)
        {
            containerImageBase[id] = source;
        }

        public ImageSource GetContainerImageSource(int id)
        {
            if (containerImageBase.ContainsKey(id))
            {
                return containerImageBase[id];
            }
            else
            {
                return null;
            }
        }

        //returns true if the image is cached
        public bool isContainerCached(int id)
        {
            return containerImageBase.ContainsKey(id);
        }

        //returns true if the image is cached
        public bool isItemCached(int id)
        {
            return itemImageBase.ContainsKey(id);
        }

        public ImageSource GetItemImageSource(int id)
        {
            if (itemImageBase.ContainsKey(id))
            {
                return itemImageBase[id];
            }
            else
            {
                return null;
            }
        }


        public void AddItemImage(int id, ImageSource source)
        {
            itemImageBase[id] = source;
        }
    }
}
