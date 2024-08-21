using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("Rotba", Schema = "dbo")]
    public class Rotba
    {
        [Key]
        public int ID { get; set; }
        public String RotbaName { get; set; }
        public int RotbaType { get; set; }
    }

}