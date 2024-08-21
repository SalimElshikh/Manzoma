using ElecWarSystem.Data;
using ElecWarSystem.Models;
using System.Linq;

namespace ElecWarSystem.Serivces
{
    public class FardService
    {
        private readonly AppDBContext dBContext;
        private readonly FardDetailsService FardDetailsService;
        public FardService()
        {
            dBContext = new AppDBContext();
            FardDetailsService = new FardDetailsService();
        }

        public void setFard(Fard Fard)
        {
            dBContext.Fard.Add(Fard);
            dBContext.SaveChanges();
            FardDetailsService.setStatus(Fard.FardID);
        }
        public TmamEnum getFard(long tmamID ,long FardDetailsID)
        {
            Fard Fard = dBContext.Fard.FirstOrDefault(row => 
                row.FardID == FardDetailsID &&
                row.TmamID == tmamID);
            if (Fard == null)
            {
                return TmamEnum.Exist;
            }
            return Fard.Status;
        }
        public void DeleteFard(long tmamID, long FardDetailsID)
        {
            if(tmamID != 0 && FardDetailsID != 0)
            {
                Fard Fard = dBContext.Fard.FirstOrDefault(row =>
                                        row.TmamID == tmamID &&
                                        row.FardID == FardDetailsID);
                if (Fard != null)
                {
                    dBContext.Fard.Remove(Fard);
                    dBContext.SaveChanges();
                }
            }
        }
    }
}