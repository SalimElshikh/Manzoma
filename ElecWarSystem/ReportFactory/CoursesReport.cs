using ElecWarSystem.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElecWarSystem.Models.OutDoor;

namespace ElecWarSystem.ReportFactory
{
    public class Fer2asReport : ReportGenerator<Fer2a>
    {
        private Dictionary<String, Dictionary<String, List<Fer2a>>> Fer2asList;
        public Fer2asReport(Dictionary<String, Dictionary<String, List<Fer2a>>> Fer2asList,
            DateTime tmamDate, string title)
            : base(tmamDate, title, 30,4)
        {
            this.Fer2asList = Fer2asList;
        }

        protected override void CreateTableHead()
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 10f, Font.BOLD);
            baseColor = BaseColor.LIGHT_GRAY;
            this.CreateCell("م");
            this.CreateCell("الرتبة/الدرجة", 2);
            this.CreateCell("الإسم", 4);
            this.CreateCell("الفرقة/الدورة", 6);
            this.CreateCell("مكان إنعقاد الفرقة/الدورة", 6);
            this.CreateCell("المدة من", 3);
            this.CreateCell("المدة إلى", 3);
            this.CreateCell("بند الأوامر", 5);

        }

        protected override void CreateTableRow(int i, Fer2a Fer2a)
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 8f, Font.NORMAL);
            baseColor = BaseColor.WHITE;
            this.CreateCell($"{Utilites.numbersE2A(i.ToString())}");
            this.CreateCell(Fer2a.Fer2aDetails.FardDetails.Rotba.RotbaName, 2);
            this.CreateCell(Fer2a.Fer2aDetails.FardDetails.FullName, 4);
            this.CreateCell(Utilites.numbersE2A(Fer2a.Fer2aDetails.Fer2aName), 6);
            this.CreateCell(Utilites.numbersE2A(Fer2a.Fer2aDetails.Fer2aPlace), 6);
            this.CreateCell(Utilites.numbersE2A(Fer2a.Fer2aDetails.DateFrom.ToString("dd/MM/yyyy")), 3);
            this.CreateCell(Utilites.numbersE2A(Fer2a.Fer2aDetails.DateTo.ToString("dd/MM/yyyy")), 3);
            this.CreateCell(Utilites.numbersE2A(Fer2a.Fer2aDetails.CommandItem.Number.ToString()), 2);
            this.CreateCell(Utilites.numbersE2A(Fer2a.Fer2aDetails.CommandItem.Date.ToString("dd/MM/yyyy")), 3);

        }

        protected override void ReportBody()
        {
            this.CreateTableHead();
            int i = 1;
            foreach (var Fer2asPerZone in Fer2asList)
            {
                if (Fer2asPerZone.Value.Count > 0)
                {
                    this.CreateTitleWithBackgroundColor($"وحدات فى نطاق {Fer2asPerZone.Key}",
                                    fontSize: 12f,
                                    fontStyle: Font.BOLD);
                    foreach (var Fer2aPerUnit in Fer2asPerZone.Value)
                    {
                        if (Fer2aPerUnit.Value.Count > 0)
                        {
                            this.CreateTitleWithBackgroundColor(Utilites.numbersE2A(Fer2aPerUnit.Key),
                                    fontSize: 10f,
                                    fontStyle: Font.BOLD,
                                    align: Element.ALIGN_LEFT);
                            foreach (Fer2a Fer2a in Fer2aPerUnit.Value)
                            {
                                this.CreateTableRow(i, Fer2a);
                                i++;
                            }
                        }
                    }
                }
            }
        }
    }
}