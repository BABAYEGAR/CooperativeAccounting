using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CooperativeAccounting.Models;
using CooperativeAccounting.Models.DataBaseConnections;
using CooperativeAccounting.Models.Encryption;
using CooperativeAccounting.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CooperativeAccounting.Controllers
{
    public class HomeController : Controller
    {
        private readonly CooperativeAccountingDataContext _databaseConnection;

        /// <summary>
        ///     Intitialize some connections from the class constructor
        /// </summary>
        /// <param name="databaseConnection"></param>
        public HomeController(CooperativeAccountingDataContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public IActionResult Index()
        {
            return View();
        }
        [SessionExpireFilter]
        public IActionResult Dashboard()
        {
            var appTransport = new AppTransport();
            appTransport.AppUsers = _databaseConnection.AppUsers.Where(n=>n.RoleId != 1).ToList();
            appTransport.CurrentBalance =
                (decimal) (_databaseConnection.Transactions.Include(n => n.TransactionType).Where(n => n.TransactionType.Credit).Sum(n => n.Amount) -
                           _databaseConnection.Transactions.Include(n => n.TransactionType).Where(n => n.TransactionType.Debit).Sum(n => n.Amount));
            appTransport.Transactions = _databaseConnection.Transactions.Include(n=>n.TransactionType).ToList();
            return View(appTransport);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
