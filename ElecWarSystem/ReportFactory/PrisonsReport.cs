using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElecWarSystem.Models;

namespace ElecWarSystem.ReportFactory
{
    public class SegnsReport : ReportGenerator<Segn>
    {
        private Dictionary<String, Dictionary<String, List<Segn>>> SegnReportData;
        public SegnsReport(Dictionary<String, Dictionary<String, List<Segn>>> SegnReportData,
            DateTime tmamDate, string title)
            : base(tmamDate, title, 23,4)
        {
            this.SegnReportData = SegnReportData;
        }

        protected override void CreateTableHead()
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 10f, Font.BOLD);
            baseColor = BaseColor.LIGHT_GRAY;
            this.CreateCell("م");
            this.CreateCell("الرتبة/الدرجة", 2);
            this.CreateCell("الإسم", 4);
            this.CreateCell("الجريمة", 3);
            this.CreateCell("العقوبة", 3);
            this.CreateCell("الآمر بالعقوبة", 2);
            this.CreateCell("المدة من", 2);
            this.CreateCell("المدة إلى", 2);
            this.CreateCell("بند الأوامر", 4);
        }

        protected override void CreateTableRow(int i, Segn Segn)
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 8f, Font.NORMAL);
            baseColor = BaseColor.WHITE;
            this.CreateCell(Utilites.numbersE2A(i.ToString()));
            this.CreateCell(Segn.SegnDetails.FardDetails.Rotba.RotbaName, 2);
            this.CreateCell(Segn.SegnDetails.FardDetails.FullName, 4);
            this.CreateCell(Utilites.numbersE2A(Segn.SegnDetails.Gareema), 3);
            this.CreateCell(Utilites.numbersE2A(Segn.SegnDetails.Eqab), 3);
            this.CreateCell(Utilites.numbersE2A(Segn.SegnDetails.Mo3aqeb), 2);
            this.CreateCell(Utilites.numbersE2A(Segn.SegnDetails.DateFrom.ToString("dd/MM/yyyy")), 2);
            this.CreateCell(Utilites.numbersE2A(Segn.SegnDetails.DateTo.ToString("dd/MM/yyyy")), 2);
            this.CreateCell(Utilites.numbersE2A(Segn.SegnDetails.CommandItem.Number.ToString()), 2);
            this.CreateCell(Utilites.numbersE2A(Segn.SegnDetails.CommandItem.Date.ToString("dd/MM/yyyy")), 2);
        }

        protected override void ReportBody()
        {
            this.CreateTableHead();
            int i = 1;
            foreach (var SegnsPerZone in SegnReportData)
            {
                if (SegnsPerZone.Value.Count > 0)
                {
                    this.CreateTitleWithBackgroundColor($"وحدات فى نطاق {SegnsPerZone.Key}",
                                    fontSize: 12f,
                                    fontStyle: Font.BOLD);
                    foreach (var SegnPerUnit in SegnsPerZone.Value)
                    {
                        if (SegnPerUnit.Value.Count > 0)
                        {
                            this.CreateTitleWithBackgroundColor(Utilites.numbersE2A(SegnPerUnit.Key),
                                    fontSize: 10f,
                                    fontStyle: Font.BOLD,
                                    align: Element.ALIGN_LEFT);
                            foreach (Segn Segn in SegnPerUnit.Value)
                            {
                                this.CreateTableRow(i, Segn);
                                i++;
                            }
                        }
                    }
                }
            }
        }
    }
}