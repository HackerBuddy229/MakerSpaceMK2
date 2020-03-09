using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakerSpaceMK2.Models;
using MakerSpaceMK2.Models.DatabaseModels;
using MakerSpaceMK2.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakerSpaceMK2.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(NewUserModel newUserModel, DataServices dataServices)
        {
            var adminKey = LocalConfigurationServices.GetAdminKey();
            if (!newUserModel.AdminKey.Equals(adminKey))
            {
                return View();
            }
            var salt = PasswordServices.SaltGen();
            var newUser = new UserDataModel(newUserModel.Username, newUserModel.Name, PasswordServices.HashGen(salt, newUserModel.Password), salt);
            var newUserList = new List<UserDataModel>();
            newUserList.Add(newUser);
            dataServices.SaveUserData(newUserList, false);
            return RedirectToAction(actionName: "Index", controllerName: "Admin");
        }


        [HttpGet]
        public IActionResult DeleteUser()
        {
            return View();
        }


        [HttpPost]
        public IActionResult DeleteUser(DeleteUserRequest deleteUserRequest, DataServices dataServices)
        {
            var user = dataServices.GetUserData().Where(u => u.Username.Equals(deleteUserRequest.Username));
            if (user.Count() == 0)
            {
                return View();
            }
            var adminKey = LocalConfigurationServices.GetAdminKey();
            if (deleteUserRequest.AdminKey != adminKey)
            {
                return View();
            }
            var users = dataServices.GetUserData().Where(u => !u.Username.Equals(deleteUserRequest.Username));
            var usersList = users.ToList();
            dataServices.SaveUserData(usersList, true);
            return RedirectToAction(actionName: "Index", controllerName: "Admin");
        }


    }
}