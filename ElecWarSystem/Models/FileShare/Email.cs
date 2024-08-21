using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("Email", Schema = "dbo")]
    public class Email
    {
        public long ID { get; set; }
        public int SenderUserID { get; set; }
        [ForeignKey("SenderUserID")]
        public We7daRa2eeseya Sender { get; set; }
        public String Subject { get; set; }
        public String EmailText { get; set; }
        [Column(TypeName = "datetime")]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime SendDateTime { get; set; }
        public List<Mostalem> Mostalem { get; set; } = new List<Mostalem>();
        public List<Document> Document { get; set; } = new List<Document>();
    }
}