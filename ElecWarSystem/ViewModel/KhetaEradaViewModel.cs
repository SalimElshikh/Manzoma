using System.Collections.Generic;
using System;
using System.Web.Mvc;
using ElecWarSystem.Models.OutDoor.OutDoorNew;
using ElecWarSystem.Models;
using ElecWarSystem.ViewModel;




namespace ElecWarSystem.ViewModels
{ 
    public class KhetaEradaViewModel : BaseEntitityNewTmam
    {
        public KhetaEradaViewModel()
        {
            Asl7aNames = new List<SelectListItem>();
            MarkbatNames = new List<SelectListItem>();
            Za5iraNames = new List<SelectListItem>();
            Mo3edatNames = new List<SelectListItem>();
        }
        public KhetaErada KhetaErada { get; set; }

        public int SelectedAsl7aNameId { get; set; }
        public IEnumerable<SelectListItem> Asl7aNames { get; set; }
        public int SelectedMarkbatNameId { get; set; }
        public IEnumerable<SelectListItem> MarkbatNames { get; set; }
        public int SelectedZa5iraNameId { get; set; }
        public IEnumerable<SelectListItem> Za5iraNames { get; set; }
        public int SelectedMo3edatNameId { get; set; }
        public IEnumerable<SelectListItem> Mo3edatNames { get; set; }

    }
}