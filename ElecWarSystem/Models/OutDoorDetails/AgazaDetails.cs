using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("AgazaDetails", Schema = "dbo")]
    public class AgazaDetails : OutdoorDetail
    {
        public String AgazaType { get; set; }
    }
}