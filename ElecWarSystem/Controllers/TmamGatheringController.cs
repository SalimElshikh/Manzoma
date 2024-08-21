using ElecWarSystem.Models;
using ElecWarSystem.Serivces;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace ElecWarSystem.Controllers
{
    public class TmamGatheringController : Controller
    {
        private TmamService tmamService;
        private TmamGatheringService tmamGatheringService;
        public TmamGatheringController()
        {
            tmamService = new TmamService();
            tmamGatheringService = new TmamGatheringService();
        }
        public void initViewer()
        {
            UserRoles userRoles = (UserRoles)byte.Parse(Request.Cookies["Roles"].Value);

            if ((userRoles & UserRoles.Viewer) == UserRoles.Viewer &&
                    (userRoles & UserRoles.Admin) != UserRoles.Admin)
            {
                DateTime dateTime = DateTime.Today;
                tmamGatheringService = new TmamGatheringService(dateTime);
                tmamService = new TmamService(dateTime);
            }
        }
        [HttpGet]
        public ActionResult LeaderShip()
        {
            UserRoles userRoles = (UserRoles)byte.Parse(Request.Cookies["Roles"].Value);
            
            initViewer();
            
            if ((userRoles & UserRoles.Viewer) == UserRoles.Viewer ||
                (userRoles & UserRoles.Admin) == UserRoles.Admin)
            {
                ViewBag.leadersTmams = tmamGatheringService.GetAllLeaderTmam();
                ViewBag.Ra2ees3ameleyatID = tmamGatheringService.GetAllAltCommandor();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        [HttpGet]
        public ActionResult RecievedTmam()
        {
            ViewBag.SubmittedTmams = tmamGatheringService.GetTmamsSubmitted();
            return View();
        }
        [HttpGet]
        public ActionResult Officers()
        {
            initViewer();
            ViewBag.ZoneUnitsTmam = tmamGatheringService.GetOfficersTmam(IsOfficers: true);
            return View();
        }
        [HttpGet]
        public ActionResult NonOfficers()
        {
            initViewer();
            ViewBag.ZoneUnitsTmam = tmamGatheringService.GetOfficersTmam(IsOfficers: false);
            return View();
        }
        [HttpGet]
        public ActionResult Marady()
        {
            initViewer();
            ViewBag.Marady = tmamGatheringService.GetMaradysTmam();
            return View();
        }

        
        [HttpGet]
        public ActionResult Ma2moreya()
        {
            initViewer();

            ViewBag.Ma2moreya = tmamGatheringService.GetMa2moreyasTmam();
            return View();
        }
        [HttpGet]
        public ActionResult Segn()
        {
            initViewer();
            ViewBag.Segn = tmamGatheringService.GetSegnsTmam();
            return View();
        }
        [HttpGet]
        public ActionResult Gheyab()
        {
            initViewer();
            ViewBag.Gheyab = tmamGatheringService.GetGheyabsTmam();
            return View();
        }
        [HttpGet]
        public ActionResult Mostashfa()
        {
            initViewer();
            ViewBag.Mostashfa = tmamGatheringService.GetMostashfasTmam();
            return View();
        }
        public ActionResult KharegBelad()
        {
            initViewer();
            ViewBag.KharegBelad = tmamGatheringService.GetOutOfCountriesTmam();
            return View();
        }
        public ActionResult Mo3askr()
        {
            initViewer();
            ViewBag.Mo3askr = tmamGatheringService.GetMo3askrsTmam();
            return View();
        }
        public ActionResult Fer2a()
        {
            initViewer();
            ViewBag.Fer2a = tmamGatheringService.GetFer2asTmam();
            return View();
        }
        [HttpGet]
        public ActionResult TmamDetails(int id)
        {
            ViewBag.tmam = tmamService.GetTmamWithAllDetails(id);
            ViewBag.LeaderTmam = tmamService.GetLeaderTmamPerUnit(id);
            return View();
        }
        [HttpPost]
        public void MakeTmamRecive(int unitID)
        {
            tmamService.ReciveTmam(unitID);
        }
        [HttpPost]
        public void MakeTmamReturn(int unitID)
        {
            tmamService.ReturnTmam(unitID);
        }
    }
}