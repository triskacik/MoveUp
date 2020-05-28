using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MoveUp.Models
{
    public class LocationPolyline : ModelBase
    {
        public Polyline Polyline { get; set; } = new Polyline();

        public LocationPolyline()
        {
            Polyline.StrokeColor = Color.FromHex("#007AFF");
            Polyline.StrokeWidth = 10;
        }

        public void AddData(Position position)
        {
            Polyline.Geopath.Add(position);
        }
    }
}
