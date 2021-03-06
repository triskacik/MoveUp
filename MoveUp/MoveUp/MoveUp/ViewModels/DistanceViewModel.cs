﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using Microcharts;
using MoveUp.Factories.Interfaces;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;
using SkiaSharp;
using Xamarin.Essentials;

namespace MoveUp.ViewModels
{
    public class DistanceViewModel : ViewModelBase
    {
        private ICoreMotionController motionController;
        private ICoreMotionStorage storageManager;
        private IPreferencesStorage preferencesStorage;

        public CoreMotionData MotionData { get; set; }
        public CoreMotionWeeklyData WeeklyMotionData { get; set; }
        public List<CoreMotionMonthlyData> MonthlyMotionData { get; set; }
        public CoreMotionMonthlyData ThisMonthMotionData { get; set; }
        public double MaxDailyDistance { get; set; }
        public double DistanceTarget { get; set; }
        public CoreMotionData AllTimeAverageData { get; set; }

        public Chart WeekChart { get; set; }
        public Chart MonthChart { get; set; }

        public ICommand SetTargetCom { get; set; }

        public DistanceViewModel(ICommandFactory commandFac,
                                 ICoreMotionController motionContr,
                                 INavigationService navigation,
                                 ICoreMotionStorage storage,
                                 IPreferencesStorage preferences) : base(navigation, commandFac)
        {
            motionController = motionContr;
            storageManager = storage;
            preferencesStorage = preferences;

            SetTargetCom = commandFac.CreateCommand(SetTarget);

            DistanceTarget = preferencesStorage.GetDistanceTarget();

            InitializeMotionAsync();
        }

        private async void InitializeMotionAsync()
        {
            await motionController.TriggerPedometerAsync();
            MotionData = motionController.GetData();
            WeeklyMotionData = await motionController.GetWeeklyDataAsync();
            MonthlyMotionData = await storageManager.GetMonthlyDataAsync();
            AllTimeAverageData = await storageManager.GetAllTimeAverageDataAsync();
            ThisMonthMotionData = MonthlyMotionData[MonthlyMotionData.Count - 1];
            InitializeWeeklyChart();
            InitializeMonthlyChart();
            FindMaxDistance();
        }

        private void InitializeWeeklyChart()
        {
            List<Entry> weeklyEntries = new List<Entry>();

            foreach (var motionData in WeeklyMotionData.Data)
            {
                float colorHue = (float) (motionData.Distance / DistanceTarget) * 100;
                if (colorHue > 100)
                    colorHue = 100;

                weeklyEntries.Add(new Entry((float) motionData.Distance)
                {
                    Label = motionData.Date.DayOfWeek.ToString(),
                    Color = SKColor.FromHsl(colorHue, 100, 45),
                    TextColor = SKColor.Parse("#000000"),
                    ValueLabel = motionData.Distance.ToString()
                });
            }

            WeekChart = new BarChart()
            {
                Entries = weeklyEntries,
                BackgroundColor = SKColor.Empty,
                LabelTextSize = 25
            };
        }

        private void InitializeMonthlyChart()
        {
            List<Entry> monthlyEntries = new List<Entry>();

            for (int i = MonthlyMotionData.Count - 1; i >= 0 && i > MonthlyMotionData.Count - 8; i--)
            {
                float colorHue = (float) (MonthlyMotionData[i].Distance / DistanceTarget) * 100;
                if (colorHue > 100)
                    colorHue = 100;

                monthlyEntries.Add(new Entry((float) MonthlyMotionData[i].Distance)
                {
                    Label = MonthlyMotionData[i].Date.Month.ToString(),
                    Color = SKColor.FromHsl(colorHue, 100, 45),
                    TextColor = SKColor.Parse("#000000"),
                    ValueLabel = MonthlyMotionData[i].Distance.ToString()
                });
            }

            int emptySlots = 7 - monthlyEntries.Count;
            for (int i = 1; i <= emptySlots; i++)
            {
                monthlyEntries.Add(new Entry(0)
                {
                    Color = SKColor.FromHsl(0, 100, 45),
                    TextColor = SKColor.Parse("#000000")
                });
            }

            monthlyEntries.Reverse();

            MonthChart = new BarChart()
            {
                Entries = monthlyEntries,
                BackgroundColor = SKColor.Empty,
                LabelTextSize = 25
            };
        }

        private void FindMaxDistance()
        {
            double maximum = preferencesStorage.GetMaxDistance();

            foreach (var motionData in WeeklyMotionData.Data)
            {
                if (motionData.Distance > maximum)
                {
                    maximum = motionData.Distance;
                }
            }

            preferencesStorage.SetMaxDistance(maximum);
            MaxDailyDistance = maximum;
        }

        private void SetTarget()
        {
            preferencesStorage.SetDistanceTarget(DistanceTarget);
            InitializeWeeklyChart();
            InitializeMonthlyChart();
        }
    }
}
