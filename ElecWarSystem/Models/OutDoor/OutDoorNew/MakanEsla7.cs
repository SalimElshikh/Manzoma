using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("MakanEsla7", Schema = "dbo")]
    public class MakanEsla7
    {
        [Key]
        public int ID { get; set; }
        [Display(Name= "مكان الاصلاح")]
        public string Name { get; set; }
    }
}