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
    public class RoleController : Controller
    {
        private readonly CooperativeAccountingDataContext _databaseConnection;

        /// <summary>
        ///     Intitialize some connections from the class constructor
        /// </summary>
        /// <param name="databaseConnection"></param>
        public RoleController(CooperativeAccountingDataContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public IActionResult Index()
        {
            return View(_databaseConnection.Roles.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(Role role)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            role.CreatedBy = signedInUserId;
            role.LastModifiedBy = signedInUserId;
            role.DateCreated = DateTime.Now;
            role.DateLastModified = DateTime.Now;
            _databaseConnection.Roles.Add(role);
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully added a new role!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Edit(Role role)
        {
            var signedInUserId = HttpContext.Session.GetInt32("LoggedInUser");
            role.LastModifiedBy = signedInUserId;
            role.DateLastModified = DateTime.Now;
            _databaseConnection.Entry(role).State = EntityState.Modified;
            _databaseConnection.SaveChanges();
            TempData["display"] = "You have successfully modified the role!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(long id)
        {
            try
            {
                var role = _databaseConnection.Roles.Find(id);
                _databaseConnection.Roles.Remove(role);
                _databaseConnection.SaveChanges();
                TempData["display"] = "You have successfully deleted the role!";
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