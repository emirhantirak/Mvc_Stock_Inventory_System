using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcInventorySystemProject.Controllers;
using MvcInventorySystemProject.Models.Entity;

namespace MvcInventorySystemProject.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales

        DbInventoryEntities db = new DbInventoryEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewSale()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewSale(Tbl_Sales sale)
        {
            db.Tbl_Sales.Add(sale);
            db.SaveChanges();
            return View("Index"); 
        }
    }
}