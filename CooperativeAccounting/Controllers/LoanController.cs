using System;
using System.Linq;
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
    public class LoanController : Controller
    {
        private readonly CooperativeAccountingDataContext _databaseConnection;

        /// <summary>
        ///     Intitialize some connections from the class constructor
        /// </summary>
        /// <param name="databaseConnection"></param>
        public LoanController(CooperativeAccountingDataContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        [SessionExpireFilter]
        public IActionResult Index()
        {
            return View(_databaseConnection.Loans.Include(n => n.AppUser).Include(n => n.TransactionType).ToList());
        }
        [SessionExpireFilter]
        public IActionResult Create(string id)
        {
            ViewBag.AppUserId = new SelectList(_databaseConnection.AppUsers.Where(n => n.RoleId > 1).ToList(),
                "AppUserId",
                "Name");
            ViewBag.BankId = new SelectList(_databaseConnection.Banks.ToList(), "BankId",
                "Name");
            var loan = new Loan();
            if (id == "yes")
            {
                loan.Emergency = true;
            }
            return View(loan);
        }

        [HttpPost]
        [SessionExpireFilter]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Loan loan)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            loan.CreatedBy = signedInUserId;
            loan.LastModifiedBy = signedInUserId;
            loan.DateCreated = DateTime.Now;
            loan.DateLastModified = DateTime.Now;
            loan.TransactionTypeId = 2;

            var transaction = new Transaction
            {
                CreatedBy = signedInUserId,
                LastModifiedBy = signedInUserId,
                DateCreated = DateTime.Now,
                DateLastModified = DateTime.Now,
                Amount = loan.Amount,
                AppUserId = loan.AppUserId,
                TransactionDate = loan.TransactionDate,
                TransactionName = loan.Purpose,
                TransactionTypeId = 2
            };
            var accountType = _databaseConnection.TransactionTypes.Find(loan.TransactionTypeId);
            if (accountType.Debit || accountType.Loan)
            {
                transaction.Action = TransactionAction.Debit.ToString();
            }

            if (accountType.Credit)
            {
                transaction.Action = TransactionAction.Credit.ToString();
            }

            _databaseConnection.Transactions.Add(transaction);
            _databaseConnection.SaveChanges();

            loan.TransactionId = transaction.TransactionId;
            if (String.IsNullOrEmpty(loan.FirstGuarantorName))
            {
                loan.FirstGuarantorName = "Guarantor";
            }
            if (String.IsNullOrEmpty(loan.SecondGuarantorName))
            {
                loan.SecondGuarantorName = "Guarantor";
            }
            if (String.IsNullOrEmpty(loan.FirstGuarantorMobile))
            {
                loan.FirstGuarantorMobile = "00000000";
            }
            if (String.IsNullOrEmpty(loan.SecondGuarantorMobile))
            {
                loan.SecondGuarantorMobile = "00000000";
            }
            _databaseConnection.Loans.Add(loan);
            _databaseConnection.SaveChanges();

            TempData["display"] = "You have successfully applied for a new loan!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        [SessionExpireFilter]
        public IActionResult Edit(long id)
        {
            var loan = _databaseConnection.Loans.Find(id);
            ViewBag.AppUserId = new SelectList(_databaseConnection.AppUsers.Where(n => n.RoleId > 1).ToList(),
                "AppUserId",
                "Name", loan.AppUserId);
            ViewBag.BankId = new SelectList(_databaseConnection.Banks.ToList(), "BankId",
                "Name", loan.BankId);
            return View(loan);
        }

        [HttpPost]
        [SessionExpireFilter]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Loan loan)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            loan.LastModifiedBy = signedInUserId;
            loan.DateLastModified = DateTime.Now;

            var transaction = _databaseConnection.Transactions.Find(loan.TransactionId);
            var accountType = _databaseConnection.TransactionTypes.Find(loan.TransactionTypeId);
            if (accountType.Debit || accountType.Loan)
            {
                transaction.Action = TransactionAction.Debit.ToString();
            }

            if (accountType.Credit)
            {
                transaction.Action = TransactionAction.Credit.ToString();
            }

            _databaseConnection.Entry(transaction).State = EntityState.Modified;
            _databaseConnection.SaveChanges();

            _databaseConnection.Entry(loan).State = EntityState.Modified;
            _databaseConnection.SaveChanges();

            TempData["display"] = "You have successfully modified the loan!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        [SessionExpireFilter]
        public ActionResult Delete(long id)
        {
            try
            {
                var loan = _databaseConnection.Loans.Find(id);
                _databaseConnection.Loans.Remove(loan);
                _databaseConnection.SaveChanges();
                TempData["display"] = "You have successfully deleted the loan!";
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