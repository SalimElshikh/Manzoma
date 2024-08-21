using ElecWarSystem.Data;
using ElecWarSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace ElecWarSystem.Serivces
{
    public class RotbaService
    {
        private readonly AppDBContext appDBContext;
        public RotbaService()
        {
            appDBContext = new AppDBContext();
        }
        public List<Rotba> GetRotbasOf(int type)
        {
            List<Rotba> Rotbas = appDBContext.Rotba.Where(row => row.RotbaType == type).ToList();
            Rotbas = InsertDefault(Rotbas);
            return Rotbas;
        }
        public List<Rotba> GetAllRotbas()
        {
            List<Rotba> Rotbas = appDBContext.Rotba.ToList();
            Rotbas = InsertDefault(Rotbas);
            return Rotbas;
        }
        private List<Rotba> InsertDefault(List<Rotba> Rotbas)
        {
            Rotba defaultRotba = new Rotba { ID = 0, RotbaName = "", RotbaType = 1 };
            Rotbas.Insert(0, defaultRotba);
            return Rotbas;
        }
    }
}