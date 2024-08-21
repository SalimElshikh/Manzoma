using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElecWarSystem.Models;

namespace ElecWarSystem.ReportFactory
{
    public class Mo3askrsReport : ReportGenerator<Mo3askr>
    {
        private Dictionary<String, Dictionary<String, List<Mo3askr>>> Mo3askrsList;
        public Mo3askrsReport(Dictionary<String, Dictionary<String, List<Mo3askr>>> Mo3askrsList,
            DateTime tmamDate, string title)
            : base(tmamDate, title, 33,4)
        {
            this.Mo3askrsList = Mo3askrsList;
        }

        protected override void CreateTableHead()
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 10f, Font.BOLD);
            baseColor = BaseColor.LIGHT_GRAY;
            this.CreateCell("م");
            this.CreateCell("الرتبة/الدرجة", 4);
            this.CreateCell("الإسم", 4);
            this.CreateCell("مكان التمركز الحالى", 8);
            this.CreateCell("السبب", 8);
            this.CreateCell("المدة من", 4);
            this.CreateCell("المدة إلى", 4);
        }

        protected override void CreateTableRow(int i, Mo3askr Mo3askr)
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 8f, Font.NORMAL);
            baseColor = BaseColor.WHITE;
            this.CreateCell($"{Utilites.numbersE2A(i.ToString())}");
            this.CreateCell(Mo3askr.Mo3askrDetails.FardDetails.Rotba.RotbaName, 4);
            this.CreateCell(Mo3askr.Mo3askrDetails.FardDetails.FullName, 4);
            this.CreateCell(Utilites.numbersE2A(Mo3askr.Mo3askrDetails.Twagod), 8);
            this.CreateCell(Utilites.numbersE2A(Mo3askr.Mo3askrDetails.Sabab), 8);
            this.CreateCell(Utilites.numbersE2A(Mo3askr.Mo3askrDetails.DateFrom.ToString("dd/MM/yyyy")), 4);
            this.CreateCell(Utilites.numbersE2A(Mo3askr.Mo3askrDetails.DateTo.ToString("dd/MM/yyyy")), 4);

        }

        protected override void ReportBody()
        {
            this.CreateTableHead();
            int i = 1;
            foreach (var Mo3askrsPerZone in Mo3askrsList)
            {
                if (Mo3askrsPerZone.Value.Count > 0)
                {
                    this.CreateTitleWithBackgroundColor($"وحدات فى نطاق {Mo3askrsPerZone.Key}",
                                    fontSize: 12f,
                                    fontStyle: Font.BOLD);
                    foreach (var Mo3askrPerUnit in Mo3askrsPerZone.Value)
                    {
                        if (Mo3askrPerUnit.Value.Count > 0)
                        {
                            this.CreateTitleWithBackgroundColor(Utilites.numbersE2A(Mo3askrPerUnit.Key),
                                    fontSize: 10f,
                                    fontStyle: Font.BOLD,
                                    align: Element.ALIGN_LEFT);
                            foreach (Mo3askr Mo3askr in Mo3askrPerUnit.Value)
                            {
                                this.CreateTableRow(i, Mo3askr);
                                i++;
                            }
                        }
                    }
                }
            }
        }
    }
}