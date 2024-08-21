using ElecWarSystem.Data;
using ElecWarSystem.Models;
using ElecWarSystem.Models.OutDoor;
using ElecWarSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using Mo3askr = ElecWarSystem.Models.Mo3askr;
using KharegBelad = ElecWarSystem.Models.KharegBelad;
using Fard = ElecWarSystem.Models.Fard;

namespace ElecWarSystem.Serivces
{
    public class TmamService
    {
        private readonly AppDBContext dBContext;
        private readonly FardDetailsService FardDetailsService;
        private readonly UnitService unitService;
        private readonly UserService userService;
        private readonly FardService FardService;
        private DateTime dateTime;
        public TmamService(DateTime? date = null)
        {
            dBContext = new AppDBContext();
            FardDetailsService = new FardDetailsService();
            unitService = new UnitService();
            userService = new UserService();
            FardService = new FardService();
            if (date != null)
            {
                dateTime = (DateTime)date;
            }
            else
            {
                dateTime = DateTime.Today.AddDays(1);
            }
        }
        public Tmam GetTmam(long id)
        {
            return dBContext.Tmams.Find(id);
        }
        public long GetTmamIDToday(int unitID)
        {
            Tmam tmam = GetTmam(unitID);
            return tmam?.ID ?? 0;
        }
        public FardDetails GetAltCommandorToday(int unitID)
        {
            Tmam tmam = GetTmam(unitID);
            FardDetails altCommandor = tmam?.AltCommander ?? new FardDetails() { ID = 0, RotbaID = 0 };
            return altCommandor;
        }
        public long AddTmam(Tmam tmam)
        {
            tmam.ID = GetTmamID(tmam);
            if (tmam.ID == 0)
            {
                dBContext.Tmams.Add(tmam);
                dBContext.SaveChanges();
                FardDetailsService.ResetFardDetailssStatus(tmam.We7daID);
            }
            if (tmam.Qa2edManoobID != null)
            {
                Tmam tmam1 = dBContext.Tmams.Find(tmam.ID);
                tmam1.Qa2edManoobID = tmam.Qa2edManoobID;
                dBContext.SaveChanges();
            }
            return tmam.ID;
        }

        public void DeleteTmam(Tmam tmam)
        {
            try
            {
                dBContext.Tmams.Remove(tmam);
                dBContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public long GetTmamID(Tmam tmam)
        {
            Tmam tmamTemp = dBContext.Tmams.
                FirstOrDefault(row => row.We7daID == tmam.We7daID && row.Date == tmam.Date);

            return tmamTemp?.ID ?? 0;
        }
        public Tmam GetTmam(int unitID)
        {
            Tmam tmam = dBContext.Tmams.
                Include("AltCommander.Rotba").
                FirstOrDefault(row => row.We7daID == unitID && row.Date == dateTime);
            return tmam;
        }
        public Tmam GetTmamSubmitted(int unitID)
        {
            Tmam tmam = dBContext.Tmams.
                Include("AltCommander.Rotba").
                FirstOrDefault(row => row.We7daID == unitID && row.Date == dateTime && row.Recieved && row.Submitted);

            return tmam;
        }
        public long AddTmamDetail(TmamDetails tmamDetail)
        {
            tmamDetail.TmamID = AddTmam(tmamDetail.Tmam);
            tmamDetail.Tmam = null;
            tmamDetail.ID = GetTmamDetailsID(tmamDetail);
            dBContext.TmamDetails.AddOrUpdate(tmamDetail);
            dBContext.SaveChanges();

            return tmamDetail.ID;
        }
        public void DeleteTmamDetail(TmamDetails tmamDetail)
        {
            dBContext.TmamDetails.Remove(tmamDetail);
            dBContext.SaveChanges();
        }
        public long GetTmamDetailsID(TmamDetails tmamDetail)
        {
            TmamDetails tmamDetailTemp = dBContext.TmamDetails
                .FirstOrDefault(row => row.TmamID == tmamDetail.TmamID && row.IsOfficers == tmamDetail.IsOfficers);
            return tmamDetailTemp?.ID ?? 0;
        }
        public TmamDetails GetTmamDetailOrDefault(int unitID, bool isOfficers)
        {
            TmamDetails tmamDetail = dBContext.TmamDetails.
                Include("Tmam.We7daRa2eeseya").
                FirstOrDefault(row => row.Tmam.We7daID == unitID &&
                                        row.Tmam.Date == dateTime &&
                                        row.IsOfficers == isOfficers &&
                                        row.Tmam.Submitted &&
                                        row.Tmam.Recieved);

            if (tmamDetail == null)
            {
                tmamDetail = new TmamDetails
                {
                    Tmam = new Tmam
                    {
                        We7daRa2eeseya = unitService.GetUnit(unitID)
                    }
                };
            }
            return tmamDetail;
        }
        public TmamDetails GetTmamDetails(int unitID, bool isOfficers)
        {
            TmamDetails tmamDetail = new TmamDetails();
            Tmam expectedTmam = new Tmam() { We7daID = unitID, Date = dateTime };
            long tmamID = GetTmamID(expectedTmam);
            TmamDetails tmamDetailTemp = new TmamDetails() { TmamID = tmamID, IsOfficers = isOfficers };
            long tmamDetailsID = GetTmamDetailsID(tmamDetailTemp);
            if (tmamDetailsID == 0)
            {
                int Qowwa = (isOfficers) ? FardDetailsService.GetFardDetailssCount(unitID, 1) : (FardDetailsService.GetFardDetailssCount(unitID, 2) + FardDetailsService.GetFardDetailssCount(unitID, 3));
                tmamDetail = new TmamDetails()
                {
                    Qowwa = Qowwa
                };
            }
            else
            {
                tmamDetail = dBContext.TmamDetails.FirstOrDefault(row => row.ID == tmamDetailsID);
                int Qowwa = (isOfficers) ? FardDetailsService.GetFardDetailssCount(unitID, 1) : (FardDetailsService.GetFardDetailssCount(unitID, 2) + FardDetailsService.GetFardDetailssCount(unitID, 3));
                tmamDetail.Qowwa = Qowwa;
                dBContext.SaveChanges();
            }
            return tmamDetail;
        }
        public Tmam GetTmamWithAllDetails(int unitID)
        {
            /*
             * Copy the Previous Tmam
             */
            PrepareTmam(unitID);
            Tmam tmam = dBContext.Tmams
                .Include("TmamDetails")
                .Include("Marady.MaradyDetails.FardDetails.Rotba")
                .Include("Ma2moreya.Ma2moreyaDetails.FardDetails.Rotba")
                .Include("Agaza.AgazaDetails.FardDetails.Rotba")
                .Include("Segn.SegnDetails.FardDetails.Rotba")
                .Include("Segn.SegnDetails.CommandItem")
                .Include("Gheyab.GheyabDetails.FardDetails.Rotba")
                .Include("Gheyab.GheyabDetails.CommandItem")
                .Include("Mostashfa.MostashfaDetails.FardDetails.Rotba")
                .Include("KharegBelad.KharegBeladDetails.FardDetails.Rotba")
                .Include("Mo3askr.Mo3askrDetails.FardDetails.Rotba")
                .Include("AltCommander.Rotba")
                .Include("We7daRa2eeseya")
                .Include("Fer2a.Fer2aDetails.FardDetails.Rotba")
                .Include("Fer2a.Fer2aDetails.CommandItem")
                .FirstOrDefault(row => row.We7daID == unitID && row.Date == dateTime);
            return tmam;
        }
        public Tmam GetTmamWithNumbers(int unitID)
        {
            Tmam tmam = dBContext.Tmams
                .Include("TmamDetails")
                .Include("Marady")
                .Include("Ma2moreya")
                .Include("Agaza")
                .Include("Segn")
                .Include("Gheyab")
                .Include("Mostashfa")
                .Include("KharegBelad")
                .Include("Mo3askr")
                .Include("We7daRa2eeseya")
                .Include("Fer2a")
                .FirstOrDefault(row => row.We7daID == unitID && row.Date == dateTime);
            return tmam;
        }
        public Tmam GetTmamWithAllDetails(int unitID, DateTime date)
        {
            Tmam tmam = dBContext.Tmams
                .Include("TmamDetails")
                .Include("Marady.MaradyDetails.FardDetails.Rotba")
                .Include("Ma2moreya.Ma2moreyaDetails.FardDetails.Rotba")
                .Include("Agaza.AgazaDetails.FardDetails.Rotba")
                .Include("Segn.SegnDetails.FardDetails.Rotba")
                .Include("Segn.SegnDetails.CommandItem")
                .Include("Gheyab.GheyabDetails.FardDetails.Rotba")
                .Include("Gheyab.GheyabDetails.CommandItem")
                .Include("Mostashfa.MostashfaDetails.FardDetails.Rotba")
                .Include("KharegBelad.KharegBeladDetails.FardDetails.Rotba")
                .Include("Mo3askr.Mo3askrDetails.FardDetails.Rotba")
                .Include("AltCommander.Rotba")
                .Include("Fer2a.Fer2aDetails.FardDetails.Rotba")
                .Include("Fer2a.Fer2aDetails.CommandItem")
                .FirstOrDefault(row => row.We7daID == unitID && row.Date == date);
            return tmam;
        }
        public void PrepareTmam(int unitID)
        {
            try
            {
                DateTime lastDate = dBContext.Tmams.Where(row => row.We7daID == unitID).Max(row => row.Date);
                if (lastDate < dateTime)
                {
                    FardDetailsService.ResetFardDetailssStatus(unitID); //reset all FardDetails status in this unit 
                    Tmam oldTmam = GetTmamWithAllDetails(unitID, lastDate);
                    Tmam newTmam = (Tmam)oldTmam.Clone();
                    oldTmam.Date = lastDate;
                    long tmamID = AddTmam(newTmam);
                    Tmam temp = GetTmamWithAllDetails(unitID, dateTime);
                    foreach (Marady Marady in temp.Marady)
                    {
                        if (FardDetailsService.FardDetailsIsLeader(Marady.MaradyDetails.FardID) != 0)
                        {
                            FardService.setFard(new Fard
                            {
                                TmamID = temp.ID,
                                FardID = Marady.MaradyDetails.FardID,
                                Status = TmamEnum.Marady
                            });
                        }
                    }
                    foreach (Gheyab Gheyab in temp.Gheyab)
                    {
                        if (FardDetailsService.FardDetailsIsLeader(Gheyab.GheyabDetails.FardID) != 0)
                        {
                            FardService.setFard(new Fard
                            {
                                TmamID = temp.ID,
                                FardID = Gheyab.GheyabDetails.FardID,
                                Status = TmamEnum.Gheyab
                            });
                        }
                    }
                    foreach (Ma2moreya Ma2moreya in temp.Ma2moreya)
                    {
                        if (FardDetailsService.FardDetailsIsLeader(Ma2moreya.Ma2moreyaDetails.FardID) != 0)
                        {
                            FardService.setFard(new Fard
                            {
                                TmamID = temp.ID,
                                FardID = Ma2moreya.Ma2moreyaDetails.FardID,
                                Status = TmamEnum.Ma2moreya
                            });
                        }
                    }
                   foreach (Agaza Agaza in temp.Agaza)
                    {
                        if (FardDetailsService.FardDetailsIsLeader(Agaza.AgazaDetails.FardID) != 0)
                        {
                            FardService.setFard(new Fard
                            {
                                TmamID = temp.ID,
                                FardID = Agaza.AgazaDetails.FardID,
                                Status = TmamEnum.Agaza
                            });
                        }
                    }
                    foreach (Segn Segn in temp.Segn)
                    {
                        if (FardDetailsService.FardDetailsIsLeader(Segn.SegnDetails.FardID) != 0)
                        {
                            FardService.setFard(new Fard
                            {
                                TmamID = temp.ID,
                                FardID = Segn.SegnDetails.FardID,
                                Status = TmamEnum.Segn
                            });
                        }
                    }
                    foreach (Mostashfa Mostashfa in temp.Mostashfa)
                    {
                        if (FardDetailsService.FardDetailsIsLeader(Mostashfa.MostashfaDetails.FardID) != 0)
                        {
                            FardService.setFard(new Fard
                            {
                                TmamID = temp.ID,
                                FardID = Mostashfa.MostashfaDetails.FardID,
                                Status = TmamEnum.Mostashfa
                            });
                        }
                    }
                    foreach (KharegBelad KharegBelad in temp.KharegBelad)
                    {
                        if (FardDetailsService.FardDetailsIsLeader(KharegBelad.KharegBeladDetails.FardID) != 0)
                        {
                            FardService.setFard(new Fard
                            {
                                TmamID = temp.ID,
                                FardID = KharegBelad.KharegBeladDetails.FardID,
                                Status = TmamEnum.KharegBelad
                            });
                        }
                    }
                    foreach (Mo3askr mo3askr in temp.Mo3askr)
                    {
                        if (FardDetailsService.FardDetailsIsLeader(mo3askr.Mo3askrDetails.FardID) != 0)
                        {
                            FardService.setFard(new Fard
                            {
                                TmamID = temp.ID,
                                FardID = mo3askr.Mo3askrDetails.FardID,
                                Status = TmamEnum.Mo3askr
                            });
                        }
                    }
                    foreach (Fer2a Fer2a in temp.Fer2a)
                    {
                        if (FardDetailsService.FardDetailsIsLeader(Fer2a.Fer2aDetails.FardID) != 0)
                        {
                            FardService.setFard(new Fard
                            {
                                TmamID = temp.ID,
                                FardID = Fer2a.Fer2aDetails.FardID,
                                Status = TmamEnum.Fer2a
                            });
                        }
                    }
                }
            }
            catch
            {

            }
        }
        public String SubmitTmam(int unitID)
        {
            StringBuilder result = new StringBuilder();
            Tmam tmam = GetTmamWithNumbers(unitID);
            
            if (tmam.Qa2edManoobID == null && tmam.We7daRa2eeseya.TawagodQa2edManoob)
                result.Append("\n-برجاء إدخال منوب العمليات عن اليوم .");
            
            if (tmam.TmamDetails.FirstOrDefault(row => row.IsOfficers) == null)
                result.Append("\n-برجاء إدخال تمام الضباط .");
            
            if (tmam.TmamDetails.FirstOrDefault(row => !row.IsOfficers) == null)
                result.Append("\n-برجاء إدخال تمام الدرجات الأخرى .");
            
            if (tmam.TmamDetails.Sum(row => row.Ma2moreya) != tmam.Ma2moreya.Count)
                result.Append($"\n-عدد المأموريات {tmam.TmamDetails.Sum(row => row.Ma2moreya)} مدخل {tmam.Ma2moreya.Count}");

            if (tmam.TmamDetails.Sum(row => row.Marady) != tmam.Marady.Count)
                result.Append($"\n-عدد الأجازت المرضية {tmam.TmamDetails.Sum(row => row.Marady)} مدخل {tmam.Marady.Count}");

            if (tmam.TmamDetails.Sum(row => row.Segn) != tmam.Segn.Count)
                result.Append($"\n-عدد السجن {tmam.TmamDetails.Sum(row => row.Segn)} مدخل {tmam.Segn.Count}");

            if (tmam.TmamDetails.Sum(row => row.Gheyab) != tmam.Gheyab.Count)
                result.Append($"\n-عدد الغياب {tmam.TmamDetails.Sum(row => row.Gheyab)} مدخل {tmam.Gheyab.Count}");

            if (tmam.TmamDetails.Sum(row => row.Mostashfa) != tmam.Mostashfa.Count)
                result.Append($"\n-عدد المستشفيات {tmam.TmamDetails.Sum(row => row.Mostashfa)} مدخل {tmam.Mostashfa.Count}");

            if (tmam.TmamDetails.Sum(row => row.Fer2a) != tmam.Fer2a.Count)
                result.Append($"\n-عدد الفرق {tmam.TmamDetails.Sum(row => row.Fer2a)} مدخل {tmam.Fer2a.Count}");

            if (result.Length == 0)
            {
                tmam.Submitted = true;
                tmam.Sa3at = ConvertTimeToMilatryFormat(DateTime.Now);
                dBContext.SaveChanges();
            }

            return result.ToString();
        }
        public String ConvertTimeToMilatryFormat(DateTime time)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int timeNum = (time.Hour * 100) + time.Minute;
            stringBuilder.Append(timeNum.ToString());
            int len = stringBuilder.Length;
            for (int i = 0; i < 4 - len; i++)
            {
                stringBuilder.Insert(0, "0");
            }
            return stringBuilder.ToString();
        }
        //change the recieve property to true in tamam
        public void ReciveTmam(int unitID)
        {
            Tmam tmam = dBContext.Tmams.FirstOrDefault(row => row.We7daID == unitID && row.Date == dateTime);
            tmam.Recieved = true;
            dBContext.SaveChanges();
        }
        public Dictionary<String, bool> GetTmamStatus(int unitID)
        {
            Tmam tmam = GetTmam(unitID);
            Dictionary<String, bool> tmamStatus = new Dictionary<String, bool>();
            tmamStatus["Submitted"] = tmam?.Submitted ?? false;
            tmamStatus["Recieved"] = tmam?.Recieved ?? false;
            return tmamStatus;
        }

        //cancel tmam cuz there is an error 
        public void ReturnTmam(int unitID)
        {
            Tmam tmam = dBContext.Tmams.FirstOrDefault(row => row.We7daID == unitID && row.Date == dateTime);
            tmam.Recieved = false;
            tmam.Submitted = false;
            dBContext.SaveChanges();
        }
        public Dictionary<String, LeaderTmamView> GetLeaderTmamPerUnit(int unitID)
        {
            Dictionary<String, LeaderTmamView> leaderTmamDic = new Dictionary<string, LeaderTmamView>();
            Tmam tmam = GetTmam(unitID);
            if (tmam != null)
            {
                We7daRa2eeseya unit = userService.GetUnit(unitID);
                if (unit.Qa2edWe7daID != null && unit.Ra2ees3ameleyatID != null)
                {
                    leaderTmamDic["UC-0"] = new LeaderTmamView(tmam.ID, (long)unit.Qa2edWe7daID);
                    leaderTmamDic["UOC-0"] = new LeaderTmamView(tmam.ID, (long)unit.Ra2ees3ameleyatID);
                    int i = 1;
                    foreach (We7daFar3eya smallUnit in unit.We7daFar3eya)
                    {
                        leaderTmamDic[$"UC-{i}"] = new LeaderTmamView(tmam.ID, (long)smallUnit.Qa2edWe7daID);
                        leaderTmamDic[$"UOC-{i}"] = new LeaderTmamView(tmam.ID, (long)smallUnit?.Ra2ees3ameleyatID);
                        i++;
                    }
                }
            }
            return leaderTmamDic;
        }
    }
}