using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressMapBook.Core.Dependency;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

[assembly: Dependency(typeof(AddressMapBook.Droid.Dependency.QuickMessageDependency))]
namespace AddressMapBook.Droid.Dependency
{
    class QuickMessageDependency : IQuickMessageDependency
    {
        public void ShowToastMessage(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}