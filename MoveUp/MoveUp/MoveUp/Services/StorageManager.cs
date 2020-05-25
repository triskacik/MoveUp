﻿using System;
using System.Collections.Generic;
using System.IO;
using MoveUp.Models;
using SQLite;

namespace MoveUp.Services.Interfaces
{
    public class StorageManager : IStorageManager
    {
        private string path;
        public SQLiteConnection connection { get; set; }

        public StorageManager()
        {
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3");

            connection = new SQLiteConnection(path);
            connection.CreateTable<CoreMotionData>();
            connection.CreateTable<HikingSavedData>();
            connection.CreateTable<SavedPosition>();
            connection.CreateTable<NotificationData>();
        }

        public TableQuery<CoreMotionData> GetCoreMotionTable()
        {
            return connection.Table<CoreMotionData>();
        }

        public void InsertCoreMotionData(CoreMotionData data)
        {
            connection.Insert(data);
        }

        public TableQuery<HikingSavedData> GetHikingDataTable()
        {
            return connection.Table<HikingSavedData>();
        }

        public void InsertHikingData(HikingSavedData data)
        {
            connection.Insert(data);
        }

        public void DeleteHikingData(HikingSavedData data)
        {
            connection.Delete(data);
        }

        public void InsertTodaysPositions(List<SavedPosition> data)
        {
            var query = connection.Table<SavedPosition>();
            connection.DeleteAll(query.Table);
            connection.InsertAll(data);
        }

        public List<SavedPosition> GetTodaysPositions()
        {
            var query = connection.Table<SavedPosition>();
            if (query.Count() > 0)
            {
                if(query.First().Date.Date != DateTime.Now.Date)
                    connection.DeleteAll(query.Table);
            }
            return query.ToList();
        }

        public void InsertNotification(NotificationData data)
        {
            connection.Insert(data);
        }

        public List<NotificationData> GetNotifications()
        {
            var query = connection.Table<NotificationData>();

            foreach (var data in query)
            {
                if (data.Date < DateTime.Now)
                {
                    connection.Delete(data);
                }
            }

            return query.ToList();
        }
    }
}
