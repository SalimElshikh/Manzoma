using ElecWarSystem.Data;
using ElecWarSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElecWarSystem.Serivces
{
    public class Mo3askrService : IOutdoorService<Mo3askr, Mo3askrDetails>
    {
        private readonly AppDBContext dBContext;
        private readonly FardDetailsService FardDetailsService;
        private readonly TmamService tmamService;
        private readonly FardService FardService ;
        public Mo3askrService()
        {
            dBContext = new AppDBContext();
            FardDetailsService = new FardDetailsService();
            tmamService = new TmamService();
            FardService = new FardService();
        }
        public Mo3askr Get(long id)
        {
            Mo3askr Mo3askr = dBContext.Mo3askr.FirstOrDefault(row => row.ID ==id);
            return Mo3askr;
        }
        public Mo3askrDetails GetDetail(long id)
        {
            Mo3askrDetails Mo3askrDetails = dBContext.Mo3askrDetails.Find(id);
            return Mo3askrDetails;
        }
        public int GetCount(long Mo3askrDetailsID)
        {
            int Mo3askrCount = dBContext.Mo3askr
                .Where(row => row.Mo3askrDetailsID == Mo3askrDetailsID)
                .Count();
            return Mo3askrCount;
        }
        public List<Mo3askr> GetAll(int unitID)
        {
            Tmam tmam = new Tmam() { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.AddTmam(tmam);
            List<Mo3askr> Mo3askr = dBContext.Mo3askr
                .Include("Mo3askrDetails")
                .Include("Mo3askrDetails.FardDetails.Rotba")
                .Where(row => row.TmamID == tmamID).ToList();
            return Mo3askr;
        }
        public long GetID(Mo3askrDetails Mo3askrDetails)
        {
            Mo3askrDetails Mo3askrDetailsTemp =
                dBContext.Mo3askrDetails
                .FirstOrDefault(row => row.DateFrom == Mo3askrDetails.DateFrom &&
                    row.DateTo == Mo3askrDetails.DateTo &&
                    row.FardID == Mo3askrDetails.FardID);

            return (Mo3askrDetails != null) ? Mo3askrDetails.ID : 0;
        }
        public long AddDetail(Mo3askrDetails Mo3askrDetails)
        {
            long id = GetID(Mo3askrDetails);
            if (id == 0)
            {
                dBContext.Mo3askrDetails.Add(Mo3askrDetails);
                dBContext.SaveChanges();
                return Mo3askrDetails.ID;
            }
            else
            {
                return id;
            }
        }
        public long Add(Mo3askr Mo3askr)
        {
            Mo3askr.Tmam = tmamService.GetTmam(Mo3askr.TmamID);
            if (Mo3askr.IsDateLogic())
            {
                Mo3askr.Mo3askrDetailsID = AddDetail(Mo3askr.Mo3askrDetails);
                long FardDetailsID = Mo3askr.Mo3askrDetails.FardID;
                Mo3askr.CleanNav();
                dBContext.Mo3askr.Add(Mo3askr);
                dBContext.SaveChanges();
                
                if (FardDetailsService.FardDetailsIsLeader(FardDetailsID) != 0)
                {
                    FardService.setFard(new Fard
                    {
                        FardID = FardDetailsID,
                        TmamID = Mo3askr.TmamID,
                        Status = TmamEnum.Mo3askr
                    });
                }
                return Mo3askr.ID;
            }
            else
            {
                return -1;
            }
        }
        public void Delete(long id)
        {
            Mo3askr Mo3askr = Get(id);
            long Mo3askrDetailsID = Mo3askr.Mo3askrDetailsID;
            Mo3askrDetails Mo3askrDetails = GetDetail(Mo3askrDetailsID);
            long FardDetailsID = Mo3askrDetails.FardID;
            if (GetCount(Mo3askrDetailsID) == 1)
            {
                DeleteDetails(Mo3askrDetails);
            }
            else
            {
                dBContext.Mo3askr.Remove(Mo3askr);
            }
            FardService.DeleteFard(Mo3askr.TmamID, FardDetailsID);
            dBContext.SaveChanges();
        }
        public void DeleteDetails(Mo3askrDetails Mo3askrDetails)
        {
            dBContext.Mo3askrDetails.Remove(Mo3askrDetails);
            dBContext.SaveChanges();
        }
        public int getTotal(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);

            int totalMo3askrs = 0;
            if (tmamID > 0)
            {
                try
                {
                    totalMo3askrs = dBContext.TmamDetails.Where(row => row.TmamID == tmamID)?.ToList().Sum(row => row.Mo3askar) ?? 0;
                }
                catch (Exception ex)
                {
                    totalMo3askrs = 0;
                }
            }
            return totalMo3askrs;
        }
        public int getEntered(int unitID)
        {
            Tmam tmam = new Tmam { We7daID = unitID, Date = DateTime.Today.AddDays(1) };
            long tmamID = tmamService.GetTmamID(tmam);
            int enteredMo3askrs = 0;
            if (tmamID > 0)
            {
                try
                {
                    enteredMo3askrs = dBContext.Mo3askr.Where(row => row.TmamID == tmamID).Count();
                }
                catch (Exception ex)
                {
                    enteredMo3askrs = 0;
                }
            }
            return enteredMo3askrs;
        }
    }
}