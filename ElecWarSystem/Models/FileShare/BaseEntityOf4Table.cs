using ElecWarSystem.Models.OutDoor.OutDoorNew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElecWarSystem.Models.FileShare
{
    public class BaseEntityOf4Table
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<KharegTmarkoz> KharegTmarkozs { get; set; }
        public List<TgarobMydanyas> TgarobMydanyas { get; set; }
        public List<AnaserMonawba> AnaserMonawbas { get; set; }
        public List<KhetaErada> KhetaEradas { get; set; }

    }
}