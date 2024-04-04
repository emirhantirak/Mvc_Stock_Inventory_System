using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcInventorySystemProject.Models.Entity;

namespace MvcInventorySystemProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        DbInventoryEntities db = new DbInventoryEntities();
        public ActionResult Index()
        {
            var values = db.Tbl_Products.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult NewProduct()
        {
            List<SelectListItem> values = (from i in db.Tbl_Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.CategoryName,
                                               Value = i.CategoryID.ToString()
                                           }).ToList();
            ViewBag.prd = values;
            return View();
        }

        [HttpPost]
        public ActionResult NewProduct(Tbl_Products prdct)
        {
            if (ModelState.IsValid)
            {
                var ctg = db.Tbl_Categories.FirstOrDefault(x => x.CategoryID == prdct.ProductCategory);
                prdct.Tbl_Categories = ctg;

                db.Tbl_Products.Add(prdct);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Product has been added successfully!";
                return RedirectToAction("NewProduct");
            }


            return View(prdct);

        }
        public ActionResult Delete(int id)
        {
            var product = db.Tbl_Products.Find(id);
            db.Tbl_Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetProduct(int id)
        {
            var product = db.Tbl_Products.Find(id);

            List<SelectListItem> values = (from i in db.Tbl_Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.CategoryName,
                                               Value = i.CategoryID.ToString()
                                           }).ToList();
            ViewBag.prd = values;

            return View("GetProduct", product);
        }
        public ActionResult Update(Tbl_Products pro)
        {
            var value = db.Tbl_Products.Find(pro.ProductID);
            value.ProductName = pro.ProductName;
            value.Brand = pro.Brand;
            //value.ProductCategory = pro.ProductCategory;
            value.Price = pro.Price;
            value.Stock = pro.Stock;
            var ctg = db.Tbl_Categories.FirstOrDefault(x => x.CategoryID == pro.ProductCategory);
            value.ProductCategory = ctg.CategoryID;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}