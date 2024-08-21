using ElecWarSystem.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElecWarSystem.ReportFactory
{
    public class GheyabsReport : ReportGenerator<Gheyab>
    {
        //
        private Dictionary<String, Dictionary<String, List<Gheyab>>> absenceReportData;
        public GheyabsReport(Dictionary<String, Dictionary<String, List<Gheyab>>> absenceReportData,
            DateTime tmamDate, string title)
            : base(tmamDate, title, 17,4)
        {
            this.absenceReportData = absenceReportData;
        }

        protected override void CreateTableHead()
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 10f, Font.BOLD);
            baseColor = BaseColor.LIGHT_GRAY;
            this.CreateCell("م");
            this.CreateCell("الرتبة/الدرجة", 2);
            this.CreateCell("الإسم", 4);
            this.CreateCell("تاريخ الغياب", 4);
            this.CreateCell("دفعة الغياب", 2);
            this.CreateCell("بند الأوامر", 4);
        }
        
        //i sent from the for function and the object we wanna write to Pdf
        protected override void CreateTableRow(int i, Gheyab Gheyab)
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 8f, Font.NORMAL);
            baseColor = BaseColor.WHITE;
            this.CreateCell($"{Utilites.numbersE2A(i.ToString())}");
            this.CreateCell(Gheyab.GheyabDetails.FardDetails.Rotba.RotbaName, 2);
            this.CreateCell(Gheyab.GheyabDetails.FardDetails.FullName, 4);
            this.CreateCell(Utilites.numbersE2A(Gheyab.GheyabDetails.DateFrom.ToString("dd/MM/yyyy")), 4);
            this.CreateCell(Utilites.numbersE2A(Gheyab.GheyabDetails.GheyabTimes.ToString()), 2);
            this.CreateCell(Utilites.numbersE2A(Gheyab.GheyabDetails.commandItem.Number.ToString()), 2);
            this.CreateCell(Utilites.numbersE2A(Gheyab.GheyabDetails.commandItem.Date.ToString("dd/MM/yyyy")), 2);
        }

        protected override void ReportBody()
        {
            this.CreateTableHead();
            int i = 1;
            foreach (var absencesPerZone in absenceReportData)
            {
                if (absencesPerZone.Value.Count > 0)
                {
                    this.CreateTitleWithBackgroundColor($"وحدات فى نطاق {absencesPerZone.Key}",
                                    fontSize: 12f,
                                    fontStyle: Font.BOLD);
                    foreach (var absencePerUnit in absencesPerZone.Value)
                    {
                        if (absencePerUnit.Value.Count > 0)
                        {
                            this.CreateTitleWithBackgroundColor(Utilites.numbersE2A(absencePerUnit.Key),
                                    fontSize: 10f,
                                    fontStyle: Font.BOLD,
                                    align: Element.ALIGN_LEFT);
                            foreach (Gheyab Gheyab in absencePerUnit.Value)
                            {
                                this.CreateTableRow(i, Gheyab);
                                i++;
                            }
                        }
                    }
                }
            }
        }
    }
}