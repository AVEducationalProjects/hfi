using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HFi.Models;

namespace HFi.Controllers
{
    public class SourceCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SourceCategories
        public async Task<ActionResult> Index()
        {
            return View(await db.SourceCategories.ToListAsync());
        }

        // GET: SourceCategories/YearPlan/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SourceCategory sourceCategory = await db.SourceCategories.FindAsync(id);
            if (sourceCategory == null)
            {
                return HttpNotFound();
            }
            return View(sourceCategory);
        }

        // GET: SourceCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SourceCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] SourceCategory sourceCategory)
        {
            if (ModelState.IsValid)
            {
                db.SourceCategories.Add(sourceCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sourceCategory);
        }

        // GET: SourceCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SourceCategory sourceCategory = await db.SourceCategories.FindAsync(id);
            if (sourceCategory == null)
            {
                return HttpNotFound();
            }
            return View(sourceCategory);
        }

        // POST: SourceCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] SourceCategory sourceCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sourceCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sourceCategory);
        }

        // GET: SourceCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SourceCategory sourceCategory = await db.SourceCategories.FindAsync(id);
            if (sourceCategory == null)
            {
                return HttpNotFound();
            }
            return View(sourceCategory);
        }

        // POST: SourceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SourceCategory sourceCategory = await db.SourceCategories.FindAsync(id);
            db.SourceCategories.Remove(sourceCategory);
            await db.SaveChangesAsync();
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
