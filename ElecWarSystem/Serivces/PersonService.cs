using ElecWarSystem.Data;
using ElecWarSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElecWarSystem.Serivces
{
    public class FardDetailsService : IDBRepository<FardDetails>
    {
        private AppDBContext dBContext;
        
        
        
        
        
        public FardDetailsService()
        {
            dBContext = new AppDBContext();
        }
        public List<FardDetails> GetAllFardDetails(int unitID)
        {
            List<FardDetails> FardDetailss = dBContext.FardDetails
                .Where(row => row.We7daID == unitID)
                .ToList();
            return FardDetailss;
        }
        public List<FardDetails> GetFardDetailss(int unitID, int type)
        {
            List<FardDetails> FardDetailss = dBContext.FardDetails
                .Include("Rotba")
                .Where(row => row.We7daID == unitID && row.Rotba.RotbaType == type && !row.Status)
                .OrderBy(row => row.RotbaID).ThenBy(row => row.FullName)
                .ToList();
            return FardDetailss;
        }
        public int GetFardDetailssCount(int unitID, int type)
        {
            int count = dBContext.FardDetails.Where(row => row.We7daID == unitID && row.Rotba.RotbaType == type).Count();
            return count;
        }

        public FardDetails Find(long? id)
        {
            FardDetails FardDetails = dBContext.FardDetails.FirstOrDefault(row => row.ID == id);
            return FardDetails;
        }
        public long? Add(FardDetails FardDetails)
        {
            dBContext.FardDetails.Add(FardDetails);
            dBContext.SaveChanges();
            return FardDetails.ID;
        }
        public List<FardDetails> GetFardDetailsOfRotba(int userId, int RotbaId)
        {

            List<FardDetails> FardDetailss = dBContext.FardDetails
                .Where(row => row.RotbaID == RotbaId && row.We7daID == userId)
                .OrderBy(row => row.FullName)
                .ToList();
            return FardDetailss;
        }
        public void Update(long? id, FardDetails FardDetails)
        {
            FardDetails FardDetailsTemp = dBContext.FardDetails.FirstOrDefault(row => row.ID == id);
            FardDetailsTemp.RotbaID = FardDetails.RotbaID;
            FardDetailsTemp.FullName = FardDetails.FullName;
            FardDetailsTemp.Raqam3askary = FardDetails.Raqam3askary;
            FardDetailsTemp.OnDuty = FardDetails.OnDuty;
            dBContext.SaveChanges();
        }

        public bool Delete(long? id)
        {
            FardDetails FardDetails = dBContext.FardDetails.FirstOrDefault(row => row.ID == id);
            int result = this.FardDetailsIsLeader(FardDetails.ID);
            if (result == 0)
            {
                List<Tmam> tmams = dBContext.Tmams.Where(row => row.Qa2edManoobID == id && row.Date < DateTime.Today ).ToList();
                dBContext.Tmams.RemoveRange(tmams);
                dBContext.SaveChanges();
                dBContext.FardDetails.Remove(FardDetails);
                dBContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public int FardDetailsIsLeader(long id)
        {
            int result = 0;
            We7daRa2eeseya unit = dBContext.FardDetails.Include("We7daRa2eeseya.We7daFar3eya").FirstOrDefault(row => row.ID == id).We7daRa2eeseya;
            if (id == unit.Qa2edWe7daID)
                result = 1;
            else if (id == unit.Ra2ees3ameleyatID)
                result = 2;
            else
            {
                for (int i = 0; i < unit.We7daFar3eya.Count; i++)
                {
                    if (id == unit.We7daFar3eya[i].Qa2edWe7daID)
                    {
                        result = i + 3;
                        break;
                    }
                    else if (id == unit.We7daFar3eya[i].Ra2ees3ameleyatID)
                    {
                        result = i + 4;
                        break;
                    }
                }
            }
            return result;
        }

        void IDBRepository<FardDetails>.Delete(long? id)
        {
            throw new NotImplementedException();
        }
        public void ResetFardDetailssStatus(int unitId)
        {
            List<FardDetails> people = GetAllFardDetails(unitId);
            foreach (FardDetails FardDetails in people)
            {
                FardDetails.Status = false;
            }
            dBContext.SaveChanges();
        }
        public void setStatus(long FardDetailsID)
        {
            FardDetails FardDetails = dBContext.FardDetails.Find(FardDetailsID);
            FardDetails.Status = true;
            dBContext.SaveChanges();
        }
        public void unCheckStatus(long FardDetailsID)
        {
            FardDetails FardDetails = dBContext.FardDetails.Find(FardDetailsID);
            FardDetails.Status = false;
            dBContext.SaveChanges();
        }
    }
}