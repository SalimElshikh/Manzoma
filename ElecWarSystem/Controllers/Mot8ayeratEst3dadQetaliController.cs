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
    public class Mot8ayeratEst3dadQetaliController : Controller
    {
        private AppDBContext db = new AppDBContext();

        // GET: Mot8ayeratEst3dadQetali
        public async Task<ActionResult> Index()
        {
            var mot8ayeratEst3dadQetalis = db.Mot8ayeratEst3dadQetalis.Include(m => m.A8radTa7arok).Include(m => m.Al7ala).Include(m => m.Ge7aTasdek);
            return View(await mot8ayeratEst3dadQetalis.ToListAsync());
        }

        // GET: Mot8ayeratEst3dadQetali/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mot8ayeratEst3dadQetali mot8ayeratEst3dadQetali = await db.Mot8ayeratEst3dadQetalis.FindAsync(id);
            if (mot8ayeratEst3dadQetali == null)
            {
                return HttpNotFound();
            }
            return View(mot8ayeratEst3dadQetali);
        }

        // GET: Mot8ayeratEst3dadQetali/Create
        public ActionResult Create()
        {
            ViewBag.A8radTa7arok_ID = new SelectList(db.A8radTa7arok, "ID", "a8radTa7arok");
            ViewBag.Al7ala_ID = new SelectList(db.Al7alas, "ID", "Name");
            ViewBag.Ge7aTasdek_ID = new SelectList(db.Ge7aTasdeks, "ID", "Name");
            return View();
        }

        // POST: Mot8ayeratEst3dadQetali/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Item,Al7ala_ID,Ge7aTasdek_ID,A8radTa7arok_ID,DateTo,DateFrom")] Mot8ayeratEst3dadQetali mot8ayeratEst3dadQetali)
        {
            if (ModelState.IsValid)
            {
                db.Mot8ayeratEst3dadQetalis.Add(mot8ayeratEst3dadQetali);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.A8radTa7arok_ID = new SelectList(db.A8radTa7arok, "ID", "a8radTa7arok", mot8ayeratEst3dadQetali.A8radTa7arok_ID);
            ViewBag.Al7ala_ID = new SelectList(db.Al7alas, "ID", "Name", mot8ayeratEst3dadQetali.Al7ala_ID);
            ViewBag.Ge7aTasdek_ID = new SelectList(db.Ge7aTasdeks, "ID", "Name", mot8ayeratEst3dadQetali.Ge7aTasdek_ID);
            return View(mot8ayeratEst3dadQetali);
        }

        // GET: Mot8ayeratEst3dadQetali/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mot8ayeratEst3dadQetali mot8ayeratEst3dadQetali = await db.Mot8ayeratEst3dadQetalis.FindAsync(id);
            if (mot8ayeratEst3dadQetali == null)
            {
                return HttpNotFound();
            }
            ViewBag.A8radTa7arok_ID = new SelectList(db.A8radTa7arok, "ID", "a8radTa7arok", mot8ayeratEst3dadQetali.A8radTa7arok_ID);
            ViewBag.Al7ala_ID = new SelectList(db.Al7alas, "ID", "Name", mot8ayeratEst3dadQetali.Al7ala_ID);
            ViewBag.Ge7aTasdek_ID = new SelectList(db.Ge7aTasdeks, "ID", "Name", mot8ayeratEst3dadQetali.Ge7aTasdek_ID);
            return View(mot8ayeratEst3dadQetali);
        }

        // POST: Mot8ayeratEst3dadQetali/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Item,Al7ala_ID,Ge7aTasdek_ID,A8radTa7arok_ID,DateTo,DateFrom")] Mot8ayeratEst3dadQetali mot8ayeratEst3dadQetali)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mot8ayeratEst3dadQetali).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.A8radTa7arok_ID = new SelectList(db.A8radTa7arok, "ID", "a8radTa7arok", mot8ayeratEst3dadQetali.A8radTa7arok_ID);
            ViewBag.Al7ala_ID = new SelectList(db.Al7alas, "ID", "Name", mot8ayeratEst3dadQetali.Al7ala_ID);
            ViewBag.Ge7aTasdek_ID = new SelectList(db.Ge7aTasdeks, "ID", "Name", mot8ayeratEst3dadQetali.Ge7aTasdek_ID);
            return View(mot8ayeratEst3dadQetali);
        }

        // GET: hhhs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mot8ayeratEst3dadQetali Mot8ayeratEst3dadQetali = await db.Mot8ayeratEst3dadQetalis.FindAsync(id);
            if (Mot8ayeratEst3dadQetali == null)
            {
                return HttpNotFound();
            }
            return View(Mot8ayeratEst3dadQetali);
        }

        // POST: hhhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Mot8ayeratEst3dadQetali Mot8ayeratEst3dadQetali = await db.Mot8ayeratEst3dadQetalis.FindAsync(id);
            db.Mot8ayeratEst3dadQetalis.Remove(Mot8ayeratEst3dadQetali);
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
