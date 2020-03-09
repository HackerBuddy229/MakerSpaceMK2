using MakerSpaceMK2.Models.DatabaseModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.IO;


namespace MakerSpaceMK2.Services
{
    public class DataServices
    {
        public string LogDataName => nameof(LogDataName) + ".json";
        public string UserDataName => nameof(UserDataName) + ".json";


        public IEnumerable<UserDataModel> GetUserData()
        {
            var stringBuilder = new StringBuilder();
            string line;
            using (var streamReader = new StreamReader(this.UserDataName))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    stringBuilder.Append(line);
                }
            }

            var json = stringBuilder.ToString();

            var Users = JsonConvert.DeserializeObject<List<UserDataModel>>(json);

            if (Users == null)
            {
                return new List<UserDataModel>();
            }

            return Users.ToArray();
        }

        public IEnumerable<LogDataModel> GetLogData()
        {
            var stringBuilder = new StringBuilder();
            string line;
            using (var streamReader = new StreamReader(this.LogDataName))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    stringBuilder.Append(line);
                }
            }

            var json = stringBuilder.ToString();

            var Log = JsonConvert.DeserializeObject<List<LogDataModel>>(json);
            if (Log == null)
            {
                return new List<LogDataModel>();
            }
            return Log.ToArray();
        }
        public void SaveUserData(List<UserDataModel> newData, bool replaceData)
        {
            var list = new List<UserDataModel>();
            if (!replaceData)
            {
                foreach (var user in GetUserData())
                {
                    list.Add(user);
                }
            }

            foreach (var user in newData)
            {
                list.Add(user);
            }

            var json = JsonConvert.SerializeObject(list, Formatting.Indented);

            using (var streamWriter = new StreamWriter(this.UserDataName))
            {
                streamWriter.Write(json);
            }


        }

        public void SaveLogData(List<LogDataModel> newData, bool replaceData)
        {
            var list = new List<LogDataModel>();
            if (!replaceData)
            {
                foreach (var log in GetLogData())
                {
                    list.Add(log);
                }
            }

            foreach (var log in newData)
            {
                list.Add(log);
            }

            var json = JsonConvert.SerializeObject(list, Formatting.Indented);

            using (var streamWriter = new StreamWriter(this.LogDataName))
            {
                streamWriter.Write(json);
            }





        }




    }
}