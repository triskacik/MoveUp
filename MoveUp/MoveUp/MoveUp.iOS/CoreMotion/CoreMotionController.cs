using System;
using System.Globalization;
using CoreMotion;
using Foundation;
using MoveUp.iOS.CoreMotion.Extensions;
using MoveUp.Models;
using MoveUp.Services.Interfaces;

namespace MoveUp.iOS.CoreMotion
{
    public class CoreMotionController : ICoreMotionController
    {
        private CMPedometer pedometer;
        private CMPedometerData pedometerData;

        public int Steps { get; set; }

        public CoreMotionController()
        {
            pedometer = new CMPedometer();
            TriggerPedometer();
        }

        private async void TriggerPedometer()
        {
            //pedometer.StartPedometerUpdates(new NSDate(), UpdatePedometerData);

            Calendar calendar;

            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Now;

            pedometerData = await pedometer.QueryPedometerDataAsync(DateConverter.DateTimeToNSDate(startDate), DateConverter.DateTimeToNSDate(endDate));
        }

        public CoreMotionData GetData()
        {
            return new CoreMotionData(pedometerData.NumberOfSteps.Int32Value);
        }
    }
}
