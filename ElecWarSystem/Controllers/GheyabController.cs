using ElecWarSystem.Models;
using ElecWarSystem.Serivces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;

namespace ElecWarSystem.Controllers
{
    public class GheyabController : Controller
    {
        private readonly UserService userService;
        private readonly RotbaService RotbaService;
        private readonly GheyabService GheyabsService;
        private readonly TmamService tmamService;
        public GheyabController()
        {
            userService = new UserService();
            RotbaService = new RotbaService();
            GheyabsService = new GheyabService();
            tmamService = new TmamService();
        }
        public ActionResult Index()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            ViewBag.Rotbas = RotbaService.GetAllRotbas();
            ViewBag.unitName = userService.GetUnitName(userId);
            ViewBag.GheyabsList = GheyabsService.GetAll(userId);
            ViewBag.TotalGheyabs = GheyabsService.getTotal(userId);
            ViewBag.EnteredGheyabs = GheyabsService.getEntered(userId);
            Request.Cookies["tmam-title"].Value = ((int)TmamEnum.Gheyab).ToString();
            
            return View();
        }
        public JsonResult GetGheyabs()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            List<Gheyab> Gheyab = GheyabsService.GetAll(userId);
            return Json(Gheyab, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public long Create(Gheyab Gheyab)
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            Gheyab.TmamID = tmamService.GetTmamID(new Tmam() { We7daID = userId, Date = DateTime.Today.AddDays(1) });
            return GheyabsService.Add(Gheyab);
        }
        [HttpPost]
        public void Delete(long GheyabID)
        {
            GheyabsService.Delete(GheyabID);
        }
    }
}