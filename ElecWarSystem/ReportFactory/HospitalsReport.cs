using ElecWarSystem.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElecWarSystem.ReportFactory
{
    public class MostashfasReport : ReportGenerator<Mostashfa>
    {
        private Dictionary<String, Dictionary<String, List<Mostashfa>>> MostashfaReportData;
        public MostashfasReport(Dictionary<String, Dictionary<String, List<Mostashfa>>> MostashfaReportData,
            DateTime tmamDate, string title)
            : base(tmamDate, title, 17,4)
        {
            this.MostashfaReportData = MostashfaReportData;
        }

        protected override void CreateTableHead()
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 10f, Font.BOLD);
            baseColor = BaseColor.LIGHT_GRAY;
            this.CreateCell("م");
            this.CreateCell("الرتبة/الدرجة", 2);
            this.CreateCell("الإسم", 4);
            this.CreateCell("إسم المستشفى", 2);
            this.CreateCell("تاريخ دخول المستشف", 2);
            this.CreateCell("التشخيص", 3);
            this.CreateCell("التوصيات الممنوحة", 3);
        }

        protected override void CreateTableRow(int i, Mostashfa Mostashfa)
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 8f, Font.NORMAL);
            baseColor = BaseColor.WHITE;
            this.CreateCell($"{Utilites.numbersE2A(i.ToString())}");
            this.CreateCell(Mostashfa.MostashfaDetails.FardDetails.Rotba.RotbaName, 2);
            this.CreateCell(Mostashfa.MostashfaDetails.FardDetails.FullName, 4);
            this.CreateCell(Utilites.numbersE2A(Mostashfa.MostashfaDetails.Mostashfa), 2);
            this.CreateCell(Utilites.numbersE2A(Mostashfa.MostashfaDetails.DateFrom.ToString("dd/MM/yyyy")), 2);
            this.CreateCell(Utilites.numbersE2A(Mostashfa.MostashfaDetails.Hala), 3);
            this.CreateCell(Utilites.numbersE2A(Mostashfa.MostashfaDetails.Tawseya), 3);
        }

        protected override void ReportBody()
        {
            this.CreateTableHead();
            int i = 1;
            foreach (var MostashfasPerZone in MostashfaReportData)
            {
                if (MostashfasPerZone.Value.Count > 0)
                {
                    this.CreateTitleWithBackgroundColor($"وحدات فى نطاق {MostashfasPerZone.Key}",
                                    fontSize: 12f,
                                    fontStyle: Font.BOLD);
                    foreach (var MostashfaPerUnit in MostashfasPerZone.Value)
                    {
                        if (MostashfaPerUnit.Value.Count > 0)
                        {
                            this.CreateTitleWithBackgroundColor(Utilites.numbersE2A(MostashfaPerUnit.Key),
                                    fontSize: 10f,
                                    fontStyle: Font.BOLD,
                                    align: Element.ALIGN_LEFT);
                            foreach (Mostashfa Mostashfa in MostashfaPerUnit.Value)
                            {
                                this.CreateTableRow(i, Mostashfa);
                                i++;
                            }
                        }
                    }
                }
            }
        }
    }
}