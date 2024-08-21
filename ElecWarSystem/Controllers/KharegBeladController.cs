using ElecWarSystem.Models;
using ElecWarSystem.Serivces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElecWarSystem.Controllers
{
    public class KharegBeladController : Controller
    {
        private readonly KharegBeladService KharegBeladService;
        private readonly TmamService tmamService;
        private readonly UserService userService;
        private readonly RotbaService RotbaService;
        public KharegBeladController()
        {
            tmamService = new TmamService();
            userService = new UserService();
            RotbaService = new RotbaService();
            KharegBeladService= new KharegBeladService();
        }
        // GET: KharegBelad
        [HttpGet]
        public ActionResult Index()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            List<KharegBelad> outOfCountries = KharegBeladService.GetAll(userId);
            ViewBag.KharegBelad = outOfCountries;
            String unitName = userService.GetUnitName(userId);
            ViewBag.unitName = unitName;
            List<Rotba> Rotbas = RotbaService.GetAllRotbas();
            ViewBag.Rotbas = Rotbas;
            ViewBag.KharegBeladTotal = KharegBeladService.getTotal(userId);
            ViewBag.KharegBeladEntered = KharegBeladService.getEntered(userId);
            Request.Cookies["tmam-title"].Value = ((int)TmamEnum.KharegBelad).ToString();
            return View();
        }
        [HttpGet]
        public JsonResult GetOutOfCountries()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            List<KharegBelad> outOfCountries = KharegBeladService.GetAll(userId);
            return Json(outOfCountries, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public long Create(KharegBelad KharegBelad)
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            KharegBelad.TmamID = tmamService.GetTmamID(new Tmam() { We7daID = userId, Date = DateTime.Today.AddDays(1) });
            return KharegBeladService.Add(KharegBelad);
        }
        [HttpPost]
        public void Delete(long id)
        {
            KharegBeladService.Delete(id);
        }
    }
}