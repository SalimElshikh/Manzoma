using ElecWarSystem.Models.IModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("Marady", Schema = "dbo")]
    public class Marady : Outdoor, IDateLogic, ICleanNav,ICloneable
    {
        public long MaradyDetailID { get; set; }
        [ForeignKey("MaradyDetailID")]
        public MaradyDetails MaradyDetails { get; set; }

        public void CleanNav()
        {
            Tmam = null;
        }

        public object Clone()
        {
            Marady Marady = new Marady();
            Marady.MaradyDetailID = this.MaradyDetailID;
            Marady.TmamID= this.TmamID;
            return Marady;
        }

        public bool IsDateLogic()
        {
            bool result = MaradyDetails.DateFrom <= Tmam.Date &&
                            MaradyDetails.DateTo > Tmam.Date && 
                            MaradyDetails.MostashfaDate <= Tmam.Date;
            return result;
        }
    }
}