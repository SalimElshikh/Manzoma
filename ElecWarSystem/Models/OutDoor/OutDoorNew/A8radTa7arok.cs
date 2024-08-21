using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("A8radTa7arok", Schema = "dbo")]
    public class A8radTa7arok
    {

        [Key]
        public int ID { get; set; }
        [Display(Name = "اغراض التحرك")]
        public string a8radTa7arok { get; set; }
        public List<KharegTmarkoz> KharegTmarkozs { get; set; }
    }
}