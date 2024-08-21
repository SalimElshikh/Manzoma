using ElecWarSystem.Models;
using ElecWarSystem.Models.OutDoor;
using ElecWarSystem.Models.OutDoorDetails;
using ElecWarSystem.Serivces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ElecWarSystem.Controllers
{
    public class Fer2aController : Controller
    {
        private readonly IOutingRepository<Fer2a, Fer2aDetails> Fer2aService;
        private readonly TmamService tmamService;
        private readonly UserService userService;
        private readonly RotbaService RotbaService;
        private readonly FardService FardService;

        public Fer2aController()
        {
            tmamService = new TmamService();
            userService = new UserService();
            RotbaService = new RotbaService();
            FardService = new FardService();
            Fer2aService = new OutingService<Fer2a, Fer2aDetails>();
        }
        [HttpGet]
        public ActionResult Index()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);

            String unitName = userService.GetUnitName(userId);
            ViewBag.unitName = unitName;

            List<Rotba> Rotbas = RotbaService.GetAllRotbas();
            ViewBag.Rotbas = Rotbas;

            return View();
        }
        [HttpGet]
        public JsonResult GetFer2as()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            Tmam tmam = new Tmam() { We7daID = userId, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.AddTmam(tmam);

            List<Fer2a> Fer2as =
                Fer2aService.GetAll(
                    row => row.TmamID == tmamID,
                    new[] { "Fer2aDetails.FardDetails.Rotba",
                            "Fer2aDetails.CommandItem"}
                ).ToList();

            return Json(Fer2as, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public long Create(Fer2a Fer2a)
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            Tmam tmam = new Tmam() { We7daID = userId, Date = DateTime.Today.AddDays(1) };
            Fer2a.TmamID = tmamService.GetTmamID(tmam);
            Fer2a.Tmam = tmam;
            if (Fer2a.IsDateLogic())
            {
                FardService.setFard(new Fard
                {
                    FardID = Fer2a.Fer2aDetails.FardID,
                    TmamID = Fer2a.TmamID,
                    Status = TmamEnum.Fer2a
                });
                Fer2a.CleanNav();
                Fer2aService.Add(Fer2a);
                return 0;
            }
            else
            {
                return -1;
            }
        }
        [HttpPost]
        public void Delete(long id)
        {
            Fer2a Fer2a = Fer2aService.Get(row => row.ID == id, new[] { "Fer2aDetails.CommandItem" });
            long FardDetailsID = Fer2a.Fer2aDetails.FardID;
            FardService.DeleteFard(Fer2a.TmamID, FardDetailsID);
            Fer2aService.Delete(row => row.ID == Fer2a.ID);
        }
        [HttpGet]
        public JsonResult GetNumbers()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            int Total = tmamService.GetTmamDetails(userId, true).Fer2a +
                        tmamService.GetTmamDetails(userId, false).Fer2a;

            Tmam expectedTmam = new Tmam() { We7daID = userId, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(expectedTmam);

            int entered = Fer2aService.GetCount(row => row.TmamID == tmamID);

            Dictionary<String, int> numbers = new Dictionary<string, int>();
            numbers.Add("total", Total);
            numbers.Add("entered", entered);
            return Json(numbers, JsonRequestBehavior.AllowGet);
        }
    }
}