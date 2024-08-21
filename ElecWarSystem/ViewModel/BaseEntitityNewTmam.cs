using ElecWarSystem.Models.OutDoor.OutDoorNew;
using ElecWarSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElecWarSystem.ViewModel
{
    public class BaseEntitityNewTmam
    {



        public List<Asl7a> Asl7as { get; set; }
        public List<Mo3edat> Mo3edats { get; set; }
        public List<Markbat> Markbats { get; set; }
        public List<Za5ira> Za5iras { get; set; }

        public int SelectAsl7aNamesId { get; set; }
        public IEnumerable<SelectListItem> listAsl7aNames { get; set; }
        public int SelectMarkbatNamesId { get; set; }
        public IEnumerable<SelectListItem> listMarkbatNames { get; set; }
        public int SelectMo3edatNamesId { get; set; }
        public IEnumerable<SelectListItem> listMo3edatNames { get; set; }
        public int SelectZa5iraNamesId { get; set; }
        public IEnumerable<SelectListItem> listZa5iraNames { get; set; }
        public List<SelectListItem> FullNames { get; set; }
        public List<SelectListItem> Rotbas { get; set; }
        
        
    }
}