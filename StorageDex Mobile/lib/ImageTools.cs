

using Android.Accounts;
using Android.Content.Res;
using FFImageLoading;
using FFImageLoading.Args;
using FFImageLoading.Forms;
using FFImageLoading.Forms.Args;
using FFImageLoading.Work;
using PCLStorage;
using Plugin.Media.Abstractions;
using StorageDexLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StorageDex_Mobile.lib
{
    class ImageTools
{
        /**
         * tools and methods for image manipulation
         */

        //converts a mediafile to an imagesource
        public static Xamarin.Forms.ImageSource MediaFileToImageSource(MediaFile fileIn)
        {
            return Xamarin.Forms.ImageSource.FromStream(() => fileIn.GetStream());
        }

  

        //imports an image to the app directory and gives it the given name - must be a jpg
        //CAUTION deletes the image of the mediafile going in
        public static async Task<bool> ImportImage(MediaFile imageIn, string parentDirectory, string newName)
        {

            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync(parentDirectory, CreationCollisionOption.OpenIfExists); //open folder, create if it dosent exist

            Stream imageInStream = imageIn.GetStream(); //get image in stream
            byte[] imageInBytes = imageInStream.ToByteArray(); //convert image in stream to byte array
                
            if((await folder.CheckExistsAsync(newName + ".jpg")).Equals(ExistenceCheckResult.FileExists))
            {
                File.Delete(Path.Combine(folder.Path, newName + ".jpg"));

            }
            IFile movedImage = await folder.CreateFileAsync(newName + ".jpg", CreationCollisionOption.ReplaceExisting); //create the new file, replace if it already exists
            using (System.IO.Stream stream = await movedImage.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
            { //open new image file
                stream.Write(imageInBytes, 0, imageInBytes.Length); //write the imagein's byte stream to the new file
            }

            //file should now be in the pcl storage

            return true;
            
        }

        //core/abstract version of the has image method - checks if the final exists as the given directory
        private  static bool HasImage(string imagePath)
        {
            if (App.IsEnterprise())
            {
                return false;
            }
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            return File.Exists(Path.Combine(rootFolder.Path, imagePath));
        }
        //returns true if the location has an image
        public static  bool HasImage(StorageLocation locationIn)
        {
            
            int id = locationIn.GetId();
            return HasImage(Path.Combine(Directories.locationImageDirectory, id.ToString() + ".jpg"));

        }

        //returns true if the container has an image
        public static bool HasImage(StorageContainer containerIn)
        {

            int id = containerIn.GetId();
            return HasImage(Path.Combine(Directories.containerImageDirectory, id.ToString() + ".jpg"));

        }

        //returns true if the item has an image
        public static bool HasImage(Item itemIn)
        {

            int id = itemIn.GetId();
            return HasImage(Path.Combine(Directories.itemImageDirectory, id.ToString() + ".jpg"));

        }


        //core/abstract version of the load image method. Loads the image at the given directory and returns it as an image source
        public static Xamarin.Forms.ImageSource LoadImage(string path) 
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            return Xamarin.Forms.ImageSource.FromFile(Path.Combine(rootFolder.Path, path));


        }

        //loads the image for the location and returns a ImageSource
        public static Xamarin.Forms.ImageSource LoadImage(StorageLocation locationIn)
        {
            int id = locationIn.GetId();
            Xamarin.Forms.ImageSource returnSource =  LoadImage(Path.Combine(Directories.locationImageDirectory, id.ToString() + ".jpg"));
            ImageBaseHandler.current.AddLocationImage(locationIn.GetId(),returnSource);
            return returnSource;
        }

        //loads the image for the container and returns a ImageSource
        public static Xamarin.Forms.ImageSource LoadImage(StorageContainer containerIn)
        {
            int id = containerIn.GetId();
            Xamarin.Forms.ImageSource returnSource = LoadImage(Path.Combine(Directories.containerImageDirectory, id.ToString() + ".jpg"));
            ImageBaseHandler.current.AddContainerImage(containerIn.GetId(), returnSource);
            return returnSource;
        }


        //loads the image for the item and returns a ImageSource
        public static Xamarin.Forms.ImageSource LoadImage(Item itemIn)
        {
            int id = itemIn.GetId();
            Xamarin.Forms.ImageSource returnSource = LoadImage(Path.Combine(Directories.itemImageDirectory, id.ToString() + ".jpg"));
            ImageBaseHandler.current.AddItemImage(itemIn.GetId(), returnSource);
            return returnSource;
        }


    }
}
