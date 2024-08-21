using ElecWarSystem.Data;
using ElecWarSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElecWarSystem.Serivces
{
    public class GheyabService : IOutdoorService<Gheyab, GheyabDetails>
    {
        private readonly AppDBContext dBContext;
        private readonly FardDetailsService FardDetailsService;
        private readonly TmamService tmamService;
        private readonly FardService FardService;

        public GheyabService()
        {
            dBContext = new AppDBContext();
            FardDetailsService = new FardDetailsService();
            tmamService = new TmamService();
            FardService = new FardService();
        }
        public Gheyab Get(long id)
        {
            Gheyab Gheyab = dBContext.Gheyab
                .FirstOrDefault(row=> row.ID == id);
            return Gheyab;
        }
        public GheyabDetails GetDetail(long id)
        {
            GheyabDetails GheyabDetails = dBContext.GheyabDetails.Find(id);
            return GheyabDetails;
        }
        public List<Gheyab> GetAll(int unitID)
        {
            Tmam tmam = new Tmam() { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.AddTmam(tmam);
            List<Gheyab> Gheyab = dBContext.Gheyab
                .Include("GheyabDetails")
                .Include("GheyabDetails.FardDetails.Rotba")
                .Include("GheyabDetails.commandItem")
                .Where(row => row.TmamID == tmamID).ToList();
            return Gheyab;
        }
        public long GetID(GheyabDetails GheyabDetails)
        {
            GheyabDetails GheyabDetailTemp =
                dBContext.GheyabDetails
                .FirstOrDefault(row => row.DateFrom == GheyabDetails.DateFrom &&
                    row.FardID == GheyabDetails.FardID);
            return GheyabDetailTemp?.ID ?? 0;
        }

        public long AddDetail(GheyabDetails GheyabDetails)
        {
            long id = GetID(GheyabDetails);
            if (id == 0)
            {
                dBContext.GheyabDetails.Add(GheyabDetails);
                dBContext.SaveChanges();
                id = GheyabDetails.ID;
            }
            return id;
        }
        public bool IsDatesLogic(Gheyab Gheyab)
        {
            Tmam tmam = tmamService.GetTmam(Gheyab.TmamID);
            bool result = Gheyab.GheyabDetails.DateFrom <= tmam.Date;
            return result;
        }
        public long Add(Gheyab Gheyab)
        {
            long FardDetailsId = 0;
            if (IsDatesLogic(Gheyab))
            {
                Gheyab.GheyabDetailsID = AddDetail(Gheyab.GheyabDetails);
                FardDetailsId = Gheyab.GheyabDetails.FardID;
                Gheyab.GheyabDetails = null;
                dBContext.Gheyab.Add(Gheyab);
                dBContext.SaveChanges();
                if(FardDetailsService.FardDetailsIsLeader(FardDetailsId) != 0){
                    FardService.setFard(new Fard
                    {
                        FardID = Gheyab.GheyabDetails.FardID,
                        TmamID = Gheyab.TmamID,
                        Status = TmamEnum.Gheyab
                    });
                }
                return Gheyab.ID;
            }
            else
            {
                return -1;
            }
        }
        public int GetCount(long GheyabDetailsID)
        {
            int GheyabCount = dBContext.Gheyab
                .Where(row => row.GheyabDetailsID == GheyabDetailsID)
                .Count();
            return GheyabCount;
        }
        public void Delete(long id)
        {
            Gheyab Gheyab = Get(id);
            long absenceID = Gheyab.GheyabDetailsID;
            GheyabDetails absenceDetail = GetDetail(absenceID);
            long FardDetailsID = absenceDetail.FardID;
            if (GetCount(absenceID) == 1)
            {
                dBContext.GheyabDetails.Remove(absenceDetail);
            }
            else
            {
                dBContext.Gheyab.Remove(Gheyab);
            }
            dBContext.SaveChanges();
            FardService.DeleteFard(Gheyab.TmamID, FardDetailsID);
        }
        public int getTotal(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);
            int? totalGheyabs = 0;
            if (tmamID > 0)
            {
                totalGheyabs = dBContext.TmamDetails.Where(row => row.TmamID == tmamID)?.ToList().Sum(row => row.Gheyab) ?? 0; ;
            }
            return (int)totalGheyabs;
        }
        public int getEntered(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);
            int enteredGheyabs = 0;
            if (tmamID > 0)
            {
                try
                {
                    enteredGheyabs = dBContext.Gheyab.Where(row => row.TmamID == tmamID).Count();
                }
                catch (Exception ex)
                {
                    enteredGheyabs = 0;
                }
            }
            return enteredGheyabs;
        }
    }
}