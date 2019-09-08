using AddressMapBook.Core.Database;
using AddressMapBook.Core.Service;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using System;
using System.IO;

namespace AddressMapBook.Core
{
    public class CoreApp : MvxApplication
    {
        public override void Initialize()
        {
            //CreatableTypes()



            //    .EndingWith("Service")
            //    .AsInterfaces()
            //    .RegisterAsLazySingleton();

            CreatableTypes()
            .EndingWith("Service")
            .AsInterfaces()
            .RegisterAsLazySingleton();

            GoogleMapApiService.Initialize();

            //RegisterNavigationServiceAppStart<ViewModels.AddressMapViewModel>();
            RegisterAppStart<ViewModels.AddressMapViewModel>();

        }

        static MapsDB database;

        public static MapsDB Database
        {
            get
            {
                if (database == null)
                {
                    database = new MapsDB(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MapsDB.db3"));
                }
                return database;
            }
        }
    }
}



#region Entry / Exit

#endregion

#region Declarations

#region Injections

#endregion

#region Privates

#endregion

#region Properties

#endregion

#region Commands

#endregion

#endregion

#region Methods / Functions / Navigations

#region Navigations

#endregion

#region Methods

#endregion

#region Functions

#endregion

#endregion
