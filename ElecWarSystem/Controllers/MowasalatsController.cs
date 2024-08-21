using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElecWarSystem.Data;
using ElecWarSystem.Models;

namespace ElecWarSystem.Controllers
{
    public class MowasalatsController : Controller
    {
        private AppDBContext db = new AppDBContext();

        // GET: Mowasalats
        public async Task<ActionResult> Index()
        {
            return View(await db.Mowasalats.ToListAsync());
        }

        // GET: Mowasalats/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mowasalat mowasalat = await db.Mowasalats.FindAsync(id);
            if (mowasalat == null)
            {
                return HttpNotFound();
            }
            return View(mowasalat);
        }

        // GET: Mowasalats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mowasalats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,EstrategyQa2ed,EstrategyMarkazAmaliat,EstrategyTa7wela,SentralQa2ed,SentralMarkazAmaliat,MowaslatLaselkya,TransfarData,HotNumber")] Mowasalat mowasalat)
        {
            if (ModelState.IsValid)
            {
                db.Mowasalats.Add(mowasalat);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mowasalat);
        }

        // GET: Mowasalats/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mowasalat mowasalat = await db.Mowasalats.FindAsync(id);
            if (mowasalat == null)
            {
                return HttpNotFound();
            }
            return View(mowasalat);
        }

        // POST: Mowasalats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,EstrategyQa2ed,EstrategyMarkazAmaliat,EstrategyTa7wela,SentralQa2ed,SentralMarkazAmaliat,MowaslatLaselkya,TransfarData,HotNumber")] Mowasalat mowasalat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mowasalat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mowasalat);
        }

        // GET: Mowasalats/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mowasalat mowasalat = await db.Mowasalats.FindAsync(id);
            if (mowasalat == null)
            {
                return HttpNotFound();
            }
            return View(mowasalat);
        }

        // POST: Mowasalats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Mowasalat mowasalat = await db.Mowasalats.FindAsync(id);
            db.Mowasalats.Remove(mowasalat);
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
