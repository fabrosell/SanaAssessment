using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Domain.Model;
using Domain.Repositories;

namespace SanaAssessment.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository = null;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        // GET: Product
        public ActionResult Index()
        {
            return View(_repository.List());
        }

        // GET: Product/Details/5
        public ActionResult Details(Guid id)
        {
            var p = _repository.Get(id);
            return View(p);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                _repository.Insert(GetProductFromFormCollection(collection));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(Guid id)
        {
            var p = _repository.Get(id);
            return View(p);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {                
                _repository.Update(GetProductFromFormCollection(collection));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(Guid id)
        {
            var p = _repository.Get(id);
            return View(p);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                _repository.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Storage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Storage(Storage storage, FormCollection collection)
        {
            try
            {                
                UnityConfig.RegisterComponents(storage.Name);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private Product GetProductFromFormCollection(FormCollection collection)
        {
            // I opted for not to double-validate input from form. This won't be the case for a release app.
            return new Product()
            {
                ProductId = collection["productId"] != null ? Guid.Parse(collection["ProductId"]) : Guid.Empty,
                ProductNumber = Int32.Parse(collection["ProductNumber"]),
                Title = collection["Title"],
                Price = Decimal.Parse(collection["Price"])
            };
        }
    }
}
