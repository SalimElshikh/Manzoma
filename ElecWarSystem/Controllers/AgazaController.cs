using ElecWarSystem.Models;
using ElecWarSystem.Serivces;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ElecWarSystem.Controllers
{
    public class AgazaController : Controller
    {
        private readonly UserService userService;
        private readonly RotbaService RotbaService;
        private readonly AgazaService AgazaService;
        private readonly TmamService tmamService;
        public AgazaController()
        {
            userService = new UserService();
            RotbaService = new RotbaService();
            AgazaService = new AgazaService();
            tmamService = new TmamService();
        }
        // GET: Agaza
        public ActionResult Index()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            ViewBag.Rotbas = RotbaService.GetAllRotbas();
            ViewBag.unitName = userService.GetUnitName(userId);
            ViewBag.Agaza = AgazaService.GetAll(userId);
            ViewBag.AgazaTotal = AgazaService.getTotal(userId);
            ViewBag.AgazaEntered = AgazaService.getEntered(userId);
            Request.Cookies["tmam-title"].Value = ((int)TmamEnum.Agaza).ToString();

            return View();
        }
        public JsonResult GetAgaza()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            List<Agaza> Agaza = AgazaService.GetAll(userId);
            return Json(Agaza, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public long Create(Agaza Agaza)
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            Agaza.TmamID = tmamService.GetTmamID(new Tmam() { We7daID = userId, Date = DateTime.Today.AddDays(1) });
            long ID = AgazaService.Add(Agaza);
            return ID;
        }
        [HttpPost]
        public void Delete(long AgazaID)
        {
            AgazaService.Delete(AgazaID);
        }

    }
}