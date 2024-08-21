using ElecWarSystem.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElecWarSystem.ReportFactory
{
    public class Ma2moreyasReport : ReportGenerator<Ma2moreya>
    {
        private Dictionary<String, Dictionary<String, List<Ma2moreya>>> Ma2moreyaReportData;
        public Ma2moreyasReport(Dictionary<String, Dictionary<String, List<Ma2moreya>>> Ma2moreyaReportData,
            DateTime tmamDate, string title)
            : base(tmamDate, title, 17,4)
        {
            this.Ma2moreyaReportData = Ma2moreyaReportData;
        }

        protected override void CreateTableHead()
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 10f, Font.BOLD);
            baseColor = BaseColor.LIGHT_GRAY;
            this.CreateCell("م");
            this.CreateCell("الرتبة", 2);
            this.CreateCell("الإسم", 4);
            this.CreateCell("جهة المأمورية", 3);
            this.CreateCell("الآمر بالمأمورية", 3);
            this.CreateCell("الفترة من", 2);
            this.CreateCell("الفترة إلى", 2);
        }

        protected override void CreateTableRow(int i, Ma2moreya Ma2moreya)
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 8f, Font.NORMAL);
            baseColor = BaseColor.WHITE;
            this.CreateCell($"{Utilites.numbersE2A(i.ToString())}");
            this.CreateCell(Ma2moreya.Ma2moreyaDetails.FardDetails.Rotba.RotbaName, 2);
            this.CreateCell(Ma2moreya.Ma2moreyaDetails.FardDetails.FullName, 4);
            this.CreateCell(Utilites.numbersE2A(Ma2moreya.Ma2moreyaDetails.Ma2moreyaPlace), 3);
            this.CreateCell(Utilites.numbersE2A(Ma2moreya.Ma2moreyaDetails.Ma2moreyaCommandor), 3);
            this.CreateCell(Utilites.numbersE2A(Ma2moreya.Ma2moreyaDetails.DateFrom.ToString("dd/MM/yyyy")), 2);
            this.CreateCell(Utilites.numbersE2A(Ma2moreya.Ma2moreyaDetails.DateTo.ToString("dd/MM/yyyy")), 2);
        }

        protected override void ReportBody()
        {
            this.CreateTableHead();
            int i = 1;
            foreach (var Ma2moreyasPerZone in Ma2moreyaReportData)
            {
                if (Ma2moreyasPerZone.Value.Count > 0)
                {
                    this.CreateTitleWithBackgroundColor($"وحدات فى نطاق {Ma2moreyasPerZone.Key}",
                                    fontSize: 12f,
                                    fontStyle: Font.BOLD);
                    foreach (var Ma2moreyaPerUnit in Ma2moreyasPerZone.Value)
                    {
                        if (Ma2moreyaPerUnit.Value.Count > 0)
                        {
                            this.CreateTitleWithBackgroundColor(Utilites.numbersE2A(Ma2moreyaPerUnit.Key),
                                    fontSize: 10f,
                                    fontStyle: Font.BOLD,
                                    align: Element.ALIGN_LEFT);
                            foreach (Ma2moreya Ma2moreya in Ma2moreyaPerUnit.Value)
                            {
                                this.CreateTableRow(i, Ma2moreya);
                                i++;
                            }
                        }
                    }
                }
            }
        }
    }
}