using System;
using System.Collections.Generic;
using System.Linq;
using CooperativeAccounting.Models.DataBaseConnections;
using CooperativeAccounting.Models.Encryption;
using CooperativeAccounting.Models.Entities;
using CooperativeAccounting.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        [SessionExpireFilter]
        public IActionResult Index()
        {
            return View(_databaseConnection.Transactions.Include(n => n.AppUser).Include(n => n.TransactionType).ToList());
        }
        [SessionExpireFilter]
        public IActionResult Years(string page)
        {
            ViewBag.Page = page;
            return View();
        }
        [SessionExpireFilter]
        public IActionResult Months(int? id, string page)
        {
            ViewBag.Year = id;
            ViewBag.Page = page;
            return View();
        }
        [SessionExpireFilter]
        public IActionResult SalaryDeduction(int year, int month)
        {
            ViewBag.Year = year;
            ViewBag.Month = month;
            ViewBag.Loans= _databaseConnection.Loans.Include(n => n.AppUser).ToList();
            ViewBag.Welfares = _databaseConnection.Welfares.Include(n => n.AppUser).ToList();
            return View(_databaseConnection.AppUsers.ToList());
        }
        [SessionExpireFilter]
        public IActionResult CashContribution(int year,int month)
        {
            ViewBag.Year = year;
            ViewBag.Month = month;
            return View(_databaseConnection.Transactions.Include(n => n.AppUser).Include(n => n.TransactionType)
                .ToList().Where(n => n.TransactionType.Cash).ToList().Where(n=>n.DateCreated.Year == year && n.DateCreated.Month == month).ToList());
        }
        public IActionResult PayCashContribution(int year, int month)
        {
            ViewBag.Year = year;
            ViewBag.Month = month;
            return View(_databaseConnection.AppUsers.ToList());
        }

        /// <summary>
        ///     Search for photographer
        /// </summary>
        /// <param name="table_records"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PayCashContribution(int[] table_records)
        {
            try
            {
                List<Transaction>usersTransactions = new List<Transaction>();
                if (table_records != null && table_records.Length > 0)
                {
                    var length = table_records.Length;
                    for (var i = 0; i < length; i++)
                    {
                        long userId = Convert.ToInt64(table_records[i]);
                        var user = _databaseConnection.AppUsers.Find(userId);
                        var transaction = new Transaction
                        {
                            DateCreated = DateTime.Now,
                            TransactionDate = DateTime.Now,
                            TransactionName = "Monthly Contribution",
                            CreatedBy = userId,
                            LastModifiedBy = userId,
                            AppUserId = userId,
                            Action = TransactionAction.Credit.ToString(),
                            Amount = user.MonthlyContribution,
                            TransactionTypeId = 1,
                            BankId = 1,
                            DateLastModified = DateTime.Now,
                            VoucherNumber = "NIL"
                        };

                        usersTransactions.Add(transaction);

                    }
                    _databaseConnection.Transactions.AddRange(usersTransactions);
                    _databaseConnection.SaveChanges();
                    TempData["display"] = "You have successfully paid the member(s) monthly contribution!";
                    TempData["notificationtype"] = NotificationType.Success.ToString();
                    return RedirectToAction("PayCashContribution");
                }

                TempData["display"] = "You did not select any member!";
                TempData["notificationtype"] = NotificationType.Error.ToString();
                return RedirectToAction("PayCashContribution");


            }
            catch (Exception ex)
            {
                TempData["display"] = ex.ToString();
                TempData["notificationtype"] = NotificationType.Error.ToString();
                return RedirectToAction("PayCashContribution");
            }
        }

        [SessionExpireFilter]
        public IActionResult PersonalLedger(long id)
        {
            return View(_databaseConnection.Transactions.Include(n => n.AppUser).Include(n => n.TransactionType).Where(n=>n.AppUserId == id).ToList());
        }
        [SessionExpireFilter]
        public IActionResult CashBook()
        {
            return View(_databaseConnection.Transactions.Include(n => n.AppUser).Include(n=>n.TransactionType)
                .ToList().Where(n=>n.TransactionType.Cash).ToList());
        }
        [SessionExpireFilter]
        public IActionResult TrialBalance()
        {
            return View(_databaseConnection.Transactions.Include(n => n.AppUser).Include(n => n.TransactionType)
                .ToList().Where(n => n.TransactionType.Cash).ToList());
        }
        [SessionExpireFilter]
        public IActionResult BalanceSheet()
        {
            return View(_databaseConnection.Transactions.Include(n => n.AppUser).Include(n => n.TransactionType)
                .ToList().Where(n => n.TransactionType.Asset || n.TransactionType.Equity || n.TransactionType.Liability).ToList());
        }
        [SessionExpireFilter]
        public IActionResult Create()
        {
            ViewBag.BankId = new SelectList(_databaseConnection.Banks.ToList(), "BankId",
                "Name");
            ViewBag.AppUserId = new SelectList(_databaseConnection.AppUsers.Where(n=>n.RoleId > 1).ToList(), "AppUserId",
                "Name");
            ViewBag.TransactionTypeId = new SelectList(_databaseConnection.TransactionTypes.Where(n=>n.TransactionTypeId != 2).ToList(), "TransactionTypeId",
                "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilter]
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
        [SessionExpireFilter]
        public IActionResult Edit(long id)
        {
            var transaction = _databaseConnection.Transactions.Find(id);
            ViewBag.AppUserId = new SelectList(_databaseConnection.AppUsers.Where(n => n.RoleId > 1).ToList(), "AppUserId",
                "Name",transaction.AppUserId);
            ViewBag.TransactionTypeId = new SelectList(_databaseConnection.TransactionTypes.Where(n => n.TransactionTypeId != 2).ToList(), "TransactionTypeId",
                "Name",transaction.TransactionTypeId);
            ViewBag.BankId = new SelectList(_databaseConnection.Banks.ToList(), "BankId",
                "Name",transaction.BankId);
            return View(transaction);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilter]
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
        [SessionExpireFilter]
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