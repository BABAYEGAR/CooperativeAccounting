using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooperativeAccounting.Models.DataBaseConnections;
using CooperativeAccounting.Models.Entities;
using CooperativeAccounting.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CooperativeAccounting.Controllers
{
    public class AppUserController : Controller
    {
        private readonly CooperativeAccountingDataContext _databaseConnection;

        /// <summary>
        ///     Intitialize some connections from the class constructor
        /// </summary>
        /// <param name="databaseConnection"></param>
        public AppUserController(CooperativeAccountingDataContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public IActionResult Index()
        {
            return View(_databaseConnection.AppUsers.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(AppUser appUser)
        {

            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            appUser.CreatedBy = signedInUserId;
            appUser.LastModifiedBy = signedInUserId;
            appUser.DateCreated = DateTime.Now;
            appUser.DateLastModified = DateTime.Now;

            //generate password
            var generator = new Random();
            var number = generator.Next(0, 1000000).ToString("D6");

            appUser.Password = number;
            appUser.ConfirmPassword = number;

            _databaseConnection.AppUsers.Add(appUser);
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully added a new member!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Edit(AppUser appUser)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            appUser.LastModifiedBy = signedInUserId;
            appUser.DateLastModified = DateTime.Now;
            _databaseConnection.Entry(appUser).State = EntityState.Modified;
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully modified the member!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(long id)
        {
            try
            {
                var appUser = _databaseConnection.AppUsers.Find(id);
                _databaseConnection.AppUsers.Remove(appUser);
                _databaseConnection.SaveChanges();
                TempData["display"] = "You have successfully deleted the member!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
    }
}