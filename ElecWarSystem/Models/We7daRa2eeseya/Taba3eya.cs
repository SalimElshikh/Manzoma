using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("Taba3eya", Schema = "dbo")]
    public class Taba3eya
    {
        [Key]
        [Display(Name = "ID")]
        public int ID { get; set; }
        public String Taba3eyaName { get; set; } = String.Empty;
        public String Taba3eyaAlias { get; set; } = String.Empty;
        public List<We7daRa2eeseya> We7daRa2eeseya { get; set; } = new List<We7daRa2eeseya>();
    }
}