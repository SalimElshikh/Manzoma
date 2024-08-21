using ElecWarSystem.Data;
using ElecWarSystem.Models;

namespace ElecWarSystem.Serivces
{
    public class SmallUnitService : IDBRepository<We7daFar3eya>
    {
        private readonly AppDBContext appDBContext;
        private readonly FardDetailsService FardDetailsService;
        public SmallUnitService()
        {
            appDBContext = new AppDBContext();
            FardDetailsService = new FardDetailsService();
        }
        public long? Add(We7daFar3eya smallUnit)
        {
            appDBContext.We7daFar3eya.Add(smallUnit);
            appDBContext.SaveChanges();
            return smallUnit.ID;
        }

        public void Delete(long? id)
        {
            appDBContext.We7daFar3eya.Remove(Find(id));
            appDBContext.SaveChanges();
        }

        public We7daFar3eya Find(long? id)
        {
            return appDBContext.We7daFar3eya.Find(id);
        }

        public void Update(long? id, We7daFar3eya entity)
        {
            We7daFar3eya smallUnit = Find(id);
            smallUnit.We7daName = entity.We7daName;
            FardDetailsService.Update(smallUnit.Qa2edWe7daID, entity.Qa2edWe7da);
            FardDetailsService.Update(smallUnit.Ra2ees3ameleyatID, entity.Ra2ees3ameleyat);
            appDBContext.SaveChanges();
        }
    }
}