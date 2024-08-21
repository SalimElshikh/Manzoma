using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ElecWarSystem.Models.OutDoor.OutDoorNew
{
    public class Ge7aTasdek
    {
        public int ID { get; set; }
        [Display(Name = "جهة التصديق")]
        public string Name { get; set; }    

    }
}