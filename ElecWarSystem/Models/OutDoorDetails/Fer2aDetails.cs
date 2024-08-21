using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models.OutDoorDetails
{
    [Table("Fer2aDetails", Schema = "dbo")]
    public class Fer2aDetails : OutdoorDetail
    {
        public String Fer2aName { get; set; } = String.Empty;
        public String Fer2aPlace { get; set; } = String.Empty;
        public long CommandItemID { get; set; }
        [ForeignKey("CommandItemID")]
        public CommandItems CommandItem { get; set; }

    }
}