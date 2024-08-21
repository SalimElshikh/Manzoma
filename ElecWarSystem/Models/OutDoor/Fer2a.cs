using ElecWarSystem.Models.OutDoorDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElecWarSystem.Models.OutDoor
{
    [Table("Fer2a", Schema = "dbo")]

    public class Fer2a : Outdoor
    {
        public long Fer2aDetailsID { get; set; }
        [ForeignKey("Fer2aDetailsID")]
        public Fer2aDetails Fer2aDetails { get; set; }
        public void CleanNav()
        {
            Tmam = null;
        }

        public object Clone()
        {
            Fer2a Fer2a = new Fer2a();
            Fer2a.Fer2aDetailsID = this.Fer2aDetailsID;
            Fer2a.TmamID = this.TmamID;
            return Fer2a;
        }

        public bool IsDateLogic()
        {
            bool result = Fer2aDetails.DateFrom <= Tmam.Date &&
                                Fer2aDetails.DateTo > Tmam.Date;
            return result;
        }
    }
}