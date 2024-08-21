using ElecWarSystem.Models;
using ElecWarSystem.Models.OutDoor.OutDoorNew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElecWarSystem.ViewModel
{
    public class AnaserMonawbaViewModel  : BaseEntitityNewTmam
    {
        public AnaserMonawba AnaserMonawbas { get; set; }
        public AnaserMonawbaViewModel()
        {

            Mo3edats = new List<Mo3edat>();
            Asl7as = new List<Asl7a>();
            Za5iras = new List<Za5ira>();
            Markbats = new List<Markbat>();
        }
    }
}