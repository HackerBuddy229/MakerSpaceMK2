using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MakerSpaceMK2.Models;
using MakerSpaceMK2.Models.DatabaseModels;
using System.Device.Gpio;
using MakerSpaceMK2.Services;
using System.Threading;

namespace MakerSpaceMK2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GpioController controller;
        private readonly int pinOut;

        public HomeController(ILogger<HomeController> logger, GpioController gpioController)
        {
            _logger = logger;
            this.controller = gpioController;
            

            pinOut = 17;
            controller.OpenPin(pinOut, PinMode.Output);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(LoginRequestModel lrm, DataServices dataServices)
        {
            lrm.RequestTime = DateTime.Now;



            var Users = dataServices.GetUserData();
            var user = Users.Where(u => u.Username.Equals(lrm.Username));
            if (user == null || !user.Any())
            {
                return View();
            }

            var userArray = user.ToArray();
            if (!PasswordServices.CompareHash(userArray[0].PasswordHash, PasswordServices.HashGen(userArray[0].Salt, lrm.Password)))
            {
                return View();
            }
            var newData = new List<LogDataModel>();
            newData.Add(new LogDataModel(userArray[0], DateTime.Now));
            dataServices.SaveLogData(newData, false);


            
            controller.Write(pinOut, PinValue.High);
            Thread.Sleep(1000);
            controller.Write(pinOut, PinValue.Low);
            
            return RedirectToAction(actionName: "Unlocked", controllerName: "Home");

        }

        [HttpGet]
        public IActionResult Information()
        {

            return View();
        }


        public IActionResult Unlocked()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
