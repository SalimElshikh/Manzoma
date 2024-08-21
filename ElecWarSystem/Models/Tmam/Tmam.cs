using ElecWarSystem.Models.OutDoor;
using ElecWarSystem.Serivces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ElecWarSystem.Models
{
    [Table("Tmam", Schema = "dbo")]
    public class Tmam : ICloneable
    {
        [Key]
        public long ID { get; set; }
        [Column(TypeName = "datetime")]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime Date { get; set; }
        public int We7daID { get; set; }
        [ForeignKey("We7daID")]
        public We7daRa2eeseya We7daRa2eeseya { get; set; }
        //AltCommander property ==> ka2ed Manoop
        public long? Qa2edManoobID { get; set; }
        [ForeignKey("Qa2edManoobID")]
        public FardDetails AltCommander { get; set; }
        public bool Submitted { get; set; } = false;
        public bool Recieved { get; set; } = false;
        public String Sa3at { get; set; } = "-";
        public List<TmamDetails> TmamDetails { get; set; } = new List<TmamDetails>();
        public List<Marady> Marady { get; set; } = new List<Marady>();
        public List<Ma2moreya> Ma2moreya { get; set; } = new List<Ma2moreya>();
        public List<Agaza> Agaza { get; set; } = new List<Agaza>();
        public List<Segn> Segn { get; set; } = new List<Segn>();
        public List<Gheyab> Gheyab { get; set; } = new List<Gheyab>();
        public List<Mostashfa> Mostashfa { get; set; } = new List<Mostashfa>();
        public List<KharegBelad> KharegBelad { get; set; } = new List<KharegBelad>();
        public List<Mo3askr> Mo3askr { get; set; } = new List<Mo3askr>();
        public List<Fer2a> Fer2a { get; set; } = new List<Fer2a>();



        public object Clone()
        {
            Tmam tmam = new Tmam() { We7daID = this.We7daID, Date = DateTime.Today.AddDays(1) };
            this.Date = tmam.Date;
            FardService FardService = new FardService();
            foreach (TmamDetails tmamDetail in this.TmamDetails)
            {
                
                tmam.TmamDetails.Add((TmamDetails)tmamDetail.Clone());
            }
            //each tmam-Gheyab-Agaza-Ma2moreya-Marady ==> clone function making a copy of yesterday tmam
            // and each unit can update its tmam 
            foreach (Marady Marady in this.Marady)
            {
                if (Marady.IsDateLogic())
                {
                    tmam.Marady.Add((Marady)Marady.Clone());
                }
            }

            foreach (Gheyab Gheyab in this.Gheyab)
            {
                if (Gheyab.IsDateLogic())
                {
                    tmam.Gheyab.Add((Gheyab)Gheyab.Clone());
                }
            }

            foreach (Ma2moreya Ma2moreya in this.Ma2moreya)
            {
                if (Ma2moreya.IsDateLogic())
                {
                    tmam.Ma2moreya.Add((Ma2moreya)Ma2moreya.Clone());
                }
            }

            foreach (Agaza Agaza in this.Agaza)
            {
                if (Agaza.IsDateLogic())
                {
                    tmam.Agaza.Add((Agaza)Agaza.Clone());
                }
            }

            foreach (Segn Segn in this.Segn)
            {
                if (Segn.IsDateLogic())
                {
                    tmam.Segn.Add((Segn)Segn.Clone());
                }
            }
            foreach (Mostashfa Mostashfa in this.Mostashfa)
            {
                if (Mostashfa.IsDateLogic())
                {
                    tmam.Mostashfa.Add((Mostashfa)Mostashfa.Clone());
                }
            }
            foreach (KharegBelad KharegBelad in this.KharegBelad)
            {
                if (KharegBelad.IsDateLogic())
                {
                    tmam.KharegBelad.Add((KharegBelad)KharegBelad.Clone());
                }
            }
            foreach (Mo3askr mo3askr in this.Mo3askr)
            {
                if (mo3askr.IsDateLogic())
                {
                    tmam.Mo3askr.Add((Mo3askr)mo3askr.Clone());
                }
            }
            foreach (Fer2a Fer2a in this.Fer2a)
            {
                if (Fer2a.IsDateLogic())
                {
                    tmam.Fer2a.Add((Fer2a)Fer2a.Clone());
                }
            }
            return tmam;
        }
    }
}