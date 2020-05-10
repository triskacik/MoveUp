using System;
namespace MoveUp.Models
{
    public class AltitudeData : ModelBase
    {
        public double Elevation { get; set; }

        public AltitudeData()
        {
            Elevation = 0;
        }
    }
}
