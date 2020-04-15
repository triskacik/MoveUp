using System;
using MoveUp.Helpers;

namespace MoveUp.Models
{
    public class CoreMotionMonthlyData : ModelBase
    {
        public DateTime Date { get; set; }

        private int numOfEntries;
        private int steps;
        private double distance;
        private int floors;

        public int Steps
        {
            get => steps / numOfEntries;
        }

        public double Distance
        {
            get => Math.Round(distance / numOfEntries, 3);
        }

        public int Floors
        {
            get => floors / numOfEntries;
        }

        public int Calories
        {
            get => CaloriesCounter.GetCalories(Distance);
        }

        public CoreMotionMonthlyData(DateTime date)
        {
            Date = date;
            numOfEntries = 0;
            steps = 0;
            distance = 0;
            floors = 0;
        }

        public void AddEntry(CoreMotionData data)
        {
            numOfEntries += 1;
            steps += data.Steps;
            distance += data.Distance;
            floors += data.Floors;
        }
    } 
}
