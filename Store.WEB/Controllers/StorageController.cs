using System.Collections.Generic;
using System.Linq;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.WEB.Models;
using Store.CORE.Models;
using Store.CORE.Interfaces;

namespace Store.WEB.Controllers
{
    public class StorageController : Controller
    {
        private readonly ILogger<StorageController> _logger;
        private readonly Func<IStorage> _contex;
        public StorageController(ILogger<StorageController> logger, Func<IStorage> context)
        {
            _logger  = logger ?? throw new ArgumentNullException(nameof(logger));
            _contex = context ?? throw new ArgumentNullException(nameof(context));           
        }

        private readonly USDrate usd = new USDrate();
        private readonly EURrate eur = new EURrate();

        private List<Product> ProductsList
        {
            get
            {
                using (IStorage db = _contex())
                {
                    return db.Products.ToList();
                }
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
                    using (IStorage db = _contex())
                    {
                        products.AddRange(
                            db.Products
                            .Where(w => w.Name.Contains(model.SearchString))
                            .ToList());
                    }
                    break;
                case SearchType.byDescription:
                    using (IStorage db = _contex())
                    {
                        products.AddRange(
                            db.Products
                            .Where(w => w.Description.Contains(model.SearchString))
                            .ToList());
                    }
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
                using (IStorage db = _contex())
                {
                    // db.Products.Add(product);
                    // db.SaveChanges();
                    db.Insert(product);
                }
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
            using (IStorage db = _contex())
            {
                Product deletedProduct = db.Products.FirstOrDefault(w=> w.Id == ID);
                if (deletedProduct != null)
                {
                    // db.Products.Remove(deletedProduct);
                    // db.SaveChanges();
                    db.Delete(deletedProduct);
                }
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