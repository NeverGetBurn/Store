using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Models;
namespace Store.Core.Controllers
{
    public class StorageController : Controller
    {

        private readonly USDrate usd = new USDrate();
        private readonly EURrate eur = new EURrate();

        private List<Product> ProductsList
        {
            get
            {
                using (Context db = new Context())
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
                    using (Context db = new Context())
                    {
                        products.AddRange(
                            db.Products
                            .Where(w => w.Name.Contains(model.SearchString))
                            .ToList());
                    }
                    break;
                case SearchType.byDescription:
                    using (Context db = new Context())
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
                using (Context db = new Context())
                {
                    db.Products.Add(product);
                    db.SaveChanges();
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
            using (Context db = new Context())
            {
                Product deletedProduct = db.Products.Find(ID);
                if (deletedProduct != null)
                {
                    db.Products.Remove(deletedProduct);
                    db.SaveChanges();
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