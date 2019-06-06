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
        readonly USDrate usd = new USDrate();
        readonly EURrate eur = new EURrate();


        [HttpGet]
        public ActionResult StoragePage()
        {
            ViewBag.Value = 1;
            return View(db.Products);
        }

        [HttpPost]
        public ActionResult StoragePage(string valueType)
        { //свитч блок?? комплексная модель??
            if (valueType == "rubValue")
            { 
                ViewBag.Valute = "rub";
                return RedirectToAction("StoragePage");
            }

            else if (valueType == "eurValue")
            {
                ViewBag.Value = 74.2344M; //поменять на eur.GetEUR();
                ViewBag.Valute = "eur";
                return View(db.Products);
            }

            else if (valueType == "usdValue")
            {
                ViewBag.Value = 62.4543M; //поменять на usd.GetUSD();
                ViewBag.Valute = "usd";
                return View(db.Products);
            }
            else return RedirectToAction("StoragePage"); //бесполезная ветка
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

            else return View("StoragePage"); // бесполезная ветка
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
            if (deletedProduct != null)
            {
                db.Products.Remove(deletedProduct);
                db.SaveChanges();
            }
            return RedirectToAction("StoragePage");
        }
    }
}