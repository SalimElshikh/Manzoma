using ElecWarSystem.Data;
using ElecWarSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ElecWarSystem.Serivces
{
    public class StorageManager
    {
        private readonly AppDBContext appDBContext;
        public StorageManager()
        {
            appDBContext = new AppDBContext();
        }
        public Space getCapacityPerUnit(int userId)
        {
            long capacityPerUnit = appDBContext.We7daRa2eeseya.Find(userId).StorageSize;
            return new Space(capacityPerUnit);
        }
        public Space getUsedPerUnit(int userId)
        {
            long usedPerUnitActual = 0; 
            IQueryable<Email> emails = appDBContext.Emails.Include("Document").Where(row => row.SenderUserID == userId);
            foreach(Email email in emails)
            {
                foreach (Document document in email.Document)
                {
                    if (File.Exists(document.FilePath))
                    {
                        usedPerUnitActual += new FileInfo(document.FilePath).Length;
                    }
                }
            }
            long usedPerUnitExpected = appDBContext.We7daRa2eeseya.Find(userId).UsedStorage;
            if (usedPerUnitActual != usedPerUnitExpected) 
            {
                appDBContext.We7daRa2eeseya.Find(userId).UsedStorage = usedPerUnitActual;
                appDBContext.SaveChanges();
                usedPerUnitExpected = usedPerUnitActual;
            }
            return new Space(usedPerUnitExpected);
        }
        public bool increaseUsed(int unitId, long space)
        {
            long used = getUsedPerUnit(unitId).SpaceInBytes;
            long capacity = getCapacityPerUnit(unitId).SpaceInBytes;
            long avaliable = capacity - used;
            if(space > avaliable)
            {
                return false;
            }
            else
            {
                appDBContext.We7daRa2eeseya.Find(unitId).UsedStorage += space;
                appDBContext.SaveChanges();
                return true;
            }
        }
        public bool decreaseUsed(int unitId, long space)
        {
            long used = getUsedPerUnit(unitId).SpaceInBytes;
            
            if (space > used)
            {
                return false;
            }
            else
            {
                appDBContext.We7daRa2eeseya.Find(unitId).UsedStorage -= space;
                appDBContext.SaveChanges();
                return true;
            }
        }
    }
}