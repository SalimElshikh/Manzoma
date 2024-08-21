using ElecWarSystem.Models.IModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElecWarSystem.Models
{
    public class Mo3askr : Outdoor, IDateLogic, ICleanNav, ICloneable
    {
        public long Mo3askrDetailsID { get; set; }
        [ForeignKey("Mo3askrDetailsID")]
        public Mo3askrDetails Mo3askrDetails { get; set; }

        public void CleanNav()
        {
            Tmam = null;
        }

        public object Clone()
        {
            Mo3askr mo3askr = new Mo3askr();
            mo3askr.Mo3askrDetailsID = this.Mo3askrDetailsID;
            mo3askr.TmamID= this.TmamID;
            return mo3askr;
        }

        public bool IsDateLogic()
        {
            bool result = Mo3askrDetails.DateFrom <= Tmam.Date &&
                                Mo3askrDetails.DateTo > Tmam.Date;
            return result;
        }
    }
}