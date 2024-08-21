using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElecWarSystem.Models.OutDoor.OutDoorNew
{
    public class TgarobMydanyas
    {

        public long  ID { get; set; }
        public string Qa2edTaghroba { get; set; }
        public string RotbaQa2edTaghroba { get; set; }
        public string MkanEltaghroba { get; set; }
        public string TagrobaType { get; set; }
        public long DobatNum { get; set; }
        public long DargatNum { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTo { get; set; }

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