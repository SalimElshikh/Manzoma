using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElecWarSystem.Models
{
    [Table("Fard", Schema = "dbo")]
    public class Fard
    {
        [Key]
        public long ID { get; set; }
        public long TmamID { get; set; }
        [ForeignKey("TmamID")]
        public Tmam Tmam { get; set; }
        public long FardID { get; set; }
        [ForeignKey("FardID")]
        public FardDetails FardDetails { get; set; }
        public TmamEnum Status { get; set; }
    }
}