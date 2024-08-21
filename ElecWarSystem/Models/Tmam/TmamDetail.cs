using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("TmamDetails", Schema = "dbo")]
    public class TmamDetails : ICloneable
    {
        [Key]
        public long ID { get; set; }
        public long TmamID { get; set; }
        [ForeignKey("TmamID")]
        public Tmam Tmam { get; set; }
        public bool IsOfficers { get; set; }
        public int Qowwa { get; set; } = 0;
        public int Ma2moreya { get; set; } = 0;
        public int Agaza { get; set; } = 0;
        public int Marady { get; set; } = 0;
        public int Segn { get; set; } = 0;
        public int Gheyab { get; set; } = 0;
        public int Mostashfa { get; set; } = 0;
        public int KharegBelad { get; set; } = 0;
        public int Mo3askar { get; set; } = 0;
        public int Horoob { get; set; } = 0;
        public int Fer2a { get; set; } = 0;
        //return total number of people not existting in the militry unit
        public int GetOutting()
        {
            return Ma2moreya + Agaza + Marady + Segn + Gheyab + Mostashfa + KharegBelad + Mo3askar + Horoob + Fer2a;
        }
        public int GetExisting()
        {
            return Qowwa - GetOutting();
        }
        public int GetOuttingPrecetage()
        {
            int outs = this.GetOutting();
            float prec;
            if (this.Qowwa == 0)
            {
                prec = 0;
            }
            else
            {
                prec = ((float)outs / (float)this.Qowwa) * 100;

            }
            return (int)prec;
        }

        public object Clone()
        {
            TmamDetails tmamDetail = new TmamDetails();
            tmamDetail.IsOfficers = this.IsOfficers;
            tmamDetail.Mostashfa = this.Mostashfa;
            tmamDetail.Qowwa = this.Qowwa;
            tmamDetail.Ma2moreya = this.Ma2moreya;
            tmamDetail.Gheyab = this.Gheyab;
            tmamDetail.Marady = this.Marady;
            tmamDetail.Agaza = this.Agaza;
            tmamDetail.Segn = this.Segn;
            tmamDetail.KharegBelad = this.KharegBelad;
            tmamDetail.Mo3askar = this.Mo3askar;
            tmamDetail.Horoob = this.Horoob;
            tmamDetail.Fer2a = this.Fer2a;
            return tmamDetail;
        }
    }
}