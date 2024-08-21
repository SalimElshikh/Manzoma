using ElecWarSystem.Models;
using ElecWarSystem.Models.OutDoor.OutDoorNew;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ElecWarSystem.Models
{
    public class Mot8ayeratEst3dadQetali
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب.")]
        [RegularExpression("^[a-zA-Zء-ي]+$", ErrorMessage = "الرجاء إدخال حروف أبجدية فقط.")]
        [Display(Name = "العنصر")]
        public string Item { get; set; }

        public int? Al7ala_ID { get; set; }
        [ForeignKey("Al7ala_ID")]
        public Al7ala Al7ala { get; set; }
        public int? Ge7aTasdek_ID { get; set; }
        [ForeignKey("Ge7aTasdek_ID")]
        public Ge7aTasdek Ge7aTasdek { get; set; }
        public int? A8radTa7arok_ID { get; set; }
        [ForeignKey("A8radTa7arok_ID")]
        public A8radTa7arok A8radTa7arok { get; set; }
        [Display(Name = "التاريخ من ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTo { get; set; }

        [Display(Name  = "التاريخ الى ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateFrom { get; set; }

    }
}
