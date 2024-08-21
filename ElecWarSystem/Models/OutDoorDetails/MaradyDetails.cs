using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("MaradyDetails", Schema = "dbo")]
    public class MaradyDetails : OutdoorDetail
    {
        public String Mostashfa { get; set; } = String.Empty;
        [Column(TypeName = "datetime")]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime MostashfaDate { get; set; }
        public String Hala { get; set; } = String.Empty;
    }
}