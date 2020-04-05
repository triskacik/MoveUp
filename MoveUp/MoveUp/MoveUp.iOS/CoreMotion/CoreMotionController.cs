using System;
using System.Threading.Tasks;
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
        private CoreMotionData motionData = new CoreMotionData();
        private bool couningAvailable = false;

        public int Steps { get; set; }

        public CoreMotionController()
        {
            if (CMPedometer.IsStepCountingAvailable)
            {
                couningAvailable = true;
                pedometer = new CMPedometer();
            }
        }

        public async Task TriggerPedometer()
        {
            if (couningAvailable)
            {
                DateTime startDate = DateTime.Today;
                DateTime endDate = DateTime.Now;

                pedometerData = await pedometer.QueryPedometerDataAsync(DateConverter.DateTimeToNSDate(startDate), DateConverter.DateTimeToNSDate(endDate));

                pedometer.StartPedometerUpdates(DateConverter.DateTimeToNSDate(startDate), UpdatePedometerData);
                UpdatePedometerData(pedometerData, null);
            }
        }

        private void UpdatePedometerData(CMPedometerData data, NSError error)
        {
            if (error == null)
            {
                pedometerData = data;
                motionData.Steps = pedometerData.NumberOfSteps.Int32Value;
                motionData.Distance = pedometerData.Distance.Int32Value / 1000d;
                motionData.Floors = pedometerData.FloorsAscended.Int32Value;
                motionData.RefreshCalories();
            } 
        }

        public CoreMotionData GetData()
        {
            return motionData;
        }
    }
}
