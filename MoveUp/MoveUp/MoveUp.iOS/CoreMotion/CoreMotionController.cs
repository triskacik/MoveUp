using System;
using System.Collections.Generic;
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
        private CMPedometer activityPedometer;

        private CMPedometerData pedometerData;
        private CMPedometerData pedometerActivityData = new CMPedometerData();

        private CoreMotionData motionData = new CoreMotionData();
        private CoreActivityData activityData;

        private bool couningAvailable = false;

        public int Steps { get; set; }

        public CoreMotionController()
        {
            if (CMPedometer.IsStepCountingAvailable)
            {
                couningAvailable = true;
                pedometer = new CMPedometer();
                activityPedometer = new CMPedometer();
            }
        }

        public async Task TriggerPedometerAsync()
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
            }
        }

        public void TriggerActivity()
        {
            if (couningAvailable)
            {
                activityData = new CoreActivityData();
                DateTime startDate = DateTime.Now;

                activityPedometer.StartPedometerUpdates(DateConverter.DateTimeToNSDate(startDate), UpdateActivityData);
            }
        }

        public void StopActivityUpdates()
        {
            activityPedometer.StopPedometerUpdates();
        }

        private void UpdateActivityData(CMPedometerData data, NSError error)
        {
            if (error == null)
            {
                pedometerActivityData = data;

                if (pedometerActivityData.AverageActivePace != null)
                    activityData.AvgSpeed = Math.Round(pedometerActivityData.AverageActivePace.DoubleValue * 3.6, 1);
                if (pedometerActivityData.Distance != null)
                    activityData.Distance = Math.Round(pedometerActivityData.Distance.DoubleValue / 1000, 2);
                if (pedometerActivityData.CurrentCadence != null)
                    activityData.Pace = Math.Round(pedometerActivityData.CurrentCadence.DoubleValue, 1);
            }
        }

        public CoreMotionData GetData()
        {
            return motionData;
        }

        public CoreActivityData GetActivityData()
        {
            return activityData;
        }

        public async Task<CoreMotionWeeklyData> GetWeeklyDataAsync()
        {
            List<CoreMotionData> data = new List<CoreMotionData>();

            if (couningAvailable)
            {
                for (int i = 6; i > 0; i--)
                {
                    DateTime startDate = DateTime.Today.AddDays(-i);
                    DateTime endDate = startDate.AddDays(1);

                    var pedometerData = await pedometer.QueryPedometerDataAsync(DateConverter.DateTimeToNSDate(startDate), DateConverter.DateTimeToNSDate(endDate));

                    CoreMotionData oneDayData = new CoreMotionData
                    {
                        Date = startDate,
                        Steps = pedometerData.NumberOfSteps.Int32Value,
                        Distance = pedometerData.Distance.Int32Value / 1000d,
                        Floors = pedometerData.FloorsAscended.Int32Value
                    };

                    data.Add(oneDayData);
                }

                data.Add(motionData);
            }

            return new CoreMotionWeeklyData(data);
        }
    }
}
