using ElecWarSystem.Data;
using ElecWarSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace ElecWarSystem.Serivces
{
    public class EmailService
    {
        private AppDBContext dBContext;
        public EmailService()
        {
            dBContext = new AppDBContext();
        }
        public List<Email> GetExportedEmails(int unitID)
        {
            List<Email> emails = dBContext.Emails
                .Include("Sender")
                .Where(row => row.SenderUserID == unitID)
                .OrderByDescending(m => m.SendDateTime)
                .ToList();
            return emails;
        }
        public List<Email> GetRecievedEmails(int unitID)
        {
            List<Email> emails = dBContext.Emails
                .Include("Sender")
                .Include("Mostalem")
                .Where(row => row.Mostalem.Contains(row.Mostalem.FirstOrDefault(rec => rec.MostalemID == unitID)))
                .OrderByDescending(m => m.SendDateTime)
                .ToList();
            return emails;
        }
        public List<Mostalem> GetRecievers(int unitID)
        {
            List<Mostalem> recievers = dBContext.Mostalem
                .Include("Email.Sender")
                .Where(row => row.MostalemID == unitID)
                .OrderByDescending(m => m.Email.SendDateTime)
                .ToList();
            foreach (Mostalem reciever in recievers)
            {
                reciever.Email.Mostalem = null;
            }
            return recievers;
        }
        public int GetCountOfUnReadEmails(int unitID)
        {
            int count = dBContext.Mostalem.Where(row => row.MostalemID == unitID && !row.Maqroo2).Count(); ;
            return count;
        }
    }
}