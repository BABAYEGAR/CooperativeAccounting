using System.Linq;
using CooperativeAccounting.Models.DataBaseConnections;
using CooperativeAccounting.Models.Encryption;
using CooperativeAccounting.Models.Entities;
using CooperativeAccounting.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CooperativeAccounting.Controllers
{
    public class AccountController : Controller
    {
        private readonly CooperativeAccountingDataContext _databaseConnection;

        /// <summary>
        ///     Intitialize some connections from the class constructor
        /// </summary>
        /// <param name="databaseConnection"></param>
        public AccountController(CooperativeAccountingDataContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public IActionResult Login(string returnUrl)
        {
            if (returnUrl != null && returnUrl == "sessionExpired")
            {
                _databaseConnection.Dispose();
                HttpContext.Session.Clear();

                //display notification
                TempData["display"] = "Your session has expired, Login to continue!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                return View();
            }
            _databaseConnection.Dispose();
            HttpContext.Session.Clear();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AccountModel model)
        {
            var user = _databaseConnection.AppUsers.Include(n=>n.Role).SingleOrDefault(n => n.Email == model.Email);
            if (user != null)
            {
                var passwordCorrect = new Hashing().ValidatePassword(model.Password, user.Password);
                if (passwordCorrect)
                {
                    var userString = JsonConvert.SerializeObject(user);
                    HttpContext.Session.SetString("User", userString);
                    HttpContext.Session.SetInt32("LoggedInUser", (int) user.AppUserId);
                    TempData["display"] = "Welcome Back "+user.Name + "!";
                    TempData["notificationtype"] = NotificationType.Success.ToString();
                    return RedirectToAction("Dashboard", "Home");

                }
                else
                {
                    TempData["display"] = "Your Login details are wrong, Try again!";
                    TempData["notificationtype"] = NotificationType.Error.ToString();
                    return View(model);
                }
            }
            else
            {
                TempData["display"] = "Your Account does not exist, Try again!";
                TempData["notificationtype"] = NotificationType.Error.ToString();
                return View(model);
            }
            return View();
        }
        public IActionResult LogOut()
        {
            return RedirectToAction("Login");
        }
    }
}