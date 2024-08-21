using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("SegnDetails", Schema = "dbo")]
    public class SegnDetails : OutdoorDetail
    {
        public String Gareema { get; set; } = String.Empty;
        public String Eqab { get; set; } = String.Empty;
        public String Mo3aqeb { get; set; } = String.Empty;
        public String SegnPlace { get; set; } = String.Empty;
        public long CommandItemID { get; set; }
        [ForeignKey("CommandItemID")]
        public CommandItems CommandItem { get; set; }
    }
}