using System;
using MoveUp.Factories.Interfaces;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;
using Xamarin.Forms.Maps;

namespace MoveUp.ViewModels
{
    public class HikingSavedViewModel : ViewModelBase
    {
        public HikingSavedData Data { get; set; } = new HikingSavedData();

        public Map Map { get; set; }
        public LocationPolyline Polyline { get; set; } = new LocationPolyline();

        public HikingSavedViewModel(INavigationService navigationService,
                                    ICommandFactory commandFactory,
                                    IHistoryDataFeeder dataFeeder) : base(navigationService, commandFactory)
        {
            Data = dataFeeder.GetHikingData();
            InitializeMap();
        }

        private void InitializeMap()
        {
            Map = new Map();
            Map.MapType = MapType.Hybrid;
            Map.HasZoomEnabled = true;

            foreach (var p in Data.Positions)
            {
                Polyline.Polyline.Geopath.Add(new Position(p.Latitude, p.Longitude));
            }

            Map.MapElements.Add(Polyline.Polyline);

            if (Polyline.Polyline.Geopath.Count > 0)
                Map.MoveToRegion(new MapSpan(Polyline.Polyline.Geopath[Polyline.Polyline.Geopath.Count - 1], 0.01, 0.01));
        }
    }
}
