using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcInventorySystemProject.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcInventorySystemProject.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category

        DbInventoryEntities db = new DbInventoryEntities();
        public ActionResult Index(int page = 1)
        {
            //var values = db.Tbl_Categories.ToList();
            var values = db.Tbl_Categories.ToList().ToPagedList(page,5);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewCategory()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult NewCategory(Tbl_Categories ctgry)
        //{
        //    db.Tbl_Categories.Add(ctgry);
        //    db.SaveChanges();
        //    return View();
        //}

        [HttpPost]
        public ActionResult NewCategory(Tbl_Categories ctgry)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Categories.Add(ctgry);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Category has been added successfully!";
                return RedirectToAction("NewCategory");
            }
            else
            {
                return View("NewCategory");
            }

            //return View(ctgry);

        }
        public ActionResult Delete(int id)
        {
            var category = db.Tbl_Categories.Find(id);
            db.Tbl_Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetCategory(int id)
        {
            var category = db.Tbl_Categories.Find(id);
            return View("GetCategory", category);
        }
        public ActionResult Update(Tbl_Categories cat)
        {
            var value = db.Tbl_Categories.Find(cat.CategoryID);
            value.CategoryName = cat.CategoryName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}