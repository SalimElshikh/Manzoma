using ElecWarSystem.Data;
using ElecWarSystem.Models;
using ElecWarSystem.Models.OutDoor.OutDoorNew;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElecWarSystem.Serivces
{
    public class KharegTmarkozService
    {
        private AppDBContext dBContext;
        public KharegTmarkozService()
        {
            dBContext = new AppDBContext();
        }
        public List<FardDetails> GetFardDetailss(int unitID, int type)
        {
            List<FardDetails> FardDetailss = dBContext.FardDetails
                .Include("RotbaID")
                .Where(row => row.We7daID == unitID && row.Rotba.RotbaType == type && !row.Status)
                .OrderBy(row => row.RotbaID).ThenBy(row => row.FullName)
                .ToList();
            return FardDetailss;
        }
        //public List<A8radTa7arok> GetGharad(int gharadID)
        //{
        //    List<A8radTa7arok> Gharad = dBContext.A8radTa7arok.Where(row => row.ID == gharadID).OrderBy(row => row.ID).ToList();
        //    return Gharad;
        //}
        //public List<Models.Mo3edat> GetMo3edat(int mo3edatID)
        //{
        //    List<Mo3edat> Mo3edat = dBContext.Mo3edats.Where(row => row.ID == mo3edatID).OrderBy(row => row.ID).ToList();
        //    return Mo3edat;
        //}
        //public List<Markbat> GetMarkabat(int ID)
        //{
        //    List<Markbat> markbat = dBContext.Markbats.Where(row => row.ID == ID).OrderBy(row => row.ID).ToList();
        //    return markbat;
        //}
        //public List<Asl7a> GetAsl7a(int ID)
        //{
        //    List<Asl7a> asl7a = dBContext.Asl7as.Where(row => row.ID == ID).OrderBy(row => row.ID).ToList();
        //    return asl7a;
        //}
        //public List<Za5ira> GetZa5ira(int ID)
        //{
        //    List<Za5ira> za5ira = dBContext.Za5iras.Where(row => row.ID == ID).OrderBy(row => row.ID).ToList();
        //    return za5ira;
        //}
        //public List<Rotba> GetRotbas(int ID)
        //{
        //    List<Rotba> rotba = dBContext.Rotba.Where(row => row.ID == ID).OrderBy(row => row.ID).ToList();
        //    return rotba;
        //}
    }
}