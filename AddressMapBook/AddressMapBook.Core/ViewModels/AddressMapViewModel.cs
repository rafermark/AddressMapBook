using AddressMapBook.Core.Event;
using AddressMapBook.Core.Interface;
using AddressMapBook.Core.Models;
using MvvmCross.ViewModels;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Xamarin.Forms.Maps;
using AddressMapBook.Core.Dependency;
using System.Threading.Tasks;
using System.Linq;

namespace AddressMapBook.Core.ViewModels
{
    public class AddressMapViewModel : MvxViewModel
    {

        #region Entry / Exit

        public AddressMapViewModel(IMvxNavigationService navigationService,
            IConnection connection, IMvxMessenger messenger,
            IPermissionDependency permissionDependency, IGoogleMapsApiService googleMapsApiService,
            IGoogleMapTableAccess googleMapTableAccess, IQuickMessageDependency quickMessageDependency)
        {
            _navigationService = navigationService;
            _connection = connection;
            _messenger = messenger;
            _permissionDependency = permissionDependency;
            _googleMapsApiService = googleMapsApiService;
            _googleMapTableAccess = googleMapTableAccess;
            _quickMessageDependency = quickMessageDependency;
            Title = "Address Book";
            Init();

            _token = _messenger.Subscribe<AddressResult>((addr) =>
            {
                try
                {
                    if (addr != null)
                    {
                        if (!AddedAddresses.Any(x => x.PlaceId == addr.Address.PlaceId))
                        {
                            _googleMapTableAccess.Insert(addr.Address);
                            AddedAddresses.Add(addr.Address);
                            ExecuteSelect(addr.Address);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _quickMessageDependency.ShowToastMessage(ex.Message);
                }
            });
        }

        async void Init()
        {
            AddNewAddressCommand = new MvxCommand(async () =>
            {
                try
                {
                    if (LocationGranted && _connection.IsConnected())
                        await _navigationService.Navigate<SearchAddressViewModel>();
                    else if (!LocationGranted)
                        await ExecuteAskPermission();
                    else if (!_connection.IsConnected())
                        _quickMessageDependency.ShowToastMessage("No internet connection.");
                }
                catch (Exception ex)
                {
                    _quickMessageDependency.ShowToastMessage(ex.Message);
                }

            });

            SelectCommand = new MvxCommand<GoogleMapTables>(ExecuteSelect);

            AskPermissionCommand = new MvxCommand(async () =>
            {
                await ExecuteAskPermission();
            });


            LocationGranted = await _permissionDependency.LocationPermission();

            try
            {
                var _dbList = await _googleMapTableAccess.GetMapTables();
                AddedAddresses = new MvxObservableCollection<GoogleMapTables>(_dbList);
                if(AddedAddresses.Count > 0)
                {
                    ExecuteSelect(AddedAddresses.First());
                }
            }
            catch (Exception ex)
            {
                _quickMessageDependency.ShowToastMessage(ex.Message);
            }

            await SetMap();
        }

        ~AddressMapViewModel()
        {

        }

        //public override void Prepare(GooglePlaceAutoCompletePrediction parameter)
        //{
        //    if (parameter != null)
        //        AddedAddresses.Add(parameter);
        //}

        #endregion

        #region Declarations

        #region Injections

        private readonly IMvxNavigationService _navigationService;
        private readonly IConnection _connection;
        private readonly IMvxMessenger _messenger;
        private readonly IPermissionDependency _permissionDependency;
        private readonly IGoogleMapsApiService _googleMapsApiService;
        private readonly IGoogleMapTableAccess _googleMapTableAccess;
        private readonly IQuickMessageDependency _quickMessageDependency;

        #endregion

        #region Privates

        private readonly MvxSubscriptionToken _token;

        #endregion

        #region Properties

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private MvxObservableCollection<GoogleMapTables> _addedAddresses;
        public MvxObservableCollection<GoogleMapTables> AddedAddresses
        {
            get { return _addedAddresses; }
            set { SetProperty(ref _addedAddresses, value); }
        }

        private Map _map;
        public Map Map
        {
            get { return _map; }
            set { SetProperty(ref _map, value); }
        }

        private bool _locationGranted;
        public bool LocationGranted
        {
            get { return _locationGranted; }
            set { SetProperty(ref _locationGranted, value); }
        }

        #endregion

        #region Commands

        public ICommand AddNewAddressCommand { get; set; }
        public ICommand SelectCommand { get; set; }
        public ICommand AskPermissionCommand { get; set; }
        public ICommand RemoveAddressCommand { get; set; }

        #endregion

        #endregion

        #region Methods / Functions / Navigations

        #region Navigations

        #endregion

        #region Methods

        private async void ExecuteSelect(GoogleMapTables address)
        {
            try
            {
                if (address != null && Map != null)
                {
                    var position = new Position(address.Latitude, address.Longitude); // Latitude, Longitude
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = position,
                        Label = "custom pin",
                        Address = "custom detail info"
                    };
                    Map.Pins.Clear();
                    Map.Pins.Add(pin);
                    Map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1)));
                    await RaisePropertyChanged("Map");
                }
                else if (Map == null)
                {
                    if (_connection.IsConnected())
                        await SetMap();

                    throw new Exception("Map not accessible.");
                }
                else if (address == null)
                    throw new Exception("Address is empty.");
            }
            catch (Exception ex)
            {
                _quickMessageDependency.ShowToastMessage(ex.Message);
            }
        }

        private async Task SetMap()
        {
            try
            {
                if (LocationGranted)
                {

                    var currLoc = await _googleMapsApiService.GetCurrentLocation();

                    Map = new Map(
                                MapSpan.FromCenterAndRadius(
                    new Position(currLoc.Location.Latitude, currLoc.Location.Longitude), Distance.FromMiles(0.3)))
                    {
                        IsShowingUser = true,
                        HeightRequest = 200,
                        WidthRequest = 320,
                    };
                }
                else
                {
                    Map = null;
                }
            }
            catch (Exception ex)
            {
                _quickMessageDependency.ShowToastMessage(ex.Message);
            }
        }

        private async Task ExecuteAskPermission()
        {
            LocationGranted = await _permissionDependency.LocationPermission();
            if (LocationGranted)
            {
                await SetMap();
            }
        }

        #endregion

        #region Functions

        #endregion

        #endregion

    }
}
