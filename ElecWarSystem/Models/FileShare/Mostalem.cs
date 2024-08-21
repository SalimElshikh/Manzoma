using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("Mostalem", Schema = "dbo")]
    public class Mostalem
    {
        [Key]
        public long ID { get; set; }
        public long EmailID { get; set; }
        [ForeignKey("EmailID")]
        public Email Email { get; set; }
        public int MostalemID { get; set; }
        [ForeignKey("MostalemID")]
        public We7daRa2eeseya RecieverUser { get; set; }
        public bool Maqroo2 { get; set; } = false;
        public bool Starred { get; set; } = false;
    }
}