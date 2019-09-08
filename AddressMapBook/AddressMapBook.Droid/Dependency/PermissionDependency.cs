using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressMapBook.Core.Dependency;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

using Xamarin.Forms;

[assembly: Dependency(typeof(AddressMapBook.Droid.Dependency.PermissionDependency))]
namespace AddressMapBook.Droid.Dependency
{
    class PermissionDependency : IPermissionDependency
    {
        public async Task<bool> LocationPermission()
        {
            bool ret = false;
            var stat = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (stat != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                {
                    Toast.MakeText(Android.App.Application.Context, "Need location permission to load map", ToastLength.Long).Show();
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                stat = results[Permission.Location];
            }

            ret = stat == PermissionStatus.Granted;
            if (stat != PermissionStatus.Granted)
            {
                Toast.MakeText(Android.App.Application.Context, "Opening Location , Permission Denied", ToastLength.Long).Show();
            }
            return ret;
        }
    }
}