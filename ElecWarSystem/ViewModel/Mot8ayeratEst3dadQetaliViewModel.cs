using ElecWarSystem.Models.OutDoor.OutDoorNew;
using ElecWarSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElecWarSystem.ViewModel
{
    public class Mot8ayeratEst3dadQetaliIndexViewModel
    {
        public Mot8ayeratEst3dadQetali Mot8ayeratEst3dadQetali { get; set; }
        public List<Al7ala> Al7alas { get; set; }
        public List<Ge7aTasdek> Ge7aTasdeks { get; set; }
        public List<A8radTa7arok> A8radTa7arok { get; set; }
        public int SelectAl7alaNamesId { get; set; }
        public IEnumerable<SelectListItem> listAl7alaNames { get; set; }
        public int SelectGe7aTasdekNamesId { get; set; }
        public IEnumerable<SelectListItem> listGe7aTasdekNames { get; set; }
        public int SelectA8radTa7arokNamesId { get; set; }
        public IEnumerable<SelectListItem> listA8radTa7arokNames { get; set; }
        public Mot8ayeratEst3dadQetaliIndexViewModel()
        {
            Mot8ayeratEst3dadQetali = new Mot8ayeratEst3dadQetali
            {
                DateFrom = DateTime.Now, 
                DateTo = DateTime.Now  
            };
        }


    }
}
