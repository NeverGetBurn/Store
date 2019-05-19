using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Store.Models;
using System.Data.Entity;

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

        [HttpPost]
        public ActionResult SearchProduct(string searchString, string action)
        {
            if (action == "byName")
            {
                var prod = from p in db.Products
                           select p;
                if (!String.IsNullOrEmpty(searchString))
                {
                    prod = prod.Where(s => s.Name.Contains(searchString));
                }
                return View("StoragePage", prod);
            }

            else if (action == "byDescription")
            {
                var prod = from p in db.Products
                           select p;
                if (!String.IsNullOrEmpty(searchString))
                {
                    prod = prod.Where(s => s.Description.Contains(searchString));
                }
                return View("StoragePage", prod);
            }

            else return View("StoragePage"); // не решил как завершить эту ветку кода
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