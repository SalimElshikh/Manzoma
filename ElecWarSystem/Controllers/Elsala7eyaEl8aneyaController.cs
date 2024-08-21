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
using ElecWarSystem.Models.OutDoor.OutDoorNew;

namespace ElecWarSystem.Controllers
{
    public class Elsala7eyaEl8aneyaController : Controller
    {
        private AppDBContext db = new AppDBContext();

        // GET: Elsala7eyaEl8aneya
        public async Task<ActionResult> Index()
        {
            var elsala7eyaEl8aneyas = db.Elsala7eyaEl8aneyas.Include(e => e.MakanEsla7s).Include(e => e.MostawaEIsla7s).Include(e => e.TypeMo2edas);
            return View(await elsala7eyaEl8aneyas.ToListAsync());
        }

        // GET: Elsala7eyaEl8aneya/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elsala7eyaEl8aneya elsala7eyaEl8aneya = await db.Elsala7eyaEl8aneyas.FindAsync(id);
            if (elsala7eyaEl8aneya == null)
            {
                return HttpNotFound();
            }
            return View(elsala7eyaEl8aneya);
        }

        // GET: Elsala7eyaEl8aneya/Create
        public ActionResult Create()
        {
            ViewBag.MakanEsla7_ID = new SelectList(db.MakanEsla7s, "ID", "Name");
            ViewBag.MostawaEIsla7_ID = new SelectList(db.MostawaEIsla7s, "ID", "Name");
            ViewBag.TypeMo2eda_ID = new SelectList(db.TypeMo2edas, "ID", "Name");
            return View();
        }

        // POST: Elsala7eyaEl8aneya/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NameMo2eda,NumMo2eda,El2gra,MakanEsla7_ID,MostawaEIsla7_ID,TypeMo2eda_ID")] Elsala7eyaEl8aneya elsala7eyaEl8aneya)
        {
            if (ModelState.IsValid)
            {
                db.Elsala7eyaEl8aneyas.Add(elsala7eyaEl8aneya);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MakanEsla7_ID = new SelectList(db.MakanEsla7s, "ID", "Name", elsala7eyaEl8aneya.MakanEsla7_ID);
            ViewBag.MostawaEIsla7_ID = new SelectList(db.MostawaEIsla7s, "ID", "Name", elsala7eyaEl8aneya.MostawaEIsla7_ID);
            ViewBag.TypeMo2eda_ID = new SelectList(db.TypeMo2edas, "ID", "Name", elsala7eyaEl8aneya.TypeMo2eda_ID);
            return View(elsala7eyaEl8aneya);
        }

        // GET: Elsala7eyaEl8aneya/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elsala7eyaEl8aneya elsala7eyaEl8aneya = await db.Elsala7eyaEl8aneyas.FindAsync(id);
            if (elsala7eyaEl8aneya == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakanEsla7_ID = new SelectList(db.MakanEsla7s, "ID", "Name", elsala7eyaEl8aneya.MakanEsla7_ID);
            ViewBag.MostawaEIsla7_ID = new SelectList(db.MostawaEIsla7s, "ID", "Name", elsala7eyaEl8aneya.MostawaEIsla7_ID);
            ViewBag.TypeMo2eda_ID = new SelectList(db.TypeMo2edas, "ID", "Name", elsala7eyaEl8aneya.TypeMo2eda_ID);
            return View(elsala7eyaEl8aneya);
        }

        // POST: Elsala7eyaEl8aneya/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NameMo2eda,NumMo2eda,El2gra,MakanEsla7_ID,MostawaEIsla7_ID,TypeMo2eda_ID")] Elsala7eyaEl8aneya elsala7eyaEl8aneya)
        {
            if (ModelState.IsValid)
            {
                db.Entry(elsala7eyaEl8aneya).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MakanEsla7_ID = new SelectList(db.MakanEsla7s, "ID", "Name", elsala7eyaEl8aneya.MakanEsla7_ID);
            ViewBag.MostawaEIsla7_ID = new SelectList(db.MostawaEIsla7s, "ID", "Name", elsala7eyaEl8aneya.MostawaEIsla7_ID);
            ViewBag.TypeMo2eda_ID = new SelectList(db.TypeMo2edas, "ID", "Name", elsala7eyaEl8aneya.TypeMo2eda_ID);
            return View(elsala7eyaEl8aneya);
        }

        // GET: Elsala7eyaEl8aneya/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elsala7eyaEl8aneya elsala7eyaEl8aneya = await db.Elsala7eyaEl8aneyas.FindAsync(id);
            if (elsala7eyaEl8aneya == null)
            {
                return HttpNotFound();
            }
            return View(elsala7eyaEl8aneya);
        }

        // POST: Elsala7eyaEl8aneya/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Elsala7eyaEl8aneya elsala7eyaEl8aneya = await db.Elsala7eyaEl8aneyas.FindAsync(id);
            db.Elsala7eyaEl8aneyas.Remove(elsala7eyaEl8aneya);
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
