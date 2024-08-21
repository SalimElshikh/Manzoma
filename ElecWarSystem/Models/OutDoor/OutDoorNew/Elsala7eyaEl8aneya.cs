using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElecWarSystem.Models.OutDoor.OutDoorNew
{
    public class Elsala7eyaEl8aneya
    {
        public int ID { get; set; }
        [Display(Name = "اسم المعدة")]
        public string NameMo2eda  { get; set; }
        [Display(Name = "عدد المعدة")]
        public int NumMo2eda { get; set; }
        [Display(Name = "الإجراءات المتخدة")]
        public string El2gra { get; set; }
        public int? MakanEsla7_ID { get; set; }
        [ForeignKey("MakanEsla7_ID")]
        public MakanEsla7 MakanEsla7s { get; set; }
        public int? MostawaEIsla7_ID { get; set; }
        [ForeignKey("MostawaEIsla7_ID")]
        public MostawaEIsla7 MostawaEIsla7s { get; set; }
        public int? TypeMo2eda_ID { get; set; }
        [ForeignKey("TypeMo2eda_ID")]
        public TypeMo2eda TypeMo2edas { get; set; }

    }
}