using System.ComponentModel.DataAnnotations;

namespace ElecWarSystem.Models.OutDoor.OutDoorNew
{
    public class TypeMo2eda
    {
        public int ID { get; set; }
        [Display(Name = "نوع المعدة")]
        public string Name { get; set; }
    }
}