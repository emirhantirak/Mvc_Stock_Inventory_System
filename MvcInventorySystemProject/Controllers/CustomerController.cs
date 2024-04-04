using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcInventorySystemProject.Models.Entity;

namespace MvcInventorySystemProject.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer

        DbInventoryEntities db = new DbInventoryEntities();
        public ActionResult Index(string p)
        {
            var values = from d in db.Tbl_Customers select d;
            if(!string.IsNullOrEmpty(p))
            {
                values = values.Where(x => x.CustomerName.Contains(p));
            }
            return View(values.ToList());
            //var values = db.Tbl_Customers.ToList();
            //return View(values);
        }

        [HttpGet]
        public ActionResult NewCustomer()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult NewCustomer(Tbl_Customers cstmr)
        //{
        //    db.Tbl_Customers.Add(cstmr);
        //    db.SaveChanges();
        //    return View();
        //}

        [HttpPost]
        public ActionResult NewCustomer(Tbl_Customers cstmr)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Customers.Add(cstmr);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Customer has been added successfully!";
                return RedirectToAction("NewCustomer");
            }
            else
            {
                return View("NewCustomer");
            }

            //return View(cstmr);
            
        }
        public ActionResult Delete(int id)
        {
            var customer = db.Tbl_Customers.Find(id);
            db.Tbl_Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetCustomer(int id)
        {
            var customer = db.Tbl_Customers.Find(id);
            return View("GetCustomer", customer);
        }
        public ActionResult Update(Tbl_Customers cust)
        {
            var value = db.Tbl_Customers.Find(cust.CustomerID);
            value.CustomerName = cust.CustomerName;
            value.CustomerSurname = cust.CustomerSurname;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}