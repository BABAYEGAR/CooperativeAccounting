﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooperativeAccounting.Models.DataBaseConnections;
using CooperativeAccounting.Models.Encryption;
using CooperativeAccounting.Models.Entities;
using CooperativeAccounting.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [SessionExpireFilter]
        public IActionResult Index()
        {
            ViewBag.Transactions = _databaseConnection.Transactions.Include(n=>n.TransactionType).ToList();
            return View(_databaseConnection.AppUsers.ToList());
        }
        /// <summary>
        ///     Sends Json responds object to view with lga of the state requested via an Ajax call
        /// </summary>
        /// <param name="id"> state id</param>
        /// <returns>lgas</returns>
        /// Microsoft.CodeDom.Providers.DotNetCompilerPlatform
        public JsonResult GetLgaForState(int id)
        {
            var lgas = _databaseConnection.Lgas.Where(n => n.StateId == id);
            return Json(lgas);
        }
        [SessionExpireFilter]
        public IActionResult Create()
        {
            ViewBag.State = new SelectList(_databaseConnection.States, "StateId", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilter]
        public IActionResult Create(AppUser appUser)
        {

            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            appUser.CreatedBy = signedInUserId;
            appUser.LastModifiedBy = signedInUserId;
            appUser.DateCreated = DateTime.Now;
            appUser.DateLastModified = DateTime.Now;
            appUser.RoleId = 2;

            //generate password
            var generator = new Random();
            var number = generator.Next(0, 1000000).ToString("D6");

            appUser.Password = new Hashing().HashPassword(number);
            appUser.ConfirmPassword = appUser.Password;

            if (_databaseConnection.AppUsers.Where(n => n.Email == appUser.Email).ToList().Count > 0)
            {
                TempData["display"] = "A member with the same email already exist!";

                TempData["notificationtype"] = NotificationType.Error.ToString();
                return View(appUser);
            }
            _databaseConnection.AppUsers.Add(appUser);
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully added a new member!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        [SessionExpireFilter]
        public IActionResult Edit(long id)
        {
            var appUser = _databaseConnection.AppUsers.Find(id);
            ViewBag.State = new SelectList(_databaseConnection.States, "StateId", "Name",appUser.StateId);
            return View(appUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilter]
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
        [SessionExpireFilter]
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