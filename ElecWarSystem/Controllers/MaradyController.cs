using ElecWarSystem.Models;
using ElecWarSystem.Serivces;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ElecWarSystem.Controllers
{
    public class MaradyController : Controller
    {
        private readonly MaradyService MaradyService;
        private readonly TmamService tmamService;
        private readonly UserService userService;
        private readonly RotbaService RotbaService;
        public MaradyController()
        {
            MaradyService = new MaradyService();
            tmamService = new TmamService();
            userService = new UserService();
            RotbaService = new RotbaService();
        }
        // GET: Marady
        [HttpGet]
        public ActionResult Index()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            List<Marady> MaradyList = MaradyService.GetAll(userId);
            ViewBag.Marady = MaradyList;
            String unitName = userService.GetUnitName(userId);
            ViewBag.unitName = unitName;
            List<Rotba> Rotbas = RotbaService.GetAllRotbas();
            ViewBag.Rotbas = Rotbas;
            ViewBag.MaradyTotal = MaradyService.getTotal(userId);
            ViewBag.MaradyEntered = MaradyService.getEntered(userId);
            Request.Cookies["tmam-title"].Value = ((int)TmamEnum.Marady).ToString();

            return View();
        }
        [HttpGet]
        public JsonResult GetMarady()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            List<Marady> MaradyList = MaradyService.GetAll(userId);
            return Json(MaradyList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public long Create(Marady Marady)
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            Marady.TmamID = tmamService.GetTmamID(new Tmam() { We7daID = userId, Date = DateTime.Today.AddDays(1) });
            return MaradyService.Add(Marady);
        }
        [HttpPost]
        public void Delete(long id)
        {
            MaradyService.Delete(id);
        }
    }
}