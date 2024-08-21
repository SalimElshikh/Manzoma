using ElecWarSystem.Models.IModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("Segn", Schema = "dbo")]
    public class Segn : Outdoor, IDateLogic, ICloneable
    {
        public long SegnDetailID { get; set; }
        [ForeignKey("SegnDetailID")]
        public SegnDetails SegnDetails { get; set; }

        public object Clone()
        {
            Segn Segn = new Segn();
            Segn.SegnDetailID = this.SegnDetailID;
            Segn.TmamID= this.TmamID;
            return Segn;
        }

        public bool IsDateLogic()
        {
            bool result = SegnDetails.DateFrom <= Tmam.Date &&
                            SegnDetails.DateTo > Tmam.Date;
            return result;
        }
    }
}