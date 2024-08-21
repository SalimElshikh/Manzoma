using ElecWarSystem.Data;
using ElecWarSystem.Models;
using ElecWarSystem.Models.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElecWarSystem.Serivces
{
    public class KharegBeladService : IOutdoorService<KharegBelad, KharegBeladDetails>
    {
        private readonly AppDBContext dBContext;
        private readonly FardService FardService;
        private readonly TmamService tmamService;
        public KharegBeladService()
        {
            dBContext = new AppDBContext();
            FardService = new FardService();
            tmamService = new TmamService();
        }
        public KharegBelad Get(long id)
        {
            KharegBelad KharegBelad = dBContext.KharegBelad.Include("KharegBeladDetails")
                .FirstOrDefault(row => row.ID == id);
            return KharegBelad;
        }
        public KharegBeladDetails GetDetail(long id)
        {
            KharegBeladDetails KharegBeladDetail = dBContext.KharegBeladDetails.Find(id);
            return KharegBeladDetail;
        }
        public int GetCount(long KharegBeladDetailsID)
        {
            int KharegBeladCount = dBContext.KharegBelad
                .Where(row => row.KharegBeladDetailID == KharegBeladDetailsID)
                .Count();
            return KharegBeladCount;
        }
        public List<KharegBelad> GetAll(int unitID)
        {
            Tmam tmam = new Tmam() { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.AddTmam(tmam);
            List<KharegBelad> outOfCountries = dBContext.KharegBelad
                .Include("KharegBeladDetails")
                .Include("KharegBeladDetails.FardDetails.Rotba")
                .Where(row => row.TmamID == tmamID).ToList();
            return outOfCountries;
        }
        public long GetID(KharegBeladDetails KharegBeladDetail)
        {
            KharegBeladDetails KharegBeladDetailsTemp =
                dBContext.KharegBeladDetails
                .FirstOrDefault(row => row.DateFrom == KharegBeladDetail.DateFrom &&
                    row.DateTo == KharegBeladDetail.DateTo &&
                    row.FardID == KharegBeladDetail.FardID);

            return (KharegBeladDetail != null) ? KharegBeladDetail.ID : 0;
        }
        public long AddDetail(KharegBeladDetails KharegBeladDetails)
        {
            long id = GetID(KharegBeladDetails);
            if (id == 0)
            {
                dBContext.KharegBeladDetails.Add(KharegBeladDetails);
                dBContext.SaveChanges();
                return KharegBeladDetails.ID;
            }
            else
            {
                return id;
            }
        }
        public long Add(KharegBelad KharegBelad)
        {
            KharegBelad.Tmam = tmamService.GetTmam(KharegBelad.TmamID);
            if (KharegBelad.IsDateLogic())
            {
                FardService.setFard(new Fard
                {
                    FardID = KharegBelad.KharegBeladDetails.FardID,
                    TmamID = KharegBelad.TmamID,
                    Status = TmamEnum.KharegBelad
                });
                KharegBelad.CleanNav();
                dBContext.KharegBelad.Add(KharegBelad);
                dBContext.SaveChanges();
                return KharegBelad.ID;
            }
            else
            {
                return -1;
            }
        }
        public void Delete(long id)
        {
            KharegBelad KharegBelad = Get(id);
            FardService.DeleteFard(
                KharegBelad.TmamID,
                KharegBelad.KharegBeladDetails.FardID);
            dBContext.KharegBelad.Remove(KharegBelad);
            dBContext.SaveChanges();
        }
        public int getTotal(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);

            int totalKharegBelads = 0;
            if (tmamID > 0)
            {
                try
                {
                    totalKharegBelads = dBContext.TmamDetails.Where(row => row.TmamID == tmamID)?.ToList().Sum(row => row.KharegBelad) ?? 0;
                }
                catch (Exception ex)
                {
                    totalKharegBelads = 0;
                }
            }
            return totalKharegBelads;
        }
        public int getEntered(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);
            int enteredKharegBelads = 0;
            if (tmamID > 0)
            {
                try
                {
                    enteredKharegBelads = dBContext.KharegBelad.Where(row => row.TmamID == tmamID).Count();
                }
                catch (Exception ex)
                {
                    enteredKharegBelads = 0;
                }
            }
            return enteredKharegBelads;
        }
    }
}