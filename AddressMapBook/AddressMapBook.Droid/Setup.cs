using Android.Content;
using MvvmCross.Forms.Core;
//using MvvmCross.Forms.Droid;
//using AddressMapBook.Core;
using AddressMapBook.Core.Dependency;
using AddressMapBook.Droid.Dependency;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using core = AddressMapBook.Core;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross;

namespace AddressMapBook.Droid
{
    public class Setup : MvxFormsAndroidSetup<core.CoreApp, core.Application>
    {

        //protected override MvvmCross.ViewModels.IMvxApplication CreateApp() => new CoreApp();

        //protected override Xamarin.Forms.Application CreateFormsApplication()
        //{
        //    return new core.Application();
        //}

        //public Setup(Context applicationContext)
        //    : base(applicationContext)
        //{
        //}

        //protected override IMvxApplication CreateApp() => new CoreApp();
        //protected override MvxFormsApplication CreateFormsApplication() => new Application();
        //protected override IMvxTrace CreateDebugTrace() => new DebugTrace();

        //protected override void InitializeFirstChance()
        //{
        //    Mvx.RegisterType<IPermissionDependency, PermissionDependency>();
        //    base.InitializeFirstChance();
        //}

        protected override void InitializeFirstChance()
        {
            Mvx.IoCProvider.RegisterType<IPermissionDependency, PermissionDependency>();
            Mvx.IoCProvider.RegisterType<IQuickMessageDependency, QuickMessageDependency>();
            base.InitializeFirstChance();
        }
    }
}
