using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms.Maps;

namespace MoveUp.Models
{
    public class LocationData : ModelBase
    {
        public ObservableCollection<Position> Positions { get; set; } = new ObservableCollection<Position>();

        public LocationData()
        {
        }
    }
}
