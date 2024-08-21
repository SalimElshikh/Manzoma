using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("Users", Schema = "dbo")]
    public class User
    {
        [Key]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [StringLength(45)]
        [Index(IsUnique = true)]
        public String UserName { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; } = false;
        public int We7daID { get; set; }
        [ForeignKey("We7daID")]
        public We7daRa2eeseya We7daRa2eeseya { get; set; }
        public UserRoles Roles { get; set; }
    }
}