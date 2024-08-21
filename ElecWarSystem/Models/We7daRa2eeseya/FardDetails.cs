using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("FardDetails", Schema = "dbo")]
    public class FardDetails
    {
        [Key]
        public long ID { get; set; }
        
        public string Raqam3askary { get; set; }
        public int We7daID { get; set; }
        [ForeignKey("We7daID")]
        public We7daRa2eeseya We7daRa2eeseya { get; set; }
        public int? RotbaID { get; set; }
        [ForeignKey("RotbaID")]
        public Rotba Rotba { get; set; }
        public String FullName { get; set; }
        public bool OnDuty { get; set; } = true;
        public bool Status { get; set; } = false;
    }
}