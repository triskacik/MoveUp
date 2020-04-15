using System;
using System.Collections.Generic;
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
        private ICommandFactory commandFactory;
        private ICoreMotionController motionController;
        private INavigationService navigationService;
        private IStorageManager storageManager;

        public CoreMotionData MotionData { get; set; }
        public CoreMotionWeeklyData WeeklyMotionData { get; set; }
        public List<CoreMotionMonthlyData> MonthlyMotionData { get; set; }
        public CoreMotionMonthlyData ThisMonthMotionData { get; set; }
        public double MaxDailyDistance { get; set; }
        public CoreMotionData AllTimeAverageData { get; set; }

        public Chart WeekChart { get; set; }
        public Chart MonthChart { get; set; }

        public DistanceViewModel(ICommandFactory commandFac, ICoreMotionController motionContr, INavigationService navigation, IStorageManager storage)
        {
            commandFactory = commandFac;
            motionController = motionContr;
            navigationService = navigation;
            storageManager = storage;

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

            float colorHue = 0;
            foreach (var motionData in WeeklyMotionData.Data)
            {
                weeklyEntries.Add(new Entry((float) motionData.Distance)
                {
                    Label = motionData.Date.DayOfWeek.ToString(),
                    Color = SKColor.FromHsl(colorHue, 100, 50),
                    TextColor = SKColor.Parse("#000000"),
                    ValueLabel = motionData.Distance.ToString()
                });
                colorHue += 8;
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

            float colorHue = 0;
            for (int i = MonthlyMotionData.Count - 1; i >= 0 && i > MonthlyMotionData.Count - 8; i--)
            {
                monthlyEntries.Add(new Entry((float) MonthlyMotionData[i].Distance)
                {
                    Label = MonthlyMotionData[i].Date.Month.ToString(),
                    Color = SKColor.FromHsl(colorHue, 100, 50),
                    TextColor = SKColor.Parse("#000000"),
                    ValueLabel = MonthlyMotionData[i].Distance.ToString()
                });
                colorHue += 8;
            }

            int emptySlots = 7 - monthlyEntries.Count;
            for (int i = 1; i <= emptySlots; i++)
            {
                monthlyEntries.Add(new Entry(0)
                {
                    Color = SKColor.FromHsl(0, 100, 50),
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
            double maximum = 0;

            if (Preferences.ContainsKey("DistanceMax"))
            {
                maximum = Preferences.Get("DistanceMax", 0);
            }
            else
            {
                Preferences.Set("DistanceMax", 0);
            }

            foreach (var motionData in WeeklyMotionData.Data)
            {
                if (motionData.Distance > maximum)
                {
                    maximum = motionData.Distance;
                }
            }

            Preferences.Set("DistanceMax", maximum);
            MaxDailyDistance = maximum;
        }
    }
}
