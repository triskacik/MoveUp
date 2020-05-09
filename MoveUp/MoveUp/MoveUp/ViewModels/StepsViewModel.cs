using System.Collections.Generic;
using System.Windows.Input;
using Microcharts;
using MoveUp.Factories.Interfaces;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;
using SkiaSharp;

namespace MoveUp.ViewModels
{
    public class StepsViewModel : ViewModelBase
    {
        private ICoreMotionController motionController;
        private ICoreMotionStorage storageManager;
        private IPreferencesStorage preferencesStorage;

        public CoreMotionData MotionData { get; set; }
        public CoreMotionWeeklyData WeeklyMotionData { get; set; }
        public List<CoreMotionMonthlyData> MonthlyMotionData { get; set; }
        public CoreMotionMonthlyData ThisMonthMotionData { get; set; }
        public int MaxDailySteps { get; set; }
        public int StepsTarget { get; set; }
        public CoreMotionData AllTimeAverageData { get; set; }

        public Chart WeekChart { get; set; }
        public Chart MonthChart { get; set; }

        public ICommand SetTargetCom { get; set; }

        public StepsViewModel(ICommandFactory commandFac,
                              ICoreMotionController motionContr,
                              INavigationService navigation,
                              ICoreMotionStorage storage,
                              IPreferencesStorage preferences) : base(navigation, commandFac)
        {
            motionController = motionContr;
            storageManager = storage;
            preferencesStorage = preferences;

            SetTargetCom = commandFac.CreateCommand(SetTarget);

            StepsTarget = preferencesStorage.GetStepsTarget();

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
            FindMaxSteps();
        }

        private void InitializeWeeklyChart()
        {
            List<Entry> weeklyEntries = new List<Entry>();

            foreach (var motionData in WeeklyMotionData.Data)
            {
                float colorHue = (float) motionData.Steps / StepsTarget * 100;
                if (colorHue > 100)
                    colorHue = 100;

                weeklyEntries.Add(new Entry(motionData.Steps)
                {
                    Label = motionData.Date.DayOfWeek.ToString(),
                    Color = SKColor.FromHsl(colorHue, 100, 45),
                    TextColor = SKColor.Parse("#000000"),
                    ValueLabel = motionData.Steps.ToString()
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
                float colorHue = (float) MonthlyMotionData[i].Steps / StepsTarget * 100;
                if (colorHue > 100)
                    colorHue = 100;

                monthlyEntries.Add(new Entry(MonthlyMotionData[i].Steps)
                {
                    Label = MonthlyMotionData[i].Date.Month.ToString(),
                    Color = SKColor.FromHsl(colorHue, 100, 45),
                    TextColor = SKColor.Parse("#000000"),
                    ValueLabel = MonthlyMotionData[i].Steps.ToString()
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

        private void FindMaxSteps()
        {
            int currentMax = preferencesStorage.GetMaxSteps();

            foreach (var motionData in WeeklyMotionData.Data)
            {
                if (motionData.Steps > currentMax)
                {
                    currentMax = motionData.Steps;
                }
            }

            preferencesStorage.SetMaxSteps(currentMax);
            MaxDailySteps = currentMax;
        }

        private void SetTarget()
        {
            preferencesStorage.SetStepsTarget(StepsTarget);
            InitializeWeeklyChart();
            InitializeMonthlyChart();
        }
    }
}
