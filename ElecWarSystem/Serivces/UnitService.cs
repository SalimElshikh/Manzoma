using ElecWarSystem.Data;
using ElecWarSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace ElecWarSystem.Serivces
{
    public class UnitService
    {
        private readonly AppDBContext appDBContext;
        public UnitService()
        {
            appDBContext = new AppDBContext();
        }


        public List<We7daRa2eeseya> GetByZone(int Taba3eyaID)
        {
            List<We7daRa2eeseya> units = appDBContext.We7daRa2eeseya.Where(row => row.Taba3eyaID == Taba3eyaID && row.Tarteeb < 43).ToList();
            return units;
        }
        public We7daRa2eeseya GetUnit(int id)
        {
            return appDBContext.We7daRa2eeseya.Find(id);
        }
    }
}