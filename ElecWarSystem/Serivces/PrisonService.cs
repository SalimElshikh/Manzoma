using ElecWarSystem.Data;
using ElecWarSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElecWarSystem.Serivces
{
    public class SegnService : IOutdoorService<Segn, SegnDetails>
    {
        private readonly AppDBContext dBContext;
        private readonly FardService FardService;
        private readonly TmamService tmamService;
        public SegnService()
        {
            dBContext = new AppDBContext();
            FardService = new FardService();
            tmamService = new TmamService();
        }
        public Segn Get(long id)
        {
            Segn Segn = dBContext.Segn
                .FirstOrDefault(row => row.ID == id);
            return Segn;
        }
        public SegnDetails GetDetail(long id)
        {
            SegnDetails SegnDetails = dBContext.SegnDetails.Find(id);
            return SegnDetails;
        }
        public List<Segn> GetAll(int unitID)
        {
            Tmam tmam = new Tmam() { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.AddTmam(tmam);
            List<Segn> Segn = dBContext.Segn
                .Include("SegnDetails")
                .Include("SegnDetails.FardDetails.Rotba")
                .Include("SegnDetails.CommandItem")
                .Where(row => row.TmamID == tmamID).ToList();
            return Segn;
        }
        public long GetID(SegnDetails SegnDetails)
        {
            SegnDetails SegnDetailTemp =
                dBContext.SegnDetails
                .FirstOrDefault(row => row.DateFrom == SegnDetails.DateFrom &&
                    row.DateTo == SegnDetails.DateTo &&
                    row.FardID == SegnDetails.FardID);
            long SegnID = 0;
            if (SegnDetailTemp != null)
            {
                SegnID = SegnDetailTemp.ID;
            }
            return SegnID;
        }

        public long AddDetail(SegnDetails SegnDetails)
        {
            long id = GetID(SegnDetails);

            if (id == 0)
            {
                dBContext.SegnDetails.Add(SegnDetails);
                dBContext.SaveChanges();
                id = SegnDetails.ID;
            }
            return id;
        }
        public bool IsDatesLogic(Segn Segn)
        {
            Tmam tmam = tmamService.GetTmam(Segn.TmamID);
            bool result = Segn.SegnDetails.DateFrom <= tmam.Date &&
                Segn.SegnDetails.DateTo > tmam.Date;
            return result;
        }
        public long Add(Segn Segn)
        {
            if (IsDatesLogic(Segn))
            {
                Segn.SegnDetailID = AddDetail(Segn.SegnDetails);
                long FardDetailsID = Segn.SegnDetails.FardID;
                Segn.SegnDetails = null;
                Segn.Tmam = null;
                dBContext.Segn.Add(Segn);
                dBContext.SaveChanges();
                FardService.setFard(new Fard
                {
                    FardID = FardDetailsID,
                    TmamID = Segn.TmamID,
                    Status = TmamEnum.Segn
                });
                return Segn.ID;
            }
            else
            {
                return -1;
            }
        }
        public int GetCount(long SegnDetailID)
        {
            int SegnCount = dBContext.Segn
                .Where(row => row.SegnDetailID == SegnDetailID)
                .Count();
            return SegnCount;
        }
        public void Delete(long id)
        {
            Segn Segn = Get(id);
            dBContext.Segn.Remove(Segn);
            dBContext.SaveChanges();
            SegnDetails SegnDetails = GetDetail(Segn.SegnDetailID);
            if (GetCount(Segn.SegnDetailID) == 1)
            {
                dBContext.SegnDetails.Remove(SegnDetails);
                long commandItemID = SegnDetails.CommandItemID;
                CommandItems commandItem = dBContext.CommandItems.Find(commandItemID);
                dBContext.CommandItems.Remove(commandItem);
            }
            FardService.DeleteFard(Segn.TmamID, Segn.SegnDetails.FardID);
        }
        public int getTotal(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);
            int totalSegns = 0;
            if (tmamID > 0)
            {
                totalSegns = dBContext.TmamDetails.Where(row => row.TmamID == tmamID)?.ToList().Sum(row => row.Segn) ?? 0;               
            }
            return totalSegns;
        }
        public int getEntered(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);
            int enteredSegns = 0;
            if (tmamID > 0)
            {
                try
                {
                    enteredSegns = dBContext.Segn.Where(row => row.TmamID == tmamID).Count();
                }
                catch (Exception ex)
                {
                    enteredSegns = 0;
                }
            }
            return enteredSegns;
        }

    }
}