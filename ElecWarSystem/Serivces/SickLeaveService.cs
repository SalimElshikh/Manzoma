using ElecWarSystem.Data;
using ElecWarSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ElecWarSystem.Serivces
{
    public class MaradyService : IOutdoorService<Marady, MaradyDetails>
    {
        private readonly AppDBContext dBContext;
        private readonly FardDetailsService FardDetailsService;
        private readonly TmamService tmamService;
        private readonly FardService FardService;

        public MaradyService()
        {
            dBContext = new AppDBContext();
            FardDetailsService = new FardDetailsService();
            tmamService = new TmamService();
            FardService = new FardService();

        }
        public Marady Get(long id)
        {
            Marady Marady = dBContext.Marady.Include(row => row.MaradyDetails). 
                FirstOrDefault(row => row.ID == id);
            return Marady;
        }
        public MaradyDetails GetDetail(long id)
        {
            MaradyDetails MaradysDetails = dBContext.MaradyDetails.Find(id);
            return MaradysDetails;
        }
        public int GetCount(long MaradyDetailsID)
        {
            int MaradyCount = dBContext.Marady
                .Where(row => row.MaradyDetailID == MaradyDetailsID)
                .Count();
            return MaradyCount;
        }
        public List<Marady> GetAll(int unitID)
        {
            Tmam tmam = new Tmam() { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.AddTmam(tmam);
            List<Marady> Marady = dBContext.Marady
                .Include("MaradyDetails")
                .Include("MaradyDetails.FardDetails.Rotba")
                .Where(row => row.TmamID == tmamID).ToList();
            return Marady;
        }
        public long GetID(MaradyDetails MaradysDetails)
        {
            MaradyDetails MaradysDetailsTemp =
                dBContext.MaradyDetails
                .FirstOrDefault(row => row.DateFrom == MaradysDetails.DateFrom &&
                    row.DateTo == MaradysDetails.DateTo &&
                    row.FardID == MaradysDetails.FardID);

            return (MaradysDetails != null) ? MaradysDetails.ID : 0;
        }
        public long AddDetail(MaradyDetails MaradysDetails)
        {
            long id = GetID(MaradysDetails);
            if (id == 0)
            {
                dBContext.MaradyDetails.Add(MaradysDetails);
                dBContext.SaveChanges();
                return MaradysDetails.ID;
            }
            else
            {
                MaradyDetails MaradysDetailsTemp = dBContext.MaradyDetails.Find(id);
                MaradysDetailsTemp.Mostashfa = MaradysDetails.Mostashfa;
                MaradysDetailsTemp.Hala = MaradysDetails.Hala;
                MaradysDetailsTemp.MostashfaDate = MaradysDetails.MostashfaDate;
                dBContext.SaveChanges();
                return id;
            }
        }

        public long Add(Marady Marady)
        {
            Marady.Tmam = tmamService.GetTmam(Marady.TmamID);
            if (Marady.IsDateLogic())
            {
                FardService.setFard(new Fard
                {
                    FardID = Marady.MaradyDetails.FardID,
                    TmamID = Marady.TmamID,
                    Status = TmamEnum.Marady
                });
                Marady.CleanNav();
                dBContext.Marady.Add(Marady);
                dBContext.SaveChanges();
                return Marady.ID;
            }
            else
            {
                return -1;
            }
        }
        public void Delete(long id)
        {
            Marady Marady = Get(id);
            FardService.DeleteFard(Marady.TmamID, Marady.MaradyDetails.FardID);
            Marady.MaradyDetails = null;
            dBContext.Marady.Remove(Marady);
            dBContext.SaveChanges();

        }
        public int getTotal(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);

            int totalMaradys = 0;
            if (tmamID > 0)
            {
                try
                {
                    totalMaradys = dBContext.TmamDetails.Where(row => row.TmamID == tmamID)?.ToList().Sum(row => row.Marady) ?? 0;
                }
                catch (Exception ex)
                {
                    totalMaradys = 0;
                }
            }
            return totalMaradys;
        }
        public int getEntered(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);
            int enteredMaradys = 0;
            if (tmamID > 0)
            {
                try
                {
                    enteredMaradys = dBContext.Marady.Where(row => row.TmamID == tmamID).Count();
                }
                catch (Exception ex)
                {
                    enteredMaradys = 0;
                }
            }
            return enteredMaradys;
        }
    }
}