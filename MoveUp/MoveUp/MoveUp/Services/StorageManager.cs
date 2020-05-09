using System;
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
        }

        public TableQuery<CoreMotionData> GetCoreMotionTable()
        {
            return connection.Table<CoreMotionData>();
        }

        public void InsertCoreMotionData(CoreMotionData data)
        {
            connection.Insert(data);
        }
    }
}
