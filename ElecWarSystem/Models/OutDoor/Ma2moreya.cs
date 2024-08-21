using ElecWarSystem.Models.IModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("Ma2moreya", Schema = "dbo")]
    public class Ma2moreya : Outdoor, IDateLogic, ICloneable
    {
        public long Ma2moreyaDetailID { get; set; }
        [ForeignKey("Ma2moreyaDetailID")]
        public Ma2moreyaDetails Ma2moreyaDetails { get; set; }

        public object Clone()
        {
            Ma2moreya Ma2moreya = new Ma2moreya();
            Ma2moreya.Ma2moreyaDetailID = this.Ma2moreyaDetailID;
            Ma2moreya.TmamID = this.TmamID;
            return Ma2moreya;
        }

        public bool IsDateLogic()
        {
            bool result = Ma2moreyaDetails.DateFrom <= Tmam.Date &&
                            Ma2moreyaDetails.DateTo > Tmam.Date;
            return result;
        }
    }
}