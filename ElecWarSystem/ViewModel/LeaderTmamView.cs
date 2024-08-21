using ElecWarSystem.Data;
using ElecWarSystem.Models;
using ElecWarSystem.Models.OutDoorDetails;
using ElecWarSystem.Serivces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElecWarSystem.ViewModel
{
    public class LeaderTmamView
    {
        private long tmamID, FardDetailsID;
        private TmamEnum status;
        private AppDBContext AppDBContext = new AppDBContext();
        private FardDetailsService FardDetailsService = new FardDetailsService();
        private FardService FardService = new FardService();
        private Dictionary<int, string> statusToTmam = new Dictionary<int, string>()
        {
            { 0 , "موجود" },
            { 1 , "أجازة" },
            { 2 , "أجازة مرضي" },
            { 3 , "مأمورية" },
            { 4 , "سجن" },
            { 5 , "غياب" },
            { 6 , "مستشفى" },
            { 7 , "خ البلاد" },
            { 8 , "تدريب خارجى" },
            { 10 , "فرقة" },
        };

        public string Tmam { get; set; }
        public OutdoorDetail OutdoorDetail { get; set; }
        public LeaderTmamView(long TmamID, long FardID)
        {
            this.tmamID = TmamID;
            this.FardDetailsID = FardID;

            this.status = FardService.getFard(this.tmamID, this.FardDetailsID);//FardDetailsService.GetStatus(FardDetailsID);
            

            Tmam = statusToTmam[(int)this.status];
            
            OutdoorDetail = GetOutdoorDetail();
            
            OutdoorDetail.FardID = this.FardDetailsID;
        }
        private OutdoorDetail GetOutdoorDetail()
        {
            OutdoorDetail outdoorDetail = new OutdoorDetail();
            switch (this.status)
            {
                case TmamEnum.Exist:
                    outdoorDetail = new OutdoorDetail();

                    break;
                case TmamEnum.Agaza:
                    AgazaDetails AgazaDetails = AppDBContext.Agaza.Include("AgazaDetails")
                        .FirstOrDefault(row => row.TmamID == this.tmamID && row.AgazaDetails.FardID == this.FardDetailsID)
                        .AgazaDetails;

                    Tmam = AgazaDetails.AgazaType;
                    outdoorDetail = AgazaDetails;
                    break;
                case TmamEnum.Marady:
                    outdoorDetail = AppDBContext.Marady.Include("MaradyDetails")
                        .FirstOrDefault(row => row.TmamID == this.tmamID && row.MaradyDetails.FardID == this.FardDetailsID)
                        .MaradyDetails;
                    break;
                case TmamEnum.Ma2moreya:
                    outdoorDetail = AppDBContext.Ma2moreya.Include("Ma2moreyaDetails")
                        .FirstOrDefault(row => row.TmamID == this.tmamID && row.Ma2moreyaDetails.FardID == this.FardDetailsID)?
                        .Ma2moreyaDetails;
                    break;
                case TmamEnum.Mostashfa:
                    outdoorDetail = AppDBContext.Mostashfa.Include("MostashfaDetails")
                        .FirstOrDefault(row => row.TmamID == this.tmamID && row.MostashfaDetails.FardID == this.FardDetailsID)
                        .MostashfaDetails;
                    break;
                case TmamEnum.Segn:
                    outdoorDetail = AppDBContext.Segn.Include("SegnDetails")
                        .FirstOrDefault(row => row.TmamID == this.tmamID && row.SegnDetails.FardID == this.FardDetailsID)
                        .SegnDetails;
                    break;
                case TmamEnum.Gheyab:
                    outdoorDetail = AppDBContext.Gheyab.Include("GheyabDetails")
                        .FirstOrDefault(row => row.TmamID == this.tmamID && row.GheyabDetails.FardID == this.FardDetailsID)
                        .GheyabDetails;
                    break;
                case TmamEnum.KharegBelad:
                    outdoorDetail = AppDBContext.KharegBelad.Include("KharegBeladDetails")
                        .FirstOrDefault(row => row.TmamID == this.tmamID && row.KharegBeladDetails.FardID == this.FardDetailsID)
                        .KharegBeladDetails;
                    break;
                case TmamEnum.Mo3askr:
                    outdoorDetail = AppDBContext.Mo3askr.Include("Mo3askrDetails")
                        .FirstOrDefault(row => row.TmamID == this.tmamID && row.Mo3askrDetails.FardID == this.FardDetailsID)
                        .Mo3askrDetails;
                    break;
                case TmamEnum.Fer2a:
                    Fer2aDetails Fer2aDetails = AppDBContext.Fer2a.Include("Fer2aDetails")
                        .FirstOrDefault(row => row.TmamID == this.tmamID && row.Fer2aDetails.FardID == this.FardDetailsID)?.Fer2aDetails;
                    Tmam = Fer2aDetails?.Fer2aName;
                    outdoorDetail = Fer2aDetails;
                    break;
                default:
                    outdoorDetail = new OutdoorDetail();
                    break;

            }
            return outdoorDetail ?? new OutdoorDetail();
        }
    }
}