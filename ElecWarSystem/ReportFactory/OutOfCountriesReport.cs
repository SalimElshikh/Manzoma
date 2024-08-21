using ElecWarSystem.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElecWarSystem.ReportFactory
{
    public class OutOfCountriesReport : ReportGenerator<KharegBelad>
    {
        private Dictionary<String, Dictionary<String, List<KharegBelad>>> outOfCountriesList;
        public OutOfCountriesReport(Dictionary<String, Dictionary<String, List<KharegBelad>>> outOfCountriesList,
            DateTime tmamDate, string title)
            : base(tmamDate, title, 29,4)
        {
            this.outOfCountriesList = outOfCountriesList;
        }

        protected override void CreateTableHead()
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 10f, Font.BOLD);
            baseColor = BaseColor.LIGHT_GRAY;
            this.CreateCell("م");
            this.CreateCell("الرتبة/الدرجة", 4);
            this.CreateCell("الإسم", 4);
            this.CreateCell("جهة السفر", 4);
            this.CreateCell("الغرض من السفر", 8);
            this.CreateCell("المدة من", 4);
            this.CreateCell("المدة إلى", 4);
        }

        protected override void CreateTableRow(int i, KharegBelad KharegBelad)
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 8f, Font.NORMAL);
            baseColor = BaseColor.WHITE;
            this.CreateCell($"{Utilites.numbersE2A(i.ToString())}");
            this.CreateCell(KharegBelad.KharegBeladDetails.FardDetails.Rotba.RotbaName, 4);
            this.CreateCell(KharegBelad.KharegBeladDetails.FardDetails.FullName, 4);
            this.CreateCell(Utilites.numbersE2A(KharegBelad.KharegBeladDetails.Balad), 4);
            this.CreateCell(Utilites.numbersE2A(KharegBelad.KharegBeladDetails.Sabab), 8);
            this.CreateCell(Utilites.numbersE2A(KharegBelad.KharegBeladDetails.DateFrom.ToString("dd/MM/yyyy")), 4);
            this.CreateCell(Utilites.numbersE2A(KharegBelad.KharegBeladDetails.DateTo.ToString("dd/MM/yyyy")), 4);

        }

        protected override void ReportBody()
        {
            this.CreateTableHead();
            int i = 1;
            foreach (var KharegBeladsPerZone in outOfCountriesList)
            {
                if (KharegBeladsPerZone.Value.Count > 0)
                {
                    this.CreateTitleWithBackgroundColor($"وحدات فى نطاق {KharegBeladsPerZone.Key}",
                                    fontSize: 12f,
                                    fontStyle: Font.BOLD);
                    foreach (var KharegBeladPerUnit in KharegBeladsPerZone.Value)
                    {
                        if (KharegBeladPerUnit.Value.Count > 0)
                        {
                            this.CreateTitleWithBackgroundColor(Utilites.numbersE2A(KharegBeladPerUnit.Key),
                                    fontSize: 10f,
                                    fontStyle: Font.BOLD,
                                    align: Element.ALIGN_LEFT);
                            foreach (KharegBelad KharegBelad in KharegBeladPerUnit.Value)
                            {
                                this.CreateTableRow(i, KharegBelad);
                                i++;
                            }
                        }
                    }
                }
            }
        }
    }
}