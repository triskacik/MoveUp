using System;
namespace MoveUp.Models
{
    public class AltitudeData : ModelBase
    {
        public double Elevation { get; private set; }
        public double ElevationGain{ get; private set; }

        private double lastRisingElevation;
        private bool isGaining;

        public AltitudeData()
        {
            Elevation = 0;
            ElevationGain = 0;
            lastRisingElevation = 0;
            isGaining = false;
        }

        public void UpdateElevation(double elevation)
        {
            if (elevation > Elevation && !isGaining)
            {
                lastRisingElevation = elevation;
                isGaining = true;

            }
            else if (elevation < Elevation && isGaining)
            {
                isGaining = false;
            }

            Elevation = elevation;
            UpdateElevationGain(elevation);
        }

        private void UpdateElevationGain(double elevation)
        {
            if ((elevation - 1) > lastRisingElevation && isGaining)
            {
                ElevationGain += elevation - lastRisingElevation;
                lastRisingElevation = elevation;
            }
        }
    }
}
