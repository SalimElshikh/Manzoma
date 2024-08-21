using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("MostawaEIsla7", Schema = "dbo")]
    public class MostawaEIsla7
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "مستوي الاصلاح")]
        public string Name { get; set; }
    }
}