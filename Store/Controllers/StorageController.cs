using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Models;

namespace Store.Controllers
{
    public class StorageController : Controller
    {
        Context db = new Context();

        [HttpGet]
        public ActionResult StoragePage()
        {
            return View(db.Products);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Status = true; // при добавлении модели статус становится "в наличии"
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("StoragePage");
            }
            else
                return View();
        }

        [HttpGet]
        public ActionResult RemoveProduct()
        {
            return View(db.Products);
        }
        
        [HttpPost]
        public ActionResult RemoveProduct(int ID)
        {
            Product deletedProduct = db.Products.Find(ID);
            if(deletedProduct != null)
            {
                db.Products.Remove(deletedProduct);
                db.SaveChanges();
            }
            return RedirectToAction("StoragePage");
        }
    }
}