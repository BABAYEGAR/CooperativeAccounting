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
    public class TransactionController : Controller
    {
        private readonly CooperativeAccountingDataContext _databaseConnection;

        /// <summary>
        ///     Intitialize some connections from the class constructor
        /// </summary>
        /// <param name="databaseConnection"></param>
        public TransactionController(CooperativeAccountingDataContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public IActionResult Index()
        {
            return View(_databaseConnection.Transactions.ToList());
        }
        public IActionResult CashBook()
        {
            return View(_databaseConnection.Transactions.Include(n=>n.AccountType).Include(n => n.AccountType)
                .ToList().Where(n=>n.AccountType.Cash));
        }
        public IActionResult TrialBalance()
        {
            return View(_databaseConnection.Transactions.Include(n => n.AccountType).Include(n => n.AccountType)
                .ToList().Where(n => n.AccountType.Cash));
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(Transaction transaction)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            transaction.CreatedBy = signedInUserId;
            transaction.LastModifiedBy = signedInUserId;
            transaction.DateCreated = DateTime.Now;
            transaction.DateLastModified = DateTime.Now;
            _databaseConnection.Transactions.Add(transaction);
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully added a new transaction!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Edit(Transaction transaction)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            transaction.LastModifiedBy = signedInUserId;
            transaction.DateLastModified = DateTime.Now;
            _databaseConnection.Entry(transaction).State = EntityState.Modified;
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully modified the transaction!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(long id)
        {
            try
            {
                var boxType = _databaseConnection.Transactions.Find(id);
                _databaseConnection.Transactions.Remove(boxType);
                _databaseConnection.SaveChanges();
                TempData["display"] = "You have successfully deleted the transaction!";
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