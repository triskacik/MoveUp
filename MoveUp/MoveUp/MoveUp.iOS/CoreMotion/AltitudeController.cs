using System;
using CoreMotion;
using Foundation;
using MoveUp.Models;
using MoveUp.Services.Interfaces;

namespace MoveUp.iOS.CoreMotion
{
    public class AltitudeController : IAltitudeController
    {
        private bool countingAvailable = false;
        private CMAltimeter altimeter;

        private CMAltitudeData altData;

        public AltitudeData altitudeData;

        public AltitudeController()
        {
            if (CMAltimeter.IsRelativeAltitudeAvailable)
            {
                countingAvailable = true;
                altimeter = new CMAltimeter();
            }
        }

        public void TriggerActivity()
        {
            if (countingAvailable)
            {
                altitudeData = new AltitudeData();
                DateTime startDate = DateTime.Now;

                altimeter.StartRelativeAltitudeUpdates(NSOperationQueue.MainQueue, UpdateAltitudeData);
            }
        }

        private void UpdateAltitudeData(CMAltitudeData data, NSError error)
        {
            if (error == null)
            {
                altData = data;

                if (altData.RelativeAltitude != null)
                    altitudeData.UpdateElevation(Math.Round(altData.RelativeAltitude.DoubleValue, 1));
            }
        }

        public void StopActivityUpdates()
        {
            altimeter.StopRelativeAltitudeUpdates();
        }

        public AltitudeData GetData()
        {
            return altitudeData;
        }
    }
}
