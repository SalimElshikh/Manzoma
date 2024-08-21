using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("GheyabDetails", Schema = "dbo")]
    public class GheyabDetails : OutdoorDetail
    {
        public int GheyabTimes { get; set; }
        public CommandItems commandItem { get; set; }

    }
}