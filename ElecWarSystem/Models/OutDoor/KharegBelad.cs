using ElecWarSystem.Models.IModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElecWarSystem.Models
{
    [Table("KharegBelad", Schema = "dbo")]
    public class KharegBelad: Outdoor, IDateLogic, ICleanNav, ICloneable
    {
        public long KharegBeladDetailID { get; set; }
        [ForeignKey("KharegBeladDetailID")]
        public KharegBeladDetails KharegBeladDetails { get; set; }

        public void CleanNav()
        {
            Tmam = null;
        }

        public object Clone()
        {
            KharegBelad KharegBelad = new KharegBelad(); 
            KharegBelad.KharegBeladDetailID = this.KharegBeladDetailID;
            KharegBelad.TmamID= this.TmamID;
            return KharegBelad;
        }

        public bool IsDateLogic()
        {
            bool result = KharegBeladDetails.DateFrom <= Tmam.Date &&
                            KharegBeladDetails.DateTo > Tmam.Date;
            return result;
        }
    }
}