using ElecWarSystem.Data;
using ElecWarSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.UI.WebControls;

namespace ElecWarSystem.Serivces
{
    public class Ma2moreyasService : IOutdoorService<Ma2moreya, Ma2moreyaDetails>
    {
        private readonly AppDBContext dBContext;
        private readonly FardDetailsService FardDetailsService;
        private readonly TmamService tmamService;
        private readonly FardService FardService;
        public Ma2moreyasService()
        {
            dBContext = new AppDBContext();
            FardDetailsService = new FardDetailsService();
            tmamService = new TmamService();
            FardService = new FardService();
        }
        public Ma2moreya Get(long id)
        {
            Ma2moreya Ma2moreya = dBContext.Ma2moreya.FirstOrDefault(row =>row.ID == id);
            return Ma2moreya;
        }
        public Ma2moreyaDetails GetDetail(long id)
        {
            Ma2moreyaDetails Ma2moreyaDetail = dBContext.Ma2moreyaDetails.Find(id);
            return Ma2moreyaDetail;
        }
        public List<Ma2moreya> GetAll(int unitID)
        {
            Tmam tmam = new Tmam() { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.AddTmam(tmam);
            List<Ma2moreya> Ma2moreyas = dBContext.Ma2moreya
                .Include("Ma2moreyaDetails")
                .Include("Ma2moreyaDetails.FardDetails.Rotba")
                .Where(row => row.TmamID == tmamID).ToList();
            return Ma2moreyas;
        }
        public long GetID(Ma2moreyaDetails Ma2moreyaDetail)
        {
            Ma2moreyaDetails Ma2moreyaDetailTemp =
                dBContext.Ma2moreyaDetails
                .FirstOrDefault(row => row.DateFrom == Ma2moreyaDetail.DateFrom &&
                    row.DateTo == Ma2moreyaDetail.DateTo &&
                    row.FardID == Ma2moreyaDetail.FardID);

            return Ma2moreyaDetailTemp?.ID ?? 0;
        }

        public long AddDetail(Ma2moreyaDetails Ma2moreyaDetail)
        {
            long id = GetID(Ma2moreyaDetail);

            if (id == 0)
            {
                dBContext.Ma2moreyaDetails.Add(Ma2moreyaDetail);
                id = Ma2moreyaDetail.ID;
            }
            else
            {
                Ma2moreyaDetails Ma2moreyaDetail1 = GetDetail(id);
                Ma2moreyaDetail1.Ma2moreyaPlace = Ma2moreyaDetail.Ma2moreyaPlace;
                Ma2moreyaDetail1.Ma2moreyaCommandor = Ma2moreyaDetail.Ma2moreyaCommandor;
            }
            dBContext.SaveChanges();
            return id;
        }
        public bool IsDatesLogic(Ma2moreya Ma2moreya)
        {
            Tmam tmam = tmamService.GetTmam(Ma2moreya.TmamID);
            bool result = Ma2moreya.Ma2moreyaDetails.DateFrom <= tmam.Date &&
                Ma2moreya.Ma2moreyaDetails.DateTo > tmam.Date;
            return result;
        }
        public long Add(Ma2moreya Ma2moreya)
        {
            
            if (IsDatesLogic(Ma2moreya))
            {
                Ma2moreya.Ma2moreyaDetailID = AddDetail(Ma2moreya.Ma2moreyaDetails);
                long FardDetailsID = Ma2moreya.Ma2moreyaDetails.FardID;
                if(Ma2moreya.Ma2moreyaDetailID != 0)
                {
                    Ma2moreya.Ma2moreyaDetails = null;
                }
                dBContext.Ma2moreya.Add(Ma2moreya);
                dBContext.SaveChanges();
                if (FardDetailsService.FardDetailsIsLeader(FardDetailsID) != 0)
                {
                    FardService.setFard(new Fard
                    {
                        FardID = FardDetailsID,
                        TmamID = Ma2moreya.TmamID,
                        Status = TmamEnum.Ma2moreya
                    });
                }
                return Ma2moreya.ID;
            }
            else
            {
                return -1;
            }
        }
        public int GetCount(long Ma2moreyaDetailID)
        {
            int Ma2moreyaCount = dBContext.Ma2moreya
                .Where(row => row.Ma2moreyaDetailID == Ma2moreyaDetailID)
                .Count();
            return Ma2moreyaCount;
        }
        public void Delete(long id)
        {
            Ma2moreya Ma2moreya = Get(id);
            long Ma2moreyaID = Ma2moreya.Ma2moreyaDetailID;
            Ma2moreyaDetails Ma2moreyaDetail = GetDetail(Ma2moreyaID);
            if (GetCount(Ma2moreyaID) == 1)
            {
                dBContext.Ma2moreyaDetails.Remove(Ma2moreyaDetail);
            }
            else
            {
                dBContext.Ma2moreya.Remove(Ma2moreya);
            }
            dBContext.SaveChanges();
            FardService.DeleteFard(Ma2moreya.TmamID, Ma2moreya.Ma2moreyaDetails.FardID);
        }
        public int getTotal(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);
            int totalMa2moreyas = 0;
            if (tmamID > 0)
            {
                totalMa2moreyas = dBContext.TmamDetails.Where(row => row.TmamID == tmamID)?.ToList().Sum(row => row.Ma2moreya) ?? 0;
            }
            return totalMa2moreyas;
        }
        public int getEntered(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);
            int enteredMa2moreyas = 0;
            if (tmamID > 0)
            {
                try
                {
                    enteredMa2moreyas = dBContext.Ma2moreya.Where(row => row.TmamID == tmamID).Count();
                }
                catch (Exception ex)
                {
                    enteredMa2moreyas = 0;
                }
            }
            return enteredMa2moreyas;
        }
    }
}