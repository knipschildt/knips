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
    public class SalesController : Controller
    {
        private db db = new db();

        // GET: Sales
        public ActionResult Index()
        {
            var salesDTOes = db.SalesDTOes.Include(s => s.product);
            return View(salesDTOes.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesDTO salesDTO = db.SalesDTOes.Find(id);
            if (salesDTO == null)
            {
                return HttpNotFound();
            }
            return View(salesDTO);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.productId = new SelectList(db.ProductDTOes, "productID", "productName");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "saleId,productId,SaleDate")] SalesDTO salesDTO)
        {
            if (ModelState.IsValid)
            {
                db.SalesDTOes.Add(salesDTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productId = new SelectList(db.ProductDTOes, "productID", "productName", salesDTO.productId);
            return View(salesDTO);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesDTO salesDTO = db.SalesDTOes.Find(id);
            if (salesDTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.ProductDTOes, "productID", "productName", salesDTO.productId);
            return View(salesDTO);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "saleId,productId,SaleDate")] SalesDTO salesDTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesDTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productId = new SelectList(db.ProductDTOes, "productID", "productName", salesDTO.productId);
            return View(salesDTO);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesDTO salesDTO = db.SalesDTOes.Find(id);
            if (salesDTO == null)
            {
                return HttpNotFound();
            }
            return View(salesDTO);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesDTO salesDTO = db.SalesDTOes.Find(id);
            db.SalesDTOes.Remove(salesDTO);
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
