using ElecWarSystem.Models.IModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("Agaza", Schema = "dbo")]
    public class Agaza : Outdoor , ICleanNav, IDateLogic, ICloneable
    {
        public long AgazaDetailID { get; set; }
        [ForeignKey("AgazaDetailID")]
        public AgazaDetails AgazaDetails { get; set; }

        public void CleanNav()
        {
            Tmam = null;
        }

        public object Clone()
        {
            Agaza Agaza = new Agaza();
            Agaza.AgazaDetailID = this.AgazaDetailID;
            Agaza.TmamID = this.TmamID;
            return Agaza;
        }

        public bool IsDateLogic()
        {
            bool result = AgazaDetails.DateFrom <= Tmam.Date &&
                            AgazaDetails.DateTo > Tmam.Date;
            return result;
        }
    }
}