using ElecWarSystem.Data;
using ElecWarSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElecWarSystem.Serivces
{
    public class MostashfaService : IOutdoorService<Mostashfa, MostashfaDetails>
    {
        private readonly AppDBContext dBContext;
        private readonly FardDetailsService FardDetailsService;
        private readonly TmamService tmamService;
        private readonly FardService FardService;
        public MostashfaService()
        {
            dBContext = new AppDBContext();
            FardDetailsService = new FardDetailsService();
            tmamService = new TmamService();
            FardService = new FardService();
        }
        public Mostashfa Get(long id)
        {
            Mostashfa Mostashfa = dBContext.Mostashfa.FirstOrDefault(row => row.ID == id);
            return Mostashfa;
        }
        public MostashfaDetails GetDetail(long id)
        {
            MostashfaDetails MostashfasDetails = dBContext.MostashfaDetails.Find(id);
            return MostashfasDetails;
        }
        public int GetCount(long MostashfaDetailsID)
        {
            int MostashfaCount = dBContext.Mostashfa
                .Where(row => row.MostashfaDetailID == MostashfaDetailsID)
                .Count();
            return MostashfaCount;
        }
        public List<Mostashfa> GetAll(int unitID)
        {
            Tmam tmam = new Tmam() { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.AddTmam(tmam);
            List<Mostashfa> Mostashfa = dBContext.Mostashfa
                .Include("MostashfaDetails")
                .Include("MostashfaDetails.FardDetails.Rotba")
                .Where(row => row.TmamID == tmamID).ToList();
            return Mostashfa;
        }
        public long GetID(MostashfaDetails MostashfasDetails)
        {
            MostashfaDetails MostashfasDetailsTemp =
                dBContext.MostashfaDetails
                .FirstOrDefault(row => row.FardID == MostashfasDetails.FardID && row.DateFrom == MostashfasDetails.DateFrom);

            return MostashfasDetailsTemp?.ID ?? 0;
        }
        public long AddDetail(MostashfaDetails MostashfasDetails)
        {
            long id = GetID(MostashfasDetails);
            if (id == 0)
            {
                dBContext.MostashfaDetails.Add(MostashfasDetails);
                dBContext.SaveChanges();
                return MostashfasDetails.ID;
            }
            else
            {
                return id;
            }
        }
        public long Add(Mostashfa Mostashfa)
        {
            if (Mostashfa.IsDateLogic())
            {
                Mostashfa.MostashfaDetailID = AddDetail(Mostashfa.MostashfaDetails);
                long FardDetailsID = Mostashfa.MostashfaDetails.FardID;
                Mostashfa.CleanNav();
                dBContext.Mostashfa.Add(Mostashfa);
                dBContext.SaveChanges();
                if (FardDetailsService.FardDetailsIsLeader(FardDetailsID) != 0)
                {
                    FardService.setFard(new Fard
                    {
                        FardID = FardDetailsID,
                        TmamID = Mostashfa.TmamID,
                        Status = TmamEnum.Mostashfa
                    });
                }
                    
                return Mostashfa.ID;
            }
            else
            {
                return -1;
            }
        }
        public void Delete(long id)
        {
            Mostashfa Mostashfa = Get(id);
            long MostashfaDetailsID = Mostashfa.MostashfaDetailID;
            MostashfaDetails MostashfasDetails = GetDetail(MostashfaDetailsID);
            long FardDetailsID = MostashfasDetails.FardID;
            if (GetCount(MostashfaDetailsID) == 1)
            {
                dBContext.MostashfaDetails.Remove(MostashfasDetails);
            }
            else
            {
                dBContext.Mostashfa.Remove(Mostashfa);
            }
            dBContext.SaveChanges();
            FardService.DeleteFard(Mostashfa.TmamID, FardDetailsID);
        }
        public int getTotal(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);

            int totalMostashfas = 0;
            if (tmamID > 0)
            {
                totalMostashfas = dBContext.TmamDetails.Where(row => row.TmamID == tmamID)?.ToList().Sum(row => row.Mostashfa) ?? 0;
            }
            return totalMostashfas;
        }//
        public int getEntered(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);
            int enteredMostashfas = 0;
            if (tmamID > 0)
            {
                try
                {
                    enteredMostashfas = dBContext.Mostashfa.Where(row => row.TmamID == tmamID).Count();
                }
                catch (Exception ex)
                {
                    enteredMostashfas = 0;
                }
            }
            return enteredMostashfas;
        }
    }
}