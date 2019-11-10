using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace Store.Core.Controllers
{
    public class StorageController : Controller
    {
        private readonly Context _db;
        public StorageController(Context context)
        {
             _db = context ?? throw new ArgumentNullException(nameof(context));
        } 

        private readonly USDrate usd = new USDrate();
        private readonly EURrate eur = new EURrate();

        private List<Product> ProductsList
        {
            get
            {
                    return _db.Products.ToList();
                
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Product> products = new List<Product>();
            ViewBag.Value = 1;
            return View(ProductsList);
        }

        [HttpPost]
        public ActionResult Index(RateEnums valueType)
        { //свитч блок?? комплексная модель??
            switch (valueType)
            {
                case RateEnums.rubValue:
                    ViewBag.Valute = "rub";
                    break;
                case RateEnums.eurValue:
                    ViewBag.Value = 74.2344M; //поменять на eur.GetEUR();
                    ViewBag.Valute = "eur";
                    break;
                case RateEnums.usdValue:
                    ViewBag.Value = 62.4543M; //поменять на usd.GetUSD();
                    ViewBag.Valute = "usd";
                    break;
                default:
                    return RedirectToAction(nameof(SomeError));

            }
            return View(ProductsList);

        }

        public ActionResult SomeError()
        {
            return View("some");
        }

        [HttpPost]
        public ActionResult SearchProduct(SearchModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(SomeError));
            }

            List<Product> products = new List<Product>();
            switch (model.Action)
            {
                case SearchType.byName:
                    
                        products.AddRange(
                            _db.Products
                            .Where(w => w.Name.Contains(model.SearchString))
                            .ToList());
                    
                    break;
                case SearchType.byDescription:
                    
                        products.AddRange(
                            _db.Products
                            .Where(w => w.Description.Contains(model.SearchString))
                            .ToList());
                    
                    break;
                default:
                    return RedirectToAction(nameof(SomeError));
            }

            return View(nameof(Index), products);
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
                
                    _db.Products.Add(product);
                    _db.SaveChanges();
                
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult RemoveProduct()
        {
            return View(ProductsList);
        }

        [HttpPost]
        public ActionResult RemoveProduct(int ID)
        {
                var deletedProduct = _db.Products.FirstOrDefault(w=>w.Id == ID);
                if (deletedProduct != null)
                {
                    _db.Products.Remove(deletedProduct);
                    _db.SaveChanges();
                }
            
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public ActionResult Some()
        {
            return View();
        }
    }
}