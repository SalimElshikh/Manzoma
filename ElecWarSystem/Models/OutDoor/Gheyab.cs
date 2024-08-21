using ElecWarSystem.Models.IModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("Gheyab", Schema = "dbo")]
    public class Gheyab : Outdoor , ICloneable, IDateLogic
    {
        public long GheyabDetailsID { get; set; }
        [ForeignKey("GheyabDetailsID")]
        public GheyabDetails GheyabDetails { get; set; }

        public object Clone()
        {
            Gheyab Gheyab = new Gheyab();
            Gheyab.GheyabDetailsID = this.GheyabDetailsID;
            Gheyab.TmamID = this.TmamID;
            return Gheyab;
        }

        public bool IsDateLogic()
        {
            bool result = GheyabDetails.DateFrom <= Tmam.Date;
            return result;
        }
    }
}