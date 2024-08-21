using ElecWarSystem.Models;
using ElecWarSystem.Serivces;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace ElecWarSystem.Controllers
{
    public class SegnController : Controller
    {
        private readonly RotbaService RotbaService;
        private readonly SegnService SegnService;
        private readonly TmamService tmamService;
        private readonly UserService userService;
        public SegnController()
        {
            RotbaService = new RotbaService();
            SegnService = new SegnService();
            tmamService = new TmamService();
            userService = new UserService();
        }
        [HttpGet]
        public ActionResult Index()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            ViewBag.SegnsList = SegnService.GetAll(userId);
            ViewBag.Rotbas = RotbaService.GetAllRotbas();
            ViewBag.unitName = userService.GetUnitName(userId);
            ViewBag.TotalSegns = SegnService.getTotal(userId);
            ViewBag.EnteredSegns = SegnService.getEntered(userId);
            Request.Cookies["tmam-title"].Value = ((int)TmamEnum.Segn).ToString();

            return View();
        }
        [HttpGet]
        public JsonResult GetSegns()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            List<Segn> Segn = SegnService.GetAll(userId);
            return Json(Segn, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public long Create(Segn Segn)
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            Segn.TmamID = tmamService.GetTmamID(new Tmam() { We7daID = userId, Date = DateTime.Today.AddDays(1) });
            return SegnService.Add(Segn);
        }
        [HttpPost]
        public void Delete(long id)
        {
            SegnService.Delete(id);
        }
    }
}