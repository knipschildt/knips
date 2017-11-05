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
    public class CategoryDTOController : Controller
    {
        private db db = new db();

        // GET: CategoryDTO
        public ActionResult Index()
        {
            return View(db.CategoryDTOes.ToList());
        }

        // GET: CategoryDTO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryDTO categoryDTO = db.CategoryDTOes.Find(id);
            if (categoryDTO == null)
            {
                return HttpNotFound();
            }
            return View(categoryDTO);
        }

        // GET: CategoryDTO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryDTO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "categoryID,catName")] CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                db.CategoryDTOes.Add(categoryDTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoryDTO);
        }

        // GET: CategoryDTO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryDTO categoryDTO = db.CategoryDTOes.Find(id);
            if (categoryDTO == null)
            {
                return HttpNotFound();
            }
            return View(categoryDTO);
        }

        // POST: CategoryDTO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "categoryID,catName")] CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryDTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryDTO);
        }

        // GET: CategoryDTO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryDTO categoryDTO = db.CategoryDTOes.Find(id);
            if (categoryDTO == null)
            {
                return HttpNotFound();
            }
            return View(categoryDTO);
        }

        // POST: CategoryDTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryDTO categoryDTO = db.CategoryDTOes.Find(id);
            db.CategoryDTOes.Remove(categoryDTO);
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
