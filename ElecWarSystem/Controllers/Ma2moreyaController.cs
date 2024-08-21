using ElecWarSystem.Models;
using ElecWarSystem.Serivces;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ElecWarSystem.Controllers
{
    public class Ma2moreyaController : Controller
    {
        private readonly UserService userService;
        private readonly RotbaService RotbaService;
        private readonly Ma2moreyasService Ma2moreyasService;
        private readonly TmamService tmamService;
        public Ma2moreyaController()
        {
            userService = new UserService();
            RotbaService = new RotbaService();
            Ma2moreyasService = new Ma2moreyasService();
            tmamService = new TmamService();
        }
        // GET: Ma2moreya
        public ActionResult Index()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            ViewBag.Rotbas = RotbaService.GetAllRotbas();
            ViewBag.unitName = userService.GetUnitName(userId);
            ViewBag.Ma2moreyasList = Ma2moreyasService.GetAll(userId);
            ViewBag.TotalMa2moreyas = Ma2moreyasService.getTotal(userId);
            ViewBag.EnteredMa2moreyas = Ma2moreyasService.getEntered(userId);
            Request.Cookies["tmam-title"].Value = ((int)TmamEnum.Ma2moreya).ToString();
            return View();
        }
        public JsonResult GetMa2moreyas()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            List<Ma2moreya> Ma2moreyas = Ma2moreyasService.GetAll(userId);
            return Json(Ma2moreyas, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public long Create(Ma2moreya Ma2moreya)
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            Ma2moreya.TmamID = tmamService.GetTmamID(new Tmam() { We7daID = userId, Date = DateTime.Today.AddDays(1) });
            return Ma2moreyasService.Add(Ma2moreya);
        }
        [HttpPost]
        public void Delete(long Ma2moreyaID)
        {
            Ma2moreyasService.Delete(Ma2moreyaID);
        }
    }
}