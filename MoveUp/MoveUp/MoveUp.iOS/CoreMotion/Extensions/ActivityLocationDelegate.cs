using System;
using CoreLocation;
using MoveUp.Models;
using Xamarin.Forms.Maps;

namespace MoveUp.iOS.CoreMotion.Extensions
{
    public class ActivityLocationDelegate : CLLocationManagerDelegate
    {
        public LocationPolyline Polyline { get; set; } = new LocationPolyline();

        public Map Map { get; set; }

        public ActivityLocationDelegate()
        {
        }

        public override void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
        {
            foreach (var l in locations)
            {
                Polyline.AddData(new Position(l.Coordinate.Latitude, l.Coordinate.Longitude));
            }

            if (Map != null)
                Map.MoveToRegion(new MapSpan(Polyline.Polyline.Geopath[Polyline.Polyline.Geopath.Count - 1], 0.01, 0.01));
        }
    }
}
