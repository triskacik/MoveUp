using System;
using System.Collections.Generic;

namespace MoveUp.Models
{
    public enum ActivitiesEnum
    {
        Hiking
    }

    public static class ActivityTypes
    {
        public static List<string> GetAll()
        {
            return new List<string>() { "Hiking" };
        }
    }
}
