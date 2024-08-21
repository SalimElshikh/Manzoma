using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElecWarSystem.Models
{
    [Table("KharegBeladDetails", Schema = "dbo")]
    public class KharegBeladDetails : OutdoorDetail
    {
        public String Balad { get; set; } = String.Empty;
        public String Sabab { get; set; } = String.Empty;
    }
}