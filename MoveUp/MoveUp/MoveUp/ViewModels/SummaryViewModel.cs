using System;
using System.Collections.Generic;
using System.Linq;
using MoveUp.Factories.Interfaces;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;
using Xamarin.Forms.Maps;

namespace MoveUp.ViewModels
{
    public class SummaryViewModel : ViewModelBase
    {    
        private ICoreMotionController motionController;
        private IPreferencesStorage preferencesStorage;
        private ILocationController locationController;
        private IStorageManager storageManager;

        public CoreMotionData MotionData { get; set; }
        public LocationPolyline RealtimePolyline { get; set; }

        public int StepsTarget { get; set; }
        public double DistanceTarget { get; set; }

        public Map Map { get; set; }

        public SummaryViewModel(ICommandFactory commandFac,
                                ICoreMotionController motionContr,
                                INavigationService navigation,
                                IPreferencesStorage preferences,
                                ILocationController locationContr,
                                IStorageManager storageMan) : base(navigation, commandFac)
        {
            motionController = motionContr;
            preferencesStorage = preferences;
            locationController = locationContr;
            storageManager = storageMan;

            StepsTarget = preferencesStorage.GetStepsTarget();
            DistanceTarget = preferencesStorage.GetDistanceTarget();

            InitializeMotionAsync();
            InitializeLocation();
            InitializeMap();
        }

        public void Refresh()
        {
            StepsTarget = preferencesStorage.GetStepsTarget();
            DistanceTarget = preferencesStorage.GetDistanceTarget();
        }

        public void SavePolyline()
        {
            List<Position> positions = new List<Position>(RealtimePolyline.Polyline.Geopath);
            storageManager.InsertTodaysPositions(positions.Select(position => new SavedPosition(position)).ToList());
        }

        private async void InitializeMotionAsync()
        {
            await motionController.TriggerPedometerAsync();
            MotionData = motionController.GetData();
        }

        private void InitializeLocation()
        {
            locationController.StartUpdates();
            RealtimePolyline = locationController.GetPolyline();

            var positions = storageManager.GetTodaysPositions().Select(position => new Position(position.Latitude, position.Longitude)).ToList();
            locationController.InitializePolyline(positions);
        }

        private void InitializeMap()
        {
            Map = new Map();
            Map.MapType = MapType.Street;
            Map.IsShowingUser = true;
            Map.HasZoomEnabled = true;

            Map.MapElements.Add(RealtimePolyline.Polyline);
            locationController.SetDelegateMap(Map);
        }
    }
}
