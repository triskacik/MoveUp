using System.Collections.Generic;
using CoreLocation;
using MoveUp.iOS.CoreMotion.Extensions;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using Xamarin.Forms.Maps;

namespace MoveUp.iOS.CoreMotion
{
    public class ActivityLocationController : IActivityLocationController
    {
        private bool isCountingAvailable = false;
        private CLLocationManager locationManager;

        public ActivityLocationDelegate LocationDelegate { get; set; } = new ActivityLocationDelegate();

        public ActivityLocationController()
        {
            if (CLLocationManager.SignificantLocationChangeMonitoringAvailable)
            {
                locationManager = new CLLocationManager();
                isCountingAvailable = true;

                locationManager.Delegate = LocationDelegate;
                locationManager.AllowsBackgroundLocationUpdates = true;
                locationManager.DistanceFilter = 10;
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

        public void SetDelegateMap(Map map)
        {
            LocationDelegate.Map = map;
        }
    }
}
