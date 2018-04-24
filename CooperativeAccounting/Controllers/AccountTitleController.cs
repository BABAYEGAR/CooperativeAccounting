using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CooperativeAccounting.Controllers
{
    public class AccountTitleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}