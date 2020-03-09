using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakerSpaceMK2.Models.DatabaseModels
{
    public class LogDataModel
    {

        public UserDataModel UserData { get; set; }
        public DateTime LogTime { get; set; }

        public LogDataModel(UserDataModel userData, DateTime logTime)
        {
            UserData = userData;
            LogTime = logTime;
        }
    }
}