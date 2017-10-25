using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VMtest.Models;

namespace VMtest.Controllers
{
    public class ProductController : Controller
    {
        private db db = new db();

        // GET: Product
        public ActionResult Index()
        {
            var productDTOes = db.ProductDTOes.Include(p => p.categories);
            return View(productDTOes.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDTO productDTO = db.ProductDTOes.Find(id);
            if (productDTO == null)
            {
                return HttpNotFound();
            }
            return View(productDTO);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.categoryID = new SelectList(db.CategoryDTOes, "categoryID", "catName");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productID,productName,categoryID")] ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                db.ProductDTOes.Add(productDTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryID = new SelectList(db.CategoryDTOes, "categoryID", "catName", productDTO.categoryID);
            return View(productDTO);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDTO productDTO = db.ProductDTOes.Find(id);
            if (productDTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryID = new SelectList(db.CategoryDTOes, "categoryID", "catName", productDTO.categoryID);
            return View(productDTO);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productID,productName,categoryID")] ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productDTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryID = new SelectList(db.CategoryDTOes, "categoryID", "catName", productDTO.categoryID);
            return View(productDTO);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDTO productDTO = db.ProductDTOes.Find(id);
            if (productDTO == null)
            {
                return HttpNotFound();
            }
            return View(productDTO);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductDTO productDTO = db.ProductDTOes.Find(id);
            db.ProductDTOes.Remove(productDTO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
