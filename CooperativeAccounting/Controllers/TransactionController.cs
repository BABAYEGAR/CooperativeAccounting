using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooperativeAccounting.Models.DataBaseConnections;
using CooperativeAccounting.Models.Entities;
using CooperativeAccounting.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            return View(_databaseConnection.Transactions.Include(n => n.AppUser).Include(n => n.TransactionType).ToList());
        }
        public IActionResult Years()
        {
            return View();
        }
        public IActionResult Months(int? id)
        {
            ViewBag.Year = id;
            return View();
        }
        public IActionResult CashContribution(int year,int month)
        {
            ViewBag.Year = year;
            ViewBag.Month = month;
            return View(_databaseConnection.Transactions.Include(n => n.AppUser).Include(n => n.TransactionType)
                .ToList().Where(n => n.TransactionType.Cash).ToList().Where(n=>n.DateCreated.Year == year && n.DateCreated.Month == month).ToList());
        }
        public IActionResult PersonalLedger(long id)
        {
            return View(_databaseConnection.Transactions.Include(n => n.AppUser).Include(n => n.TransactionType).Where(n=>n.AppUserId == id).ToList());
        }
        public IActionResult CashBook()
        {
            return View(_databaseConnection.Transactions.Include(n => n.AppUser).Include(n=>n.TransactionType)
                .ToList().Where(n=>n.TransactionType.Cash).ToList());
        }
        public IActionResult TrialBalance()
        {
            return View(_databaseConnection.Transactions.Include(n => n.AppUser).Include(n => n.TransactionType)
                .ToList().Where(n => n.TransactionType.Cash).ToList());
        }
        public IActionResult BalanceSheet()
        {
            return View(_databaseConnection.Transactions.Include(n => n.AppUser).Include(n => n.TransactionType)
                .ToList().Where(n => n.TransactionType.Asset || n.TransactionType.Equity || n.TransactionType.Liability).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.AppUserId = new SelectList(_databaseConnection.AppUsers.Where(n=>n.RoleId > 1).ToList(), "AppUserId",
                "Name");
            ViewBag.TransactionTypeId = new SelectList(_databaseConnection.TransactionTypes.ToList(), "TransactionTypeId",
                "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Transaction transaction)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            transaction.CreatedBy = signedInUserId;
            transaction.LastModifiedBy = signedInUserId;
            transaction.DateCreated = DateTime.Now;
            transaction.DateLastModified = DateTime.Now;

            var accountType = _databaseConnection.TransactionTypes.Find(transaction.TransactionTypeId);
            if (accountType.Cash)
            {
                //generate Voucher Number
                var generator = new Random();
                var number = generator.Next(0, 1000000).ToString("D8");

                transaction.VoucherNumber = "CRP-" + number;
            }

            if (accountType.Debit)
            {
                transaction.Action = TransactionAction.Debit.ToString();
            }
            if (accountType.Credit)
            {
                transaction.Action = TransactionAction.Credit.ToString();
            }
            _databaseConnection.Transactions.Add(transaction);
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully added a new transaction!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(long id)
        {
            var transaction = _databaseConnection.Transactions.Find(id);
            ViewBag.AppUserId = new SelectList(_databaseConnection.AppUsers.Where(n => n.RoleId > 1).ToList(), "AppUserId",
                "Name",transaction.AppUserId);
            ViewBag.TransactionTypeId = new SelectList(_databaseConnection.TransactionTypes.ToList(), "TransactionTypeId",
                "Name",transaction.TransactionTypeId);
            return View(transaction);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Transaction transaction)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            transaction.LastModifiedBy = signedInUserId;
            transaction.DateLastModified = DateTime.Now;

            var accountType = _databaseConnection.TransactionTypes.Find(transaction.TransactionTypeId);
            if (accountType.Debit)
            {
                transaction.Action = TransactionAction.Debit.ToString();
            }
            if (accountType.Credit)
            {
                transaction.Action = TransactionAction.Credit.ToString();
            }
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