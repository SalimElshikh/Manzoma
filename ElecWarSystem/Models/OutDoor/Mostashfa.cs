using ElecWarSystem.Models.IModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("Mostashfa", Schema = "dbo")]
    public class Mostashfa : Outdoor, IDateLogic, ICleanNav, ICloneable
    {
        public long MostashfaDetailID { get; set; }
        [ForeignKey("MostashfaDetailID")]
        public MostashfaDetails MostashfaDetails { get; set; }

        public void CleanNav()
        {
            Tmam = null;
        }

        public object Clone()
        {
            Mostashfa Mostashfa = new Mostashfa();
            Mostashfa.MostashfaDetailID = this.MostashfaDetailID;
            Mostashfa.TmamID = this.TmamID;
            return Mostashfa;
        }

        public bool IsDateLogic()
        {
            bool result = MostashfaDetails?.DateFrom <= Tmam?.Date;
            return result;
        }
    }
}