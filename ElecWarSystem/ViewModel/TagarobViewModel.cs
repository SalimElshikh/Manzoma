using ElecWarSystem.Models;
using ElecWarSystem.Models.OutDoor.OutDoorNew;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using ElecWarSystem.ViewModels;
using ElecWarSystem.ViewModel;

namespace ElecWarSystem.ViewModels
{
    public class TagarobViewModel : BaseEntitityNewTmam
    {
        public TgarobMydanyas TgarobMydanya { get; set; }
        public A8radTa7arok A8radTa7arok { get; set; }
        public TagarobViewModel()
        {
            Mo3edats = new List<Mo3edat>();
            Asl7as = new List<Asl7a>();
            Za5iras = new List<Za5ira>();
            Markbats = new List<Markbat>();
            TgarobMydanya = new TgarobMydanyas
            {
                DateFrom = DateTime.Now, 
                DateTo =  DateTime.Now  
            };

        }
        public int A8radTagarobID { get; set; }
        public IEnumerable<SelectListItem> A8radTagarobNames { get; set; }

    }
}