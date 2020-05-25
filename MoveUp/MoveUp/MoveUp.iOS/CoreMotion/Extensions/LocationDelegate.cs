using System;
using CoreLocation;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using Xamarin.Forms.Maps;

namespace MoveUp.iOS.CoreMotion.Extensions
{
    public class LocationDelegate : CLLocationManagerDelegate
    {
        public LocationPolyline Polyline { get; set; } = new LocationPolyline();
        public LocationPolyline ActivityPolyline { get; set; } = new LocationPolyline();

        public Map Map { get; set; }
        public Map ActivityMap { get; set; }

        public bool ActivityRunning { get; set; } = false;

        private IStorageManager storageManager;

        public LocationDelegate(IStorageManager storage)
        {
            storageManager = storage;
        }

        public override void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
        {
            foreach (var l in locations)
            {
                Polyline.AddData(new Position(l.Coordinate.Latitude, l.Coordinate.Longitude));

                if (ActivityRunning)
                {
                    ActivityPolyline.AddData(new Position(l.Coordinate.Latitude, l.Coordinate.Longitude));
                }

                storageManager.CachePosition(new SavedPosition(new Position(l.Coordinate.Latitude, l.Coordinate.Longitude)));
            }

            if (Map != null)
                Map.MoveToRegion(new MapSpan(Polyline.Polyline.Geopath[Polyline.Polyline.Geopath.Count - 1], 0.01, 0.01));

            if (ActivityRunning && ActivityMap != null)
                ActivityMap.MoveToRegion(new MapSpan(Polyline.Polyline.Geopath[Polyline.Polyline.Geopath.Count - 1], 0.01, 0.01));
        }
    }
}
