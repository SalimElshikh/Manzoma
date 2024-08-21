
using ElecWarSystem.Models;
using ElecWarSystem.Models.OutDoor.OutDoorNew;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("KharegTmarkoz", Schema = "dbo")]
    public class KharegTmarkoz 
    {
        public long ID { get; set; }

        public int? A8radTa7arokID { get; set; }
        public A8radTa7arok A8radTa7arok { get; set; }
        public string Qa2edTamarkoz { get; set; }
        public string RotbaQa2edTamarkoz { get; set; }

        public string MakanTamarkoz7ali { get; set; }

        public int DobatNum { get; set; }
        public int DargatNum { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTo { get; set; }

        public int? We7daRa2eeseya_ID { get; set; }
        [ForeignKey("We7daRa2eeseya_ID")]
        public We7daRa2eeseya We7daRa2eeseya { get; set; }

        // Add the relationship with Asl7aNames
        public int Asl7aName_ID { get; set; }
        [ForeignKey("Asl7aName_ID")]

        public Asl7aName Asl7aNames { get; set; }

        public int MarkbatName_ID { get; set; }
        [ForeignKey("MarkbatName_ID")]
        public MarkbatName MarkbatNames { get; set; }
        public int Mo3edatName_ID { get; set; }
        [ForeignKey("Mo3edatName_ID")]
        public Mo3edatName Mo3edatNames { get; set; }
        public int Za5iraName_ID { get; set; }
        [ForeignKey("Za5iraName_ID")]
        public Za5iraName Za5iraNames { get; set; }





    }
}
