using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("Al7ala", Schema = "dbo")]
    public class Al7ala
    {
        [Key]
        public int ID { get; set; }
        [Display(Name="الحالة")]
        public string Name { get; set; }
    }
}