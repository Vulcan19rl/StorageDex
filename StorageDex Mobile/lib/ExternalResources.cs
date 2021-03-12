using Plugin.Media;
using Plugin.Media.Abstractions;
using StorageDex_Mobile.lib.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;

namespace StorageDex_Mobile.lib
{
    class ExternalResources
    {
        /**
         * this class contains methods relating to the external resources of the device. camera, files etc
         */

        private static StoreCameraMediaOptions defaultCameraOptions = new StoreCameraMediaOptions()
        {
            CompressionQuality = 92,
            PhotoSize = PhotoSize.Medium
        };

        private static PickMediaOptions defaultPickOptions = new PickMediaOptions()
        {
            CompressionQuality = 92,
            PhotoSize = PhotoSize.Medium
        };

        //has the user pick an image from the files or gallery
        public async static Task<MediaFile> PickPhoto()
        {
            Task<bool> initTask = CrossMedia.Current.Initialize();
            initTask.Wait();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return null;
            }

            MediaFile returnFile = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(defaultPickOptions);

            if (returnFile != null)
            {
                return returnFile;
            }
            else
            {
                return null;
            }


        }

        //has the user take a photo
        public async static Task<MediaFile> TakePhoto()
        {
            Task<bool> initTask = CrossMedia.Current.Initialize();
            initTask.Wait();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return null;
            }

            MediaFile returnFile = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(defaultCameraOptions);


            if (returnFile != null)
            {
                return returnFile;
            }
            else
            {
                return null;
            }
        }

        //has the user scan a barcode and returns the code
        public async static Task<string> ScanBarcode()
        {


            var scanner = new ZXing.Mobile.MobileBarcodeScanner();

            var result = await scanner.Scan();

            if (result == null)
            {
                return null;
            }
            else
            {
                return result.Text;
            }
        }


    }
}
