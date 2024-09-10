using ElecWarSystem.Data;
using ElecWarSystem.Models;
using ElecWarSystem.Serivces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElecWarSystem.Controllers
{
    public class We7daRa2eeseyaController : Controller
    {
        private readonly AppDBContext dBContext;
        private readonly FardDetailsService FardDetailsService;
        private readonly SmallUnitService smallUnitService;
        private readonly UnitService unitService;
        public We7daRa2eeseyaController()
        {
            dBContext = new AppDBContext();
            FardDetailsService = new FardDetailsService();
            smallUnitService = new SmallUnitService();
            unitService = new UnitService();
        }
        // GET: We7daRa2eeseya
        public ActionResult DataEntry(int pg)
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            We7daRa2eeseya unit = dBContext.We7daRa2eeseya
                .Include("Qa2edWe7da")
                .Include("Ra2ees3ameleyat")
                .Include("We7daFar3eya")
                .Include("We7daFar3eya.Qa2edWe7da")
                .Include("We7daFar3eya.Ra2ees3ameleyat")
                .FirstOrDefault(row => row.ID == userId);
            ViewBag.unit = unit;
            ViewBag.pg = pg;
            List<Rotba> Rotbas = new List<Rotba>()
            {
                new Rotba{ ID = 0 , RotbaName = "", RotbaType = 1}
            };
            Rotbas.AddRange(dBContext.Rotba.Where(row => row.RotbaType == ((pg == 1 || pg == 5 ? 1 : pg - 1))).ToList());
            
            switch (pg)
            {
                case 1:
                    break;
                case 2:
                case 3:
                case 4:
                    ViewBag.FardDetails = dBContext.FardDetails.Include("Rotba").Where(row => row.We7daID == userId && row.Rotba.RotbaType == pg - 1).OrderBy(row => row.Rotba.ID).ToList();
                    break;
                case 5:
                    ViewBag.suCount = unit.We7daFar3eya.Count;
                    break;
                default:
                    break;
            }
            ViewBag.Rotbas = Rotbas;
            return View(unit);
        }
        [HttpPost]
        public ActionResult AddUnitLeadership(We7daRa2eeseya unitTemp, int? SmallUnitCount)
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            We7daRa2eeseya unit = dBContext.We7daRa2eeseya
                .Include("Qa2edWe7da")
                .Include("Ra2ees3ameleyat")
                .FirstOrDefault(row => row.ID == userId);

            if (unit.Qa2edWe7daID == null)
            {
                unitTemp.Qa2edWe7da.We7daID = userId;
                unit.Qa2edWe7daID = FardDetailsService.Add(unitTemp.Qa2edWe7da);
            }
            else
            {
                FardDetailsService.Update(unit.Qa2edWe7daID, unitTemp.Qa2edWe7da);
            }
            if (unit.Ra2ees3ameleyatID == null)
            {
                unitTemp.Ra2ees3ameleyat.We7daID = userId;
                unit.Ra2ees3ameleyatID = FardDetailsService.Add(unitTemp.Ra2ees3ameleyat);
            }
            else
            {
                FardDetailsService.Update(unit.Ra2ees3ameleyatID, unitTemp.Ra2ees3ameleyat);
                unit.Ra2ees3ameleyat.RotbaID = unitTemp.Ra2ees3ameleyat.RotbaID;
                unit.Ra2ees3ameleyat.FullName = unitTemp.Ra2ees3ameleyat.FullName;
            }
            dBContext.SaveChanges();
            TempData["SmallUnitCount"] = SmallUnitCount;
            Response.Cookies.Add(new HttpCookie("SmallUnitCount") { Value = SmallUnitCount.ToString() });
            return RedirectToAction("DataEntry", new { pg = 5 });
        }

        [HttpPost]
        public ActionResult AddSmallUnit(We7daRa2eeseya unitTemp)
        {
            string[] smallUnitsName = new string[] { "ك 1", "ك 2", "ك 3" };
            int userId = int.Parse(Request.Cookies["userID"].Value);
            We7daRa2eeseya unit = dBContext.We7daRa2eeseya
                .Include("We7daFar3eya")
                .Include("We7daFar3eya.Qa2edWe7da")
                .Include("We7daFar3eya.Ra2ees3ameleyat")
                .FirstOrDefault(row => row.ID == userId);
            int i = 0;
            foreach (We7daFar3eya smallUnit in unitTemp.We7daFar3eya)
            {
                smallUnit.We7daName = smallUnitsName[i];
                smallUnit.We7daRa2eseyaID = userId;
                smallUnit.Qa2edWe7da.We7daID = userId;
                smallUnit.Ra2ees3ameleyat.We7daID = userId;

                if (i < unit.We7daFar3eya.Count)
                {
                    smallUnitService.Update(unit.We7daFar3eya[i].ID, smallUnit);
                }
                else
                {
                    unit.We7daFar3eya.Add(smallUnit);
                }
                i++;
                try
                {
                    dBContext.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }

            return RedirectToAction("DataEntry", new { pg = 2 });
        }

        [HttpGet]
        public JsonResult GetUnitsByZone(int Taba3eyaID)
        {
            List<We7daRa2eeseya> units = unitService.GetByZone(Taba3eyaID);
            return Json(units, JsonRequestBehavior.AllowGet);
        }
    }
}