using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using MvvmCross.Platforms.Android.Views;
using Xamarin.Forms;

namespace AddressMapBook.Droid
{
    [Activity(
        Label = "AddressMapBook.Droid"
        , MainLauncher = true
        , Icon = "@mipmap/ic_launcher"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
            //StartActivity(typeof(MainActivity));
        }

        //protected override void TriggerFirstNavigate()
        //{
        //    StartActivity(typeof(MainActivity));
        //    base.TriggerFirstNavigate();
        //}

        //public override void triggersearch(string query, bundle appsearchdata)
        //{

        //    base.triggersearch(query, appsearchdata);
        //}

        protected override Task RunAppStartAsync(Bundle bundle)
        {
            StartActivity(typeof(MainActivity));
            return base.RunAppStartAsync(bundle);
        }

        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            Forms.Init(this, bundle);
            // Leverage controls' StyleId attrib. to Xamarin.UITest
            Forms.ViewInitialized += (object sender, ViewInitializedEventArgs e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.View.StyleId))
                {
                    e.NativeView.ContentDescription = e.View.StyleId;
                }
            };

            base.OnCreate(bundle);
        }
    }
}
