using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("We7daRa2eeseya", Schema = "dbo")]
    public class We7daRa2eeseya
    {
        [Key]
        [Display(Name = "ID")]
        public int ID { get; set; }
        public int Taba3eyaID { get; set; }
        [ForeignKey("Taba3eyaID")]
        public Taba3eya zone { get; set; }
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string We7daName { get; set; }
        public long StorageSize { get; set; } = 5368709120;
        public long UsedStorage { get; set; } = 0;
        public int Tarteeb { get; set; } = 0;
        public bool TawagodQa2edManoob { get; set; } = true;
        public long? Qa2edWe7daID { get; set; }
        [ForeignKey("Qa2edWe7daID")]
        public FardDetails Qa2edWe7da { get; set; }
        public long? Ra2ees3ameleyatID { get; set; }
        [ForeignKey("Ra2ees3ameleyatID")]
        public FardDetails Ra2ees3ameleyat { get; set; }
        public List<We7daFar3eya> We7daFar3eya { get; set; }
        public ICollection<FardDetails> FardDetails { get; set; }
        public List<KharegTmarkoz> KharegTmarkozs { get;}
    }
}