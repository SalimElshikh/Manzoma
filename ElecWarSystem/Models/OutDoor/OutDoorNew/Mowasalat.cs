using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("Mowasalat", Schema = "dbo")]
    public class Mowasalat
    {
        [Key]
        public int ID { get; set; }
        [Display(Name= "استراتيجي القائد")]
        public bool EstrategyQa2ed { get; set; }
        [Display(Name = "استراتيجي مركز العمليات")]
        public bool EstrategyMarkazAmaliat { get; set; }
        [Display(Name = "استراتيجي تحويلة")]
        public bool EstrategyTa7wela  { get; set; }
        [Display(Name = "سنترال القائد")]
        public bool SentralQa2ed { get; set; }
        [Display(Name = "استراتيجي مركز العمليات")]
        public bool SentralMarkazAmaliat { get; set; }
        [Display(Name = "مواصلات لاسلكية")]
        public bool MowaslatLaselkya { get; set; }
        [Display(Name = "نقل البيانات")]
        public bool TransfarData { get; set; }
        [Display(Name = "الخط الساخن ")]
        public bool HotNumber { get; set; }



    }
}