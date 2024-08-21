using ElecWarSystem.Models;
using ElecWarSystem.Serivces;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ElecWarSystem.Controllers
{
    public class MostashfaController : Controller
    {
        private readonly MostashfaService MostashfaService;
        private readonly TmamService tmamService;
        private readonly UserService userService;
        private readonly RotbaService RotbaService;
        public MostashfaController()
        {
            MostashfaService = new MostashfaService();
            tmamService = new TmamService();
            userService = new UserService();
            RotbaService = new RotbaService();
        }
        [HttpGet]
        public ActionResult Index()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            List<Mostashfa> MostashfaList = MostashfaService.GetAll(userId);
            ViewBag.Mostashfa = MostashfaList;
            String unitName = userService.GetUnitName(userId);
            ViewBag.unitName = unitName;
            List<Rotba> Rotbas = RotbaService.GetAllRotbas();
            ViewBag.Rotbas = Rotbas;
            ViewBag.MostashfaTotal = MostashfaService.getTotal(userId);
            ViewBag.MostashfaEntered = MostashfaService.getEntered(userId);
            Request.Cookies["tmam-title"].Value = ((int)TmamEnum.Mostashfa).ToString();

            return View();
        }
        [HttpGet]
        public JsonResult GetMostashfa()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            List<Mostashfa> MostashfaList = MostashfaService.GetAll(userId);
            return Json(MostashfaList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public long Create(Mostashfa Mostashfa)
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            
            Mostashfa.TmamID = tmamService.GetTmamID(new Tmam() { We7daID = userId, Date = DateTime.Today.AddDays(1) });
            Mostashfa.Tmam = new Tmam() { We7daID = userId, Date = DateTime.Today.AddDays(1) };
            return MostashfaService.Add(Mostashfa);
        }
        [HttpPost]
        public void Delete(long id)
        {
            MostashfaService.Delete(id);
        }
    }
}