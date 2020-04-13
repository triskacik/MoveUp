using System.Collections.Generic;
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
        private ICommandFactory commandFactory;
        private ICoreMotionController motionController;
        private INavigationService navigationService;

        public CoreMotionData MotionData { get; set; }
        public CoreMotionWeeklyData WeeklyMotionData { get; set; }

        public Chart WeekChart { get; set; }

        public StepsViewModel(ICommandFactory commandFac, ICoreMotionController motionContr, INavigationService navigation)
        {
            commandFactory = commandFac;
            motionController = motionContr;
            navigationService = navigation;
            InitializeMotion();
        }

        private async void InitializeMotion()
        {
            await motionController.TriggerPedometerAsync();
            MotionData = motionController.GetData();
            WeeklyMotionData = await motionController.GetWeeklyDataAsync();
            InitializeCharts();
        }

        private void InitializeCharts()
        {
            List<Entry> weeklyEntries = new List<Entry>();

            float colorHue = 0;
            foreach (var motionData in WeeklyMotionData.Data)
            {
                weeklyEntries.Add(new Entry(motionData.Steps)
                {
                    Label = motionData.Date.DayOfWeek.ToString(),
                    Color = SKColor.FromHsl(colorHue, 100, 50),
                    TextColor = SKColor.Parse("#000000"),
                    ValueLabel = motionData.Steps.ToString()
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
    }
}
