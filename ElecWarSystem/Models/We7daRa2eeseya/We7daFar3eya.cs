using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("We7daFar3eya", Schema = "dbo")]
    public class We7daFar3eya
    {
        [Key]
        public int ID { get; set; }
        public int We7daRa2eseyaID { get; set; }
        [ForeignKey("We7daRa2eseyaID")]
        public We7daRa2eeseya We7daRa2eseya { get; set; }
        public String We7daName { get; set; }
        public long? Qa2edWe7daID { get; set; }
        [ForeignKey("Qa2edWe7daID")]
        public FardDetails Qa2edWe7da { get; set; }
        public long? Ra2ees3ameleyatID { get; set; }
        [ForeignKey("Ra2ees3ameleyatID")]
        public FardDetails Ra2ees3ameleyat { get; set; }
    }
}