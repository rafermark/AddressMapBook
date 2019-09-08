using AddressMapBook.Core.Dependency;
using AddressMapBook.Droid.Dependency;
using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross;
//using MvvmCross.Forms.Droid;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.Forms.Presenters;
using MvvmCross.Platforms.Android.Presenters;
using Plugin.Permissions;
using Xamarin.Forms;

namespace AddressMapBook.Droid
{
    [Activity(Label = "MainActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity
        : MvxFormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Resource.Layout.toolbar;
            TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);

            //var formsPresenter = (MvxFormsPagePresenter)Mvx.IoCProvider.Resolve<IMvxAndroidViewPresenter>();
            //LoadApplication(formsPresenter.FormsApplication);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}


