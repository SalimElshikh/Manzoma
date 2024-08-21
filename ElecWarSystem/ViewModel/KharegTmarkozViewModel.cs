using ElecWarSystem.Models;
using ElecWarSystem.Models.OutDoor.OutDoorNew;
using ElecWarSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ElecWarSystem.ViewModels
{
    public class KharegTmarkozViewModel : BaseEntitityNewTmam
    {

        public KharegTmarkoz KharegTmarkoz { get; set; }
        public A8radTa7arok A8radTa7aroks { get; set; }
        public KharegTmarkozViewModel()
        {
            KharegTmarkoz = new KharegTmarkoz
            {
                DateFrom = DateTime.Now, 
                DateTo = DateTime.Now  
            };
            Mo3edats = new List<Models.Mo3edat>();
            Asl7as = new List<Asl7a>();
            Za5iras = new List<Za5ira>();
            Markbats = new List<Markbat>();
        }
        [Display(Name = "Asl7aNames")]


        public int A8radTa7arokID { get; set; }
        public IEnumerable<SelectListItem> A8radTa7arokNames { get; set; }

    }

}
