using ElecWarSystem.Models;
using ElecWarSystem.ReportFactory;
using ElecWarSystem.Serivces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElecWarSystem.Controllers
{
    public class TmamGatheringReportsController : Controller
    {
        private TmamGatheringService tmamGatheringService;
        private DateTime dateTmam;
        private string title;
        public TmamGatheringReportsController()
        {
            tmamGatheringService = new TmamGatheringService();
            dateTmam = DateTime.Today.AddDays(1);
        }
        public void initViewer()
        {
            UserRoles userRoles = (UserRoles)byte.Parse(Request.Cookies["Roles"].Value);

            if ((userRoles & UserRoles.Viewer) == UserRoles.Viewer &&
                    (userRoles & UserRoles.Admin) != UserRoles.Admin)
            {
                this.dateTmam = DateTime.Today;
                tmamGatheringService = new TmamGatheringService(this.dateTmam);
            }
        }
        public ActionResult LeadersTmamReport()
        {
            initViewer();
            var MaradysTmam = tmamGatheringService.GetMaradysTmam();
            this.title = "القادة";
            LeadersTmamReport leadersTmamReport = new LeadersTmamReport(tmamGatheringService.GetAllLeaderTmam(), dateTmam, this.title);
            leadersTmamReport.SetAltCommandors(tmamGatheringService.GetAllAltCommandor());
            byte[] bytes = leadersTmamReport.PrepareReport();
            return File(bytes, "application/pdf", $"{this.title}.pdf");
        }
        // GET: TmamGatheringReports
        public ActionResult OfficerTmamReport()
        {
            initViewer();
            Dictionary<String, List<TmamDetails>> officersTmamList = tmamGatheringService.GetOfficersTmam(IsOfficers: true);
            this.title = "الضباط";
            FardDetailssTmamReport FardDetailssTmamReport = new FardDetailssTmamReport(officersTmamList, this.dateTmam, this.title);
            byte[] bytes = FardDetailssTmamReport.PrepareReport();
            return File(bytes, "application/pdf", $"تمام {this.title}.pdf");
        }
        public ActionResult NonOfficerTmamReport()
        {
            initViewer();
            Dictionary<String, List<TmamDetails>> officersTmamList = tmamGatheringService.GetOfficersTmam(IsOfficers: false);
            this.title = "الدرجات الأخرى";
            FardDetailssTmamReport FardDetailssTmamReport = new FardDetailssTmamReport(officersTmamList, this.dateTmam, this.title);
            byte[] bytes = FardDetailssTmamReport.PrepareReport();
            return File(bytes, "application/pdf", $"تمام {this.title}.pdf");
        }
        public ActionResult Ma2moreyasReport()
        {
            initViewer();
            var Ma2moreyasTmam = tmamGatheringService.GetMa2moreyasTmam();
            this.title = "المأموريات";
            Ma2moreyasReport Ma2moreyaReport = new Ma2moreyasReport(Ma2moreyasTmam, this.dateTmam, this.title);
            byte[] bytes = Ma2moreyaReport.PrepareReport();

            return File(bytes, "application/pdf", $"{this.title}.pdf");
        }
        public ActionResult MaradysReport()
        {
            initViewer();
            var MaradysTmam = tmamGatheringService.GetMaradysTmam();
            this.title = "الأجازات المرضية";
            MaradyReport MaradyReport = new MaradyReport(MaradysTmam, this.dateTmam, this.title);
            byte[] bytes = MaradyReport.PrepareReport();

            return File(bytes, "application/pdf", $"{this.title}.pdf");
        }
        public ActionResult MostashfasReport()
        {
            initViewer();
            var MostashfasTmam = tmamGatheringService.GetMostashfasTmam();
            this.title = "المستشفيات";
            MostashfasReport MostashfaReport = new MostashfasReport(MostashfasTmam, this.dateTmam, $"الضباط  و الأفراد المحجوزين ب{this.title}");
            byte[] bytes = MostashfaReport.PrepareReport();

            return File(bytes, "application/pdf", $"{this.title}.pdf");
        }

        public ActionResult SegnsReport()
        {
            initViewer();
            var SegnsTmam = tmamGatheringService.GetSegnsTmam();
            this.title = "السجن";
            SegnsReport SegnReport = new SegnsReport(SegnsTmam, this.dateTmam, "الأفراد(السجن/الحبس)");
            byte[] bytes = SegnReport.PrepareReport();
            return File(bytes, "application/pdf", $"{this.title}.pdf");
        }
        public ActionResult GheyabsReport()
        {
            initViewer();
            var absencesTmam = tmamGatheringService.GetGheyabsTmam();
            this.title = "الغياب";
            GheyabsReport absenceReport = new GheyabsReport(absencesTmam, this.dateTmam, $"الأفراد {this.title}");
            byte[] bytes = absenceReport.PrepareReport();
            return File(bytes, "application/pdf", $"{this.title}.pdf");
        }
        public ActionResult OutOfCountriesReport()
        {
            initViewer();
            var OutOfCountriesTmam = tmamGatheringService.GetOutOfCountriesTmam();
            this.title = "خارج البلاد";
            OutOfCountriesReport outOfCountriesReport = new OutOfCountriesReport(OutOfCountriesTmam, this.dateTmam, $"الضباط و الأفراد المسافرين {this.title}");
            byte[] bytes = outOfCountriesReport.PrepareReport();
            return File(bytes, "application/pdf", $"{this.title}.pdf");
        }
        public ActionResult Mo3askrsReport()
        {
            initViewer();
            var Mo3askrsTmam = tmamGatheringService.GetMo3askrsTmam();
            this.title = "خارج التمركز";
            Mo3askrsReport Mo3askrsReport = new Mo3askrsReport(Mo3askrsTmam, this.dateTmam, $"الضباط و الأفراد المتواجدين {this.title}");
            byte[] bytes = Mo3askrsReport.PrepareReport();
            return File(bytes, "application/pdf", $"{this.title}.pdf");
        }
        public ActionResult Fer2asReport()
        {
            initViewer();
            var Fer2asTmam = tmamGatheringService.GetFer2asTmam();
            this.title = "الفرق و الدورات";
            Fer2asReport Fer2asReport = new Fer2asReport(Fer2asTmam, this.dateTmam, this.title);
            byte[] bytes = Fer2asReport.PrepareReport();
            return File(bytes, "application/pdf", $"{this.title}.pdf");
        }

    }
}