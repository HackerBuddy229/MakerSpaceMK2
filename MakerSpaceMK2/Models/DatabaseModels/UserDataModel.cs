using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakerSpaceMK2.Models.DatabaseModels
{
    public class UserDataModel
    {

        public string Username { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public byte[] Salt { get; set; }

        public UserDataModel(string username, string name, string passwordHash, byte[] salt)
        {
            Username = username;
            Name = name;
            PasswordHash = passwordHash;
            Salt = salt;
        }
    }
}