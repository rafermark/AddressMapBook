using AddressMapBook.Core.Dependency;
using AddressMapBook.Core.Event;
using AddressMapBook.Core.Interface;
using AddressMapBook.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AddressMapBook.Core.ViewModels
{
    public class SearchAddressViewModel : MvxViewModel
    {
        #region Entry / Exit

        public SearchAddressViewModel(IMvxNavigationService navigationService
            , IGoogleMapsApiService googleMapsApiService
            , IMvxMessenger messenger, IQuickMessageDependency quickMessageDependency)
        {
            _navigationService = navigationService;
            _googleMapsApiService = googleMapsApiService;
            _messenger = messenger;
            _quickMessageDependency = quickMessageDependency;
            Init();
        }

        void Init()
        {
            SearchCommand = new MvxCommand(ExecuteSearch);
            SelectCommand = new MvxCommand<GooglePlaceAutoCompletePrediction>(ExecuteSelect);

            PlacesResult = new List<GooglePlaceAutoCompletePrediction>();
        }

        #endregion

        #region Declarations

        #region Injections

        private readonly IMvxNavigationService _navigationService;
        private readonly IGoogleMapsApiService _googleMapsApiService;
        private readonly IMvxMessenger _messenger;
        private readonly IQuickMessageDependency _quickMessageDependency;

        #endregion

        #region Privates

        #endregion

        #region Properties

        private string _searchAddress;
        public string SearchAddress
        {
            get { return _searchAddress; }
            set
            {
                SetProperty(ref _searchAddress, value);
                ExecuteSearch();
            }
        }

        private List<GooglePlaceAutoCompletePrediction> _placesResult;
        public List<GooglePlaceAutoCompletePrediction> PlacesResult
        {
            get { return _placesResult; }
            set { SetProperty(ref _placesResult, value); }
        }

        #endregion

        #region Commands

        public ICommand SearchCommand { get; set; }
        public ICommand SelectCommand { get; set; }

        #endregion

        #endregion

        #region Methods / Functions / Navigations

        #region Navigations

        #endregion

        #region Methods

        async void ExecuteSearch()
        {
            try
            {
                if (!string.IsNullOrEmpty(SearchAddress))
                {
                    var res = await _googleMapsApiService.GetPlaces(SearchAddress);
                    if (res.AutoCompletePlaces?.Count > 0)
                    {
                        PlacesResult = new List<GooglePlaceAutoCompletePrediction>(res.AutoCompletePlaces);
                    }
                }
                else
                {
                    PlacesResult = new List<GooglePlaceAutoCompletePrediction>();
                }
            }
            catch(Exception ex)
            {
                _quickMessageDependency.ShowToastMessage(ex.Message);
            }
        }


        private async void ExecuteSelect(GooglePlaceAutoCompletePrediction address)
        {
            try
            {
                //_navigationService.Navigate<AddressMapViewModel, GooglePlaceAutoCompletePrediction>(address);
                var place = await _googleMapsApiService.GetPlaceDetails(address.PlaceId);
                address.Place = place;

                _messenger.Publish(new AddressResult(this, address.ToMapTable()));
            }
            catch (Exception ex)
            {
                _quickMessageDependency.ShowToastMessage(ex.Message);
            }
            await _navigationService.Close(this);

            //ShowViewModel<AddressMapViewModel, GooglePlaceAutoCompletePrediction>(address);
        }

        #endregion

        #region Functions

        #endregion

        #endregion
    }
}
