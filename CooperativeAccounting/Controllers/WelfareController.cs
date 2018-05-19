using System;
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
    public class WelfareController : Controller
    {

        private readonly CooperativeAccountingDataContext _databaseConnection;

        /// <summary>
        ///     Intitialize some connections from the class constructor
        /// </summary>
        /// <param name="databaseConnection"></param>
        public WelfareController(CooperativeAccountingDataContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        [SessionExpireFilter]
        public IActionResult Index()
        {
            return View(_databaseConnection.Welfares.ToList());
        }
        [SessionExpireFilter]
        public IActionResult Create(string member)
        {
            var welfare = new Welfare();
            if (!String.IsNullOrEmpty(member) && member == "yes")
            {
                welfare.Member = true;
            }
            if (!String.IsNullOrEmpty(member) && member == "no")
            {
                welfare.Member = false;
            }

            ViewBag.AppUserId = new SelectList(_databaseConnection.AppUsers.Where(n=>n.RoleId > 1).ToList(), "AppUserId",
                "Name");
            return View(welfare);
        }
        [HttpPost]
        [SessionExpireFilter]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Welfare welfare)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            welfare.CreatedBy = signedInUserId;
            welfare.LastModifiedBy = signedInUserId;
            welfare.DateCreated = DateTime.Now;
            welfare.DateLastModified = DateTime.Now;
            _databaseConnection.Welfares.Add(welfare);
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully added a new Welfare Request!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        [SessionExpireFilter]
        public IActionResult Edit(long id)
        {
            var welfare = _databaseConnection.Welfares.Find(id);
            ViewBag.AppUserId = new SelectList(_databaseConnection.AppUsers.Where(n => n.RoleId > 1).ToList(), "AppUserId",
                "Name",welfare.AppUserId);
            return View(welfare);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilter]
        public IActionResult Edit(Welfare welfare)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            welfare.LastModifiedBy = signedInUserId;
            welfare.DateLastModified = DateTime.Now;
            _databaseConnection.Entry(welfare).State = EntityState.Modified;
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully modified the Welfare Request!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        [SessionExpireFilter]
        public ActionResult Delete(long id)
        {
            try
            {
                var welfare = _databaseConnection.Welfares.Find(id);
                _databaseConnection.Welfares.Remove(welfare);
                _databaseConnection.SaveChanges();
                TempData["display"] = "You have successfully deleted the Welfare Request!";
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