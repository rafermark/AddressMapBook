﻿using Foundation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.iOS;
using MvvmCross.Platform;
using UIKit;

namespace AddressMapBook.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var setup = new Setup(this, Window);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            LoadApplication(setup.FormsApplication);

            Window.MakeKeyAndVisible();

            return true;
        }
    }
}
