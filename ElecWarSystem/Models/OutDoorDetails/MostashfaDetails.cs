using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElecWarSystem.Models
{
    [Table("MostashfaDetails", Schema = "dbo")]
    public class MostashfaDetails : OutdoorDetail
    {
        public String Mostashfa { get; set; } = String.Empty;
        public String Hala { get; set; } = String.Empty;
        public String Tawseya { get; set; } = String.Empty;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static bool operator ==(MostashfaDetails mostashfaDetails1 , MostashfaDetails mostashfaDetails2)
        {
            bool isEqual = false;
            isEqual = ((mostashfaDetails1.FardID == mostashfaDetails2.FardID) &&
                            (mostashfaDetails1.DateFrom == mostashfaDetails2.DateFrom));
            
            return isEqual;
        }

        public static bool operator !=(MostashfaDetails mostashfaDetails1, MostashfaDetails mostashfaDetails2)
        {
            bool isEqual = false;
            isEqual = ((mostashfaDetails1.FardID != mostashfaDetails2.FardID) &&
                            (mostashfaDetails1.DateFrom != mostashfaDetails2.DateFrom));

            return isEqual;
        }
    }
}