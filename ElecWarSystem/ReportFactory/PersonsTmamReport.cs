using ElecWarSystem.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElecWarSystem.ReportFactory
{
    public class FardDetailssTmamReport : ReportGenerator<TmamDetails>
    {
        private Dictionary<String, List<TmamDetails>> officerTmamList;
        public FardDetailssTmamReport(Dictionary<String, List<TmamDetails>> officerTmamList, DateTime tmamDate, string title)
            : base(tmamDate, title, 18,4)
        {
            this.officerTmamList = officerTmamList;
        }

        protected override void CreateTableHead()
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 10f, Font.BOLD);
            baseColor = BaseColor.LIGHT_GRAY;
            this.CreateCell("م");
            this.CreateCell("الوحدة", 4);
            this.CreateCell("القوة");
            this.CreateCell("موجود");
            this.CreateCell("خارج");
            this.CreateCell("أجازة");
            this.CreateCell("أجازة مرضية");
            this.CreateCell("فرقة");
            this.CreateCell("مأمورية");
            this.CreateCell("سجن");
            this.CreateCell("غياب");
            this.CreateCell("مستشفى");
            this.CreateCell("خ البلاد");
            this.CreateCell("م تد خارجى");
            this.CreateCell("نسبة الخوارج");
        }

        protected override void CreateTableRow(int i, TmamDetails tmamdetail)
        {
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 8f, Font.BOLD);
            this.CreateCell(Utilites.numbersE2A(i.ToString()));
            this.CreateCell(Utilites.numbersE2A(tmamdetail.Tmam.We7daRa2eeseya.We7daName), 4);
            font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 8f, Font.NORMAL);
            this.CreateCell(Utilites.numbersE2A(tmamdetail.Qowwa.ToString()));
            this.CreateCell(Utilites.numbersE2A(tmamdetail.GetExisting().ToString()));
            this.CreateCell(Utilites.numbersE2A(tmamdetail.GetOutting().ToString()));
            this.CreateCell(Utilites.numbersE2A(tmamdetail.Agaza.ToString()));
            this.CreateCell(Utilites.numbersE2A(tmamdetail.Marady.ToString()));
            this.CreateCell(Utilites.numbersE2A(tmamdetail.Fer2a.ToString()));
            this.CreateCell(Utilites.numbersE2A(tmamdetail.Ma2moreya.ToString()));
            this.CreateCell(Utilites.numbersE2A(tmamdetail.Segn.ToString()));
            this.CreateCell(Utilites.numbersE2A(tmamdetail.Gheyab.ToString()));
            this.CreateCell(Utilites.numbersE2A(tmamdetail.Mostashfa.ToString()));
            this.CreateCell(Utilites.numbersE2A(tmamdetail.KharegBelad.ToString()));
            this.CreateCell(Utilites.numbersE2A(tmamdetail.Mo3askar.ToString()));
            this.CreateCell($"{Utilites.numbersE2A(tmamdetail.GetOuttingPrecetage().ToString())}%");
        }

        protected override void ReportBody()
        {
            this.CreateTableHead();
            int i = 1;
            foreach (var officersTmamInZone in this.officerTmamList)
            {
                this.CreateTitleWithBackgroundColor($"وحدات فى نطاق {officersTmamInZone.Key}",
                    fontSize: 12f,
                    fontStyle: Font.BOLD);

                font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, 8f, Font.NORMAL);
                baseColor = BaseColor.WHITE;
                foreach (TmamDetails tmamDetail in officersTmamInZone.Value)
                {
                    this.CreateTableRow(i, tmamDetail);
                    i++;
                }
            }
        }
    }
}