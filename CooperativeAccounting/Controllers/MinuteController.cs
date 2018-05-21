using System;
using System.Linq;
using CooperativeAccounting.Models.DataBaseConnections;
using CooperativeAccounting.Models.Encryption;
using CooperativeAccounting.Models.Entities;
using CooperativeAccounting.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CooperativeAccounting.Controllers
{
    public class MinuteController : Controller
    {

        private readonly CooperativeAccountingDataContext _databaseConnection;

        /// <summary>
        ///     Intitialize some connections from the class constructor
        /// </summary>
        /// <param name="databaseConnection"></param>
        public MinuteController(CooperativeAccountingDataContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        [SessionExpireFilter]
        public IActionResult Index()
        {
            return View(_databaseConnection.Minutes.ToList());
        }
        [SessionExpireFilter]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [SessionExpireFilter]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Minute minute)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            minute.CreatedBy = signedInUserId;
            minute.LastModifiedBy = signedInUserId;
            minute.DateCreated = DateTime.Now;
            minute.DateLastModified = DateTime.Now;
            _databaseConnection.Minutes.Add(minute);
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully added a new Minute!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        [SessionExpireFilter]
        public IActionResult Edit(long id)
        {
            var minute = _databaseConnection.Minutes.Find(id);
            return View(minute);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilter]
        public IActionResult Edit(Minute minute)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            minute.LastModifiedBy = signedInUserId;
            minute.DateLastModified = DateTime.Now;
            _databaseConnection.Entry(minute).State = EntityState.Modified;
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully modified the Minute!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        [SessionExpireFilter]
        public ActionResult Delete(long id)
        {
            try
            {
                var minute = _databaseConnection.Minutes.Find(id);
                _databaseConnection.Minutes.Remove(minute);
                _databaseConnection.SaveChanges();
                TempData["display"] = "You have successfully deleted the Minute!";
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