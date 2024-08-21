using ElecWarSystem.Models.OutDoor.OutDoorNew;
using iText.Layout.Element;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("A8radTgarob", Schema = "dbo")]
    public class A8radTgarob
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "أغراض التجارب")]
        public string a8radTgarob { get; set; }

        public ICollection<TgarobMydanyas> TgarobMydanyas { get; set; }

    }
}