using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("Ma2moreyaDetails", Schema = "dbo")]
    public class Ma2moreyaDetails : OutdoorDetail
    {
        public String Ma2moreyaPlace { get; set; } = String.Empty;
        public String Ma2moreyaCommandor { get; set; } = String.Empty;
    }
}