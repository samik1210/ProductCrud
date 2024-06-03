using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductCrud.Models;

namespace ProductCrud.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            using (ProductDbEntities db = new ProductDbEntities())
            {
                var listofData = db.Products.ToList();
                return View(listofData);
            }
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product model)
        {

            using (var context = new ProductDbEntities())
            {
                context.Products.Add(model);
                context.SaveChanges();
                TempData["SuccessMessage"] = "Product data Created Successfully";
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var context = new ProductDbEntities())
            {
                var data = context.Products.Where(x => x.SN == id).FirstOrDefault();
                return View(data);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product Model)
        {
            using (var context = new ProductDbEntities())
            {
                var data = context.Products.Where(x => x.SN == Model.SN).FirstOrDefault();
                if (data != null)
                {
                    data.ProductName = Model.ProductName;
                    data.Description = Model.Description;
                    data.Created = Model.Created;
                    context.SaveChanges();
                    TempData["SuccessMessage"] = "Product data Updated Successfully";
                }
            }
            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ProductDbEntities())
            {
                var data = context.Products.Where(x => x.SN == id).FirstOrDefault();
                context.Products.Remove(data);
                context.SaveChanges();
                TempData["SuccessMessage"] = "Product data Deleted Successfully";
                return RedirectToAction("index");
            }
        }
        public ActionResult Details(int id)
        {
            using (var context = new ProductDbEntities())
            {
                var data = context.Products.Where(x => x.SN == id).FirstOrDefault();
                return View(data);
            }
        }
    }
}