using ElecWarSystem.Models;
using ElecWarSystem.Serivces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElecWarSystem.Controllers
{
    public class Mo3askrController : Controller
    {
        private readonly Mo3askrService Mo3askrService;
        private readonly TmamService tmamService;
        private readonly UserService userService;
        private readonly RotbaService RotbaService;
        public Mo3askrController()
        {
            tmamService = new TmamService();
            userService = new UserService();
            RotbaService = new RotbaService();
            Mo3askrService = new Mo3askrService();
        }
        [HttpGet]
        public ActionResult Index()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            List<Mo3askr> Mo3askr = Mo3askrService.GetAll(userId);
            ViewBag.Mo3askr = Mo3askr;
            String unitName = userService.GetUnitName(userId);
            ViewBag.unitName = unitName;
            List<Rotba> Rotbas = RotbaService.GetAllRotbas();
            ViewBag.Rotbas = Rotbas;
            ViewBag.Mo3askrTotal = Mo3askrService.getTotal(userId);
            ViewBag.Mo3askrEntered = Mo3askrService.getEntered(userId);
            Request.Cookies["tmam-title"].Value = ((int)TmamEnum.Mo3askr).ToString();
            return View();
        }
        [HttpGet]
        public JsonResult GetMo3askrs()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            List<Mo3askr> Mo3askr = Mo3askrService.GetAll(userId);
            return Json(Mo3askr, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public long Create(Mo3askr Mo3askr)
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            Mo3askr.TmamID = tmamService.GetTmamID(new Tmam() { We7daID = userId, Date = DateTime.Today.AddDays(1) });
            return Mo3askrService.Add(Mo3askr);
        }
        [HttpPost]
        public void Delete(long id)
        {
            Mo3askrService.Delete(id);
        }
    }
}