using ElecWarSystem.Models;
using ElecWarSystem.ReportFactory;
using ElecWarSystem.Serivces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;

namespace ElecWarSystem.Controllers
{
    public class TmamController : Controller
    {
        private readonly TmamService tmamService;
        private readonly RotbaService RotbaService;
        private readonly UserService userService;
        private readonly FardDetailsService FardDetailsService;
        public TmamController()
        {
            tmamService = new TmamService();
            RotbaService = new RotbaService();
            userService = new UserService();
            FardDetailsService = new FardDetailsService();
        }
        // GET: Tmam
        [HttpGet]
        public ActionResult Index(int pg)
        {
            ViewBag.pg = pg;
            int userId = int.Parse(Request.Cookies["userID"].Value);
            ViewBag.unitName = userService.GetUnitName(userId);
            bool isOfficers = true;
            switch (pg)
            {
                case 1:
                    isOfficers = true;
                    Request.Cookies["tmam-title"].Value = ((int)TmamEnum.Officers).ToString();
                    FardDetails altCommandor = tmamService.GetAltCommandorToday(userId);
                    ViewBag.AltCommandor = altCommandor;
                    ViewBag.AltcommandorFardDetailss = FardDetailsService.GetFardDetailsOfRotba(userId, (int)altCommandor.RotbaID);
                    break;
                case 2:
                    isOfficers = false;
                    Request.Cookies["tmam-title"].Value = ((int)TmamEnum.OtherRotbas).ToString();
                    break;
                default:
                    break;
            }
            
            ViewBag.TmamDetails = tmamService.GetTmamDetails(userId, isOfficers);
            ViewBag.Rotbas = RotbaService.GetRotbasOf(1);
            return View();
        }
        [HttpPost]
        public void AddTmamDetail(TmamDetails tmamDetail)
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            tmamDetail.Tmam.Date = DateTime.Today.AddDays(1);
            tmamDetail.Tmam.We7daID = userId;
            tmamService.AddTmamDetail(tmamDetail);
        }
        public ActionResult Review()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            ViewBag.unitName = userService.GetUnitName(userId);
            ViewBag.tmam = tmamService.GetTmamWithAllDetails(userId);
            ViewBag.LeaderTmam = tmamService.GetLeaderTmamPerUnit(userId);
            
            Tmam tmam = tmamService.GetTmam(userId);

            Tmam tmm = tmamService.GetTmam(userId);
            bool isSubmited = false;
            if (tmam != null)
            {
                isSubmited = tmm.Submitted;
            }
            Response.Cookies.Add(new HttpCookie("tmamSubmitStatus") { Value = isSubmited.ToString() });
            
            Response.Cookies.Add(new HttpCookie("tmam-title") { Value = ((int)TmamEnum.AllTmam).ToString() });
            return View();
        }
        public ActionResult ReviewReport()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            var tmam = tmamService.GetTmamWithAllDetails(userId);
            var LeaderTmam = tmamService.GetLeaderTmamPerUnit(userId);
            string title = Utilites.numbersA2E($"تمام {tmam.We7daRa2eeseya.We7daName}");
            ReviewReport reviewReport = new ReviewReport(tmam, LeaderTmam, tmam.Date, title);
            byte[] bytes = reviewReport.PrepareReport();
            return File(bytes, "application/pdf", $"{title}.pdf");
        }
        [HttpPost]
        public String SubmiitTmam()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            return tmamService.SubmitTmam(userId);
        }
        [HttpGet]
        public JsonResult GetTmamStatus()
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            Dictionary<String, bool> tmamStatus = tmamService.GetTmamStatus(userId);
            return Json(tmamStatus, JsonRequestBehavior.AllowGet);
        }
    }
}