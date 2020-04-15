using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using SQLite;
using Xamarin.Forms;

namespace MoveUp.Services
{
    public class SqLiteStorageManager : IStorageManager
    {
        private string path;
        private SQLiteConnection connection;

        private ICoreMotionController motionController;

        public SqLiteStorageManager(ICoreMotionController motionContr)
        {
            motionController = motionContr;

            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3");

            connection = new SQLiteConnection(path);
            connection.CreateTable<CoreMotionData>();
        }

        private async Task RefreshDataAsync( )
        {
            var weeklyData = await motionController.GetWeeklyDataAsync();

            if (connection.Table<CoreMotionData>().Count() == 0)
            {
                foreach (var data in weeklyData.Data)
                {
                    if (data.Date < DateTime.Today)
                    {
                        connection.Insert(data);
                    } 
                }
            } else
            {
                var lastEntryDate = connection.Table<CoreMotionData>().OrderByDescending(data => data.Date).First().Date;

                if (lastEntryDate.Date < DateTime.Today)
                {
                    foreach (var data in weeklyData.Data)
                    {
                        if (data.Date.Date > lastEntryDate.Date && data.Date < DateTime.Today)
                        {
                            connection.Insert(data);
                        }
                    }
                }
            }
        }

        public async Task<List<CoreMotionMonthlyData>> GetMonthlyDataAsync()
        {
            await RefreshDataAsync();

            List<CoreMotionMonthlyData> data = new List<CoreMotionMonthlyData>();

            foreach (var entry in connection.Table<CoreMotionData>().OrderBy(d => d.Date))
            {
                if (data.Count == 0)
                {
                    data.Add(new CoreMotionMonthlyData(entry.Date));
                    data[0].AddEntry(entry);
                } else
                {
                    int last = data.Count - 1;
                    if (data[last].Date.Month != entry.Date.Month)
                    {
                        data.Add(new CoreMotionMonthlyData(entry.Date));
                        data[last + 1].AddEntry(entry);
                    } else
                    {
                        data[last].AddEntry(entry);
                    }
                }
            }

            if (data.Count == 0)
            {
                data.Add(new CoreMotionMonthlyData(DateTime.Now));
            }

            data[data.Count - 1].AddEntry(motionController.GetData());

            return data;
        }

        public async Task<CoreMotionData> GetAllTimeAverageDataAsync()
        {
            await RefreshDataAsync();

            int steps = 0;
            double distance = 0;
            int floors = 0;
            int entries = 0;

            foreach (var entry in connection.Table<CoreMotionData>())
            {
                entries += 1;
                steps += entry.Steps;
                distance += entry.Distance;
                floors += entry.Floors;
            }

            CoreMotionData today = motionController.GetData();
            entries += 1;
            steps += today.Steps;
            floors += today.Floors;
            distance += today.Distance;

            return new CoreMotionData()
            {
                Date = DateTime.Now,
                Steps = steps / entries,
                Floors = floors / entries,
                Distance = Math.Round(distance / entries, 3)
            };
        }
    }
}
