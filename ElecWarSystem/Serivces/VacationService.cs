using ElecWarSystem.Data;
using ElecWarSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElecWarSystem.Serivces
{
    public class AgazaService
    {
        private readonly AppDBContext dBContext;
        private readonly TmamService tmamService;
        private readonly FardService FardService;
        private readonly FardDetailsService FardDetailsService;
        public AgazaService()
        {
            dBContext = new AppDBContext();
            tmamService = new TmamService();
            FardService = new FardService();
            FardDetailsService = new FardDetailsService();
        }
        public Agaza Get(long id)
        {
            Agaza Agaza = dBContext.Agaza.FirstOrDefault(row => row.ID == id);
            return Agaza;
        }
        public AgazaDetails GetDetail(long id)
        {
            AgazaDetails AgazaDetails = dBContext.AgazaDetails.Find(id);
            return AgazaDetails;
        }
        public List<Agaza> GetAll(int unitID)
        {
            Tmam tmam = new Tmam() { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.AddTmam(tmam);
            List<Agaza> Agaza = dBContext.Agaza
                .Include("AgazaDetails")
                .Include("AgazaDetails.FardDetails.Rotba")
                .Where(row => row.TmamID == tmamID).ToList();
            return Agaza;
        }
        public long GetID(AgazaDetails AgazaDetails)
        {
            AgazaDetails AgazaDetailTemp =
                dBContext.AgazaDetails
                .FirstOrDefault(row =>
                    row.DateFrom.Equals(AgazaDetails.DateFrom.Date) &&
                    row.DateTo.Equals(AgazaDetails.DateTo.Date) &&
                    row.FardID == AgazaDetails.FardID);


            long AgazaID = 0;

            if (AgazaDetailTemp != null)
            {
                AgazaID = AgazaDetailTemp.ID;
            }

            return AgazaID;
        }

        public long AddDetail(AgazaDetails AgazaDetails)
        {
            long id = GetID(AgazaDetails);

            if (id == 0)
            {
                dBContext.AgazaDetails.Add(AgazaDetails);
                dBContext.SaveChanges();
                id = AgazaDetails.ID;
            }
            return id;
        }
        public long Add(Agaza Agaza)
        {
            Agaza.Tmam = tmamService.GetTmam(Agaza.TmamID);
            if (Agaza.IsDateLogic())
            {
                if (FardDetailsService.FardDetailsIsLeader(Agaza.AgazaDetails.FardID) != 0)
                {
                    FardService.setFard(new Fard
                    {
                        FardID = Agaza.AgazaDetails.FardID,
                        TmamID = Agaza.TmamID,
                        Status = TmamEnum.Agaza
                    });
                }
                
                Agaza.CleanNav();
                dBContext.Agaza.Add(Agaza);
                dBContext.SaveChanges();
                return Agaza.ID;
            }
            else
            {
                return -1;
            }
        }
        public int GetCount(long AgazaDetailID)
        {
            int AgazaCount = dBContext.Agaza
                .Where(row => row.AgazaDetailID == AgazaDetailID)
                .Count();
            return AgazaCount;
        }
        public void Delete(long id)
        {
            Agaza Agaza = Get(id);
            long AgazaID = Agaza.AgazaDetailID;
            AgazaDetails AgazaDetails = GetDetail(AgazaID);
            FardService.DeleteFard(Agaza.TmamID, Agaza.AgazaDetails.FardID);
            if (GetCount(AgazaID) == 1)
            {
                dBContext.AgazaDetails.Remove(AgazaDetails);
            }
            else
            {
                dBContext.Agaza.Remove(Agaza);
            }
            dBContext.SaveChanges();
            
        }

        public int getTotal(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);
            int totalAgazas = 0;
            if (tmamID > 0)
            {
                totalAgazas = dBContext.TmamDetails.Where(row => row.TmamID == tmamID)?.ToList().Sum(row => row.Agaza) ?? 0;
            }
            return totalAgazas;
        }
        public int getEntered(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);
            int enteredAgazas = 0;
            if (tmamID > 0)
            {
                try
                {
                    enteredAgazas = dBContext.Agaza.Where(row => row.TmamID == tmamID).Count();
                }
                catch (Exception ex)
                {
                    enteredAgazas = 0;
                }
            }
            return enteredAgazas;
        }
    }
}