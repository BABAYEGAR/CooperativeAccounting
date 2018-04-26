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
    public class TransactionTypeController : Controller
    {

        private readonly CooperativeAccountingDataContext _databaseConnection;

        /// <summary>
        ///     Intitialize some connections from the class constructor
        /// </summary>
        /// <param name="databaseConnection"></param>
        public TransactionTypeController(CooperativeAccountingDataContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public IActionResult Index()
        {
            return View(_databaseConnection.AccountTypes.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(TransactionType accountType)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            accountType.CreatedBy = signedInUserId;
            accountType.LastModifiedBy = signedInUserId;
            accountType.DateCreated = DateTime.Now;
            accountType.DateLastModified = DateTime.Now;
            _databaseConnection.AccountTypes.Add(accountType);
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully added a new Account Type!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Edit(TransactionType accountType)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            accountType.LastModifiedBy = signedInUserId;
            accountType.DateLastModified = DateTime.Now;
            _databaseConnection.Entry(accountType).State = EntityState.Modified;
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully modified the Account Type!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(long id)
        {
            try
            {
                var boxType = _databaseConnection.AccountTypes.Find(id);
                _databaseConnection.AccountTypes.Remove(boxType);
                _databaseConnection.SaveChanges();
                TempData["display"] = "You have successfully deleted the Account Type!";
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