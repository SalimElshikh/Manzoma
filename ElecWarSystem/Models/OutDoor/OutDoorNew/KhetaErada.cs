using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElecWarSystem.Models.OutDoor.OutDoorNew
{
    public class KhetaErada
    {
        public long ID { get; set; }
        public string Qa2ed { get; set; }
        public string RotbaQa2ed { get; set; }

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