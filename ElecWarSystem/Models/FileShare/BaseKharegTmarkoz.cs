using ElecWarSystem.Models.OutDoor.OutDoorNew;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElecWarSystem.Models.FileShare
{
    public class BaseKharegTmarkoz 
    {
        public long ID { get; set; }
        public int Num { get; set; }

        public string Name { get; set; }

        public long? KharegTmarkozs_ID { get; set; }
        [ForeignKey("KharegTmarkozs_ID")]
        public KharegTmarkoz KharegTmarkozs { get; set; }

        public long? TgarobMydanyas_ID { get; set; }
        [ForeignKey("TgarobMydanyas_ID")]
        public TgarobMydanyas TgarobMydanyas { get; set; }
    }
}