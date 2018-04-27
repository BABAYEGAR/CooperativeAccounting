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
            return View(_databaseConnection.TransactionTypes.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TransactionType transactionType)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            transactionType.CreatedBy = signedInUserId;
            transactionType.LastModifiedBy = signedInUserId;
            transactionType.DateCreated = DateTime.Now;
            transactionType.DateLastModified = DateTime.Now;
            _databaseConnection.TransactionTypes.Add(transactionType);
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully added a new Transaction Type!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(long id)
        {
            var transactionType = _databaseConnection.TransactionTypes.Find(id);
            return View(transactionType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TransactionType transactionType)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            transactionType.LastModifiedBy = signedInUserId;
            transactionType.DateLastModified = DateTime.Now;
            _databaseConnection.Entry(transactionType).State = EntityState.Modified;
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully modified the Account Type!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(long id)
        {
            try
            {
                var transactionType = _databaseConnection.TransactionTypes.Find(id);
                _databaseConnection.TransactionTypes.Remove(transactionType);
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