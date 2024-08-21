using ElecWarSystem.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElecWarSystem.ReportFactory
{
    public class MaradyReport : ReportGenerator<Marady>
    {
        private Dictionary<String, Dictionary<String, List<Marady>>> MaradyReportData;
        public MaradyReport(Dictionary<String, Dictionary<String, List<Marady>>> MaradyReportData,
            DateTime tmamDate, string title) 
            : base(tmamDate, title, 19,4)
        {
            this.MaradyReportData = MaradyReportData;
        }

        protected override void CreateTableHead()
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 10f, Font.BOLD);
            baseColor = BaseColor.LIGHT_GRAY;
            this.CreateCell("م");
            this.CreateCell("الرتبة/الدرجة", 2);
            this.CreateCell("الإسم", 4);
            this.CreateCell("المستشفى", 2);
            this.CreateCell("تاريخ دخول المستشفى", 2);
            this.CreateCell("التشخيص",4);
            this.CreateCell("بدء الأجازة", 2);
            this.CreateCell("عودة الأجازة", 2);
        }

        protected override void CreateTableRow(int i, Marady Marady)
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 8f, Font.NORMAL);
            baseColor = BaseColor.WHITE;
            this.CreateCell(Utilites.numbersE2A(i.ToString()));
            this.CreateCell(Marady.MaradyDetails.FardDetails.Rotba.RotbaName, 2);
            this.CreateCell(Marady.MaradyDetails.FardDetails.FullName, 4);
            this.CreateCell(Utilites.numbersE2A(Marady.MaradyDetails.Mostashfa), 2);
            this.CreateCell(Utilites.numbersE2A(Marady.MaradyDetails.MostashfaDate.ToString("dd/MM/yyyy")), 2);
            this.CreateCell(Utilites.numbersE2A(Marady.MaradyDetails.Hala), 4);
            this.CreateCell(Utilites.numbersE2A(Marady.MaradyDetails.DateFrom.ToString("dd/MM/yyyy")), 2);
            this.CreateCell(Utilites.numbersE2A(Marady.MaradyDetails.DateTo.ToString("dd/MM/yyyy")), 2);
        }

        protected override void ReportBody()
        {
            this.CreateTableHead();
            int i = 1;
            foreach (var MaradysPerZone in MaradyReportData)
            {
                if (MaradysPerZone.Value.Count > 0)
                {
                    this.CreateTitleWithBackgroundColor($"وحدات فى نطاق {MaradysPerZone.Key}",
                                    fontSize: 12f,
                                    fontStyle: Font.BOLD);
                    foreach (var MaradyPerUnit in MaradysPerZone.Value)
                    {
                        if (MaradyPerUnit.Value.Count > 0)
                        {
                            this.CreateTitleWithBackgroundColor(Utilites.numbersE2A(MaradyPerUnit.Key),
                                    fontSize: 10f,
                                    fontStyle: Font.BOLD,
                                    align: Element.ALIGN_LEFT);
                            foreach (Marady Marady in MaradyPerUnit.Value)
                            {
                                this.CreateTableRow(i, Marady);
                                i++;
                            }
                        }
                    }
                }
            }
        }
    }
}