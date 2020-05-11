using System;
using MoveUp.Models;
using MoveUp.Services.Interfaces;

namespace MoveUp.Services
{
    public class HistoryDataFeeder : IHistoryDataFeeder
    {
        private HikingSavedData hikingData = new HikingSavedData();

        public HikingSavedData GetHikingData()
        {
            return hikingData;
        }

        public void SetHikingData(HikingSavedData data)
        {
            hikingData = data;
        }
    }
}
