using System;
using System.Collections.Generic;
using CoreLocation;
using MoveUp.iOS.CoreMotion.Extensions;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using Xamarin.Forms.Maps;

namespace MoveUp.iOS.CoreMotion
{
    public class LocationController : ILocationController
    {
        private bool isCountingAvailable = false;
        private CLLocationManager locationManager;

        public LocationDelegate LocationDelegate { get; set; } 

        public LocationController(IStorageManager storage)
        {
            LocationDelegate = new LocationDelegate(storage);

            if (CLLocationManager.SignificantLocationChangeMonitoringAvailable)
            { 
                locationManager = new CLLocationManager();
                isCountingAvailable = true;

                locationManager.Delegate = LocationDelegate;
                locationManager.AllowsBackgroundLocationUpdates = true;
                locationManager.DistanceFilter = 20;
                locationManager.DesiredAccuracy = CLLocation.AccuracyBest;
            }
        }

        public void StartUpdates()
        {
            if (isCountingAvailable)
                locationManager.StartUpdatingLocation();
        }

        public LocationPolyline GetPolyline()
        {
            return LocationDelegate.Polyline;
        }

        public LocationPolyline GetActivityPolyline()
        {
            return LocationDelegate.ActivityPolyline;
        }

        public void InitializePolyline(List<Position> positions)
        {
            LocationDelegate.Polyline.Polyline.Geopath.Clear();
            positions.ForEach(position => LocationDelegate.Polyline.Polyline.Geopath.Add(position));
        }

        public void StartActivityUpdates(Map map)
        {
            locationManager.DistanceFilter = 5;
            StartUpdates();
            LocationDelegate.ActivityPolyline = new LocationPolyline();
            LocationDelegate.ActivityMap = map;
            LocationDelegate.ActivityRunning = true;
        }

        public void StopActivityUpdates()
        {
            locationManager.DistanceFilter = 20;
            LocationDelegate.ActivityRunning = false;
        }

        public void SetDelegateMap(Map map)
        {
            LocationDelegate.Map = map;
        }
    }
}
