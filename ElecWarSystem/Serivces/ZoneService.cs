using ElecWarSystem.Data;
using ElecWarSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace ElecWarSystem.Serivces
{
    public class ZoneService
    {
        private readonly AppDBContext appDBContext;
        public ZoneService()
        {
            appDBContext = new AppDBContext();
        }
        public List<Taba3eya> GetAll()
        {
            List<Taba3eya> zones = new List<Taba3eya>();
            zones.Add(new Taba3eya { ID = 0, Taba3eyaName = "", Taba3eyaAlias = "" });
            zones.AddRange(appDBContext.Taba3eya.ToList());
            return zones;
        }
        public List<Taba3eya> GetZones()
        {
            List<Taba3eya> zones = appDBContext.Taba3eya.ToList();
            return zones;
        }
    }
}