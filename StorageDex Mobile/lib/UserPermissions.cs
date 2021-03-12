using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace StorageDex_Mobile.lib
{
    class UserPermissions
{
        /**
         * call these methods to validate permissions
         */

        //validates read and write storage permissions
        public static async Task<bool> ValidateStoragePermissions()
        {
            bool result = true;

            PermissionStatus writeStatus =  await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (writeStatus != PermissionStatus.Granted)
            {
                PermissionStatus writeResult = await Permissions.RequestAsync<Permissions.StorageWrite>();
                result = result && writeResult == PermissionStatus.Granted;
            }

            PermissionStatus readStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (readStatus != PermissionStatus.Granted)
            {
                PermissionStatus readResult = await Permissions.RequestAsync<Permissions.StorageRead>();
                result = result && readResult == PermissionStatus.Granted;
            }
            return result;

        }

        //validates camera permissions
        public static async Task<bool> ValidateCameraPermissions()
        {


            PermissionStatus cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (cameraStatus != PermissionStatus.Granted)
            {
                cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();
                return cameraStatus == PermissionStatus.Granted;
            }

            return true;



        }
    }
}
