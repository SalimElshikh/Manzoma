using ElecWarSystem.Data;
using ElecWarSystem.Models;
using ElecWarSystem.Models.OutDoor;
using ElecWarSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;

namespace ElecWarSystem.Serivces
{
    public class TmamGatheringService
    {
        private readonly AppDBContext appDBContext;
        private readonly TmamService tmamService;
        private readonly UserService userService;
        private readonly ZoneService zoneService;
        private readonly UnitService unitService;
        private DateTime dateTime;
        public TmamGatheringService(DateTime? date = null)
        {
            appDBContext = new AppDBContext();
            userService = new UserService();
            zoneService = new ZoneService();
            unitService = new UnitService();
            if (date != null)
            {
                dateTime = (DateTime)date;
            }
            else
            {
                dateTime = DateTime.Today.AddDays(1);
            }
            tmamService = new TmamService(dateTime);

        }
        private Dictionary<String, LeaderTmamView> GetLeaderTmamPerUnit(int unitID)
        {
            Dictionary<String, LeaderTmamView> leaderTmamDic = new Dictionary<string, LeaderTmamView>();
            Tmam tmam = tmamService.GetTmam(unitID);
            if (tmam != null && tmam.Submitted && tmam.Recieved)
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
        public Dictionary<String, Dictionary<String, Dictionary<String, LeaderTmamView>>> GetAllLeaderTmam()
        {
            List<Taba3eya> zones = zoneService.GetZones();
            var zoneUnitsTmam = new Dictionary<string, Dictionary<string, Dictionary<string, LeaderTmamView>>>();
            foreach (Taba3eya zone in zones)
            {
                List<We7daRa2eeseya> unitsPerZone = unitService.GetByZone(zone.ID);
                var leaderTmams = new Dictionary<string, Dictionary<string, LeaderTmamView>>();
                foreach (We7daRa2eeseya unit in unitsPerZone)
                {
                    leaderTmams[unit.We7daName] = GetLeaderTmamPerUnit(unit.ID);
                }
                zoneUnitsTmam[zone.Taba3eyaName] = leaderTmams;
            }
            return zoneUnitsTmam;
        }
        public Dictionary<String, Dictionary<String, FardDetails>> GetAllAltCommandor()
        {
            List<Taba3eya> zones = zoneService.GetZones();
            var zoneUnitsTmam = new Dictionary<string, Dictionary<string, FardDetails>>();
            foreach (Taba3eya zone in zones)
            {
                List<We7daRa2eeseya> unitsPerZone = unitService.GetByZone(zone.ID);
                var altComTmams = new Dictionary<string, FardDetails>();
                foreach (We7daRa2eeseya unit in unitsPerZone)
                {
                    Tmam tmam = tmamService.GetTmamSubmitted(unit.ID);
                    FardDetails altCommander = tmam?.AltCommander;
                    if (altCommander != null)
                    {
                        altComTmams[unit.We7daName] = altCommander;
                    }
                }
                zoneUnitsTmam[zone.Taba3eyaName] = altComTmams;
            }
            return zoneUnitsTmam;
        }

        public List<Tmam> GetTmamsSubmitted()
        {
            List<Tmam> ListOfSubmittedTmams = appDBContext.Tmams
                .Include("We7daRa2eeseya")
                .Where(row => row.Submitted && row.Date == dateTime)
                .OrderBy(row => row.Sa3at)
                .ToList();
            return ListOfSubmittedTmams;
        }

        public Dictionary<String, List<TmamDetails>> GetOfficersTmam(bool IsOfficers)
        {
            List<Taba3eya> zones = zoneService.GetZones();
            Dictionary<String, List<TmamDetails>> zoneUnitsTmam = new Dictionary<string, List<TmamDetails>>();
            foreach (Taba3eya zone in zones)
            {
                List<We7daRa2eeseya> unitsPerZone = unitService.GetByZone(zone.ID);
                List<TmamDetails> tmamDetails1 = new List<TmamDetails>();
                foreach (We7daRa2eeseya unit in unitsPerZone)
                {
                    TmamDetails tmamDetail = tmamService.GetTmamDetailOrDefault(unit.ID, IsOfficers);
                    tmamDetails1.Add(tmamDetail);
                }
                zoneUnitsTmam[zone.Taba3eyaName] = tmamDetails1;
            }
            return zoneUnitsTmam;
        }

        public Dictionary<String, Dictionary<String, List<Marady>>> GetMaradysTmam()
        {
            List<Taba3eya> zones = zoneService.GetZones();
            var zoneUnitsTmam = new Dictionary<String, Dictionary<String, List<Marady>>>();

            foreach (Taba3eya zone in zones)
            {
                var MaradysDic = new Dictionary<String, List<Marady>>();
                foreach (We7daRa2eeseya unit in unitService.GetByZone(zone.ID))
                {
                    long tmamId = tmamService.GetTmamIDToday(unit.ID);
                    if (tmamId > 0)
                    {
                        var MaradyPerUnit = appDBContext.Marady
                            .Include("MaradyDetails.FardDetails.Rotba")
                            .Include("Tmam")
                            .Where(row => row.TmamID == tmamId &&
                                        row.Tmam.Submitted &&
                                        row.Tmam.Recieved)
                            .OrderBy(row => row.MaradyDetails.FardDetails.RotbaID)
                            .ToList<Marady>();
                        if (MaradyPerUnit.Count > 0)
                        {
                            MaradysDic[unit.We7daName] = MaradyPerUnit;
                        }
                    }
                }
                if (MaradysDic.Count > 0)
                {
                    zoneUnitsTmam[zone.Taba3eyaName] = MaradysDic;
                }
            }
            return zoneUnitsTmam;
        }
        public Dictionary<String, Dictionary<String, List<KharegBelad>>> GetOutOfCountriesTmam()
        {
            List<Taba3eya> zones = zoneService.GetZones();
            var zoneUnitsTmam = new Dictionary<String, Dictionary<String, List<KharegBelad>>>();

            foreach (Taba3eya zone in zones)
            {
                var KharegBeladDic = new Dictionary<String, List<KharegBelad>>();
                foreach (We7daRa2eeseya unit in unitService.GetByZone(zone.ID))
                {
                    long tmamId = tmamService.GetTmamIDToday(unit.ID);
                    if (tmamId > 0)
                    {
                        var KharegBeladPerUnit = appDBContext.KharegBelad
                            .Include("KharegBeladDetails.FardDetails.Rotba")
                            .Include("Tmam")
                            .Where(row => row.TmamID == tmamId &&
                                        row.Tmam.Submitted &&
                                        row.Tmam.Recieved)
                            .OrderBy(row => row.KharegBeladDetails.FardDetails.RotbaID)
                            .ToList<KharegBelad>();
                        if (KharegBeladPerUnit.Count > 0)
                        {
                            KharegBeladDic[unit.We7daName] = KharegBeladPerUnit;
                        }
                    }
                }
                if (KharegBeladDic.Count > 0)
                {
                    zoneUnitsTmam[zone.Taba3eyaName] = KharegBeladDic;
                }
            }
            return zoneUnitsTmam;
        }
        public Dictionary<String, Dictionary<String, List<Ma2moreya>>> GetMa2moreyasTmam()
        {
            List<Taba3eya> zones = zoneService.GetZones();
            var zoneUnitsTmam = new Dictionary<String, Dictionary<String, List<Ma2moreya>>>();

            foreach (Taba3eya zone in zones)
            {
                var Ma2moreyasDic = new Dictionary<String, List<Ma2moreya>>();
                foreach (We7daRa2eeseya unit in unitService.GetByZone(zone.ID))
                {
                    long tmamId = tmamService.GetTmamIDToday(unit.ID);
                    var Ma2moreyaPerUnit = appDBContext.Ma2moreya
                                .Include("Ma2moreyaDetails.FardDetails.Rotba")
                                .Include("Tmam")
                                .Where(row => row.TmamID == tmamId &&
                                        row.Tmam.Submitted &&
                                        row.Tmam.Recieved)
                                .OrderBy(row => row.Ma2moreyaDetails.FardDetails.RotbaID)
                                .ToList<Ma2moreya>();
                    if (Ma2moreyaPerUnit.Count > 0)
                    {
                        Ma2moreyasDic[unit.We7daName] = Ma2moreyaPerUnit;
                    }
                }
                if (Ma2moreyasDic.Count > 0)
                {
                    zoneUnitsTmam[zone.Taba3eyaName] = Ma2moreyasDic;
                }
            }
            return zoneUnitsTmam;
        }
        public Dictionary<String, Dictionary<String, List<Segn>>> GetSegnsTmam()
        {
            List<Taba3eya> zones = zoneService.GetZones();
            var zoneUnitsTmam = new Dictionary<String, Dictionary<String, List<Segn>>>();

            foreach (Taba3eya zone in zones)
            {
                var SegnsDic = new Dictionary<String, List<Segn>>();
                foreach (We7daRa2eeseya unit in unitService.GetByZone(zone.ID))
                {
                    long tmamId = tmamService.GetTmamIDToday(unit.ID);
                    var SegnPerUnit = appDBContext.Segn
                                .Include("SegnDetails.FardDetails.Rotba")
                                .Include("SegnDetails.CommandItem")
                                .Include("Tmam")
                                .Where(row => row.TmamID == tmamId &&
                                            row.Tmam.Submitted &&
                                            row.Tmam.Recieved)
                                .OrderBy(row => row.SegnDetails.FardDetails.RotbaID)
                                .ToList<Segn>();
                    if (SegnPerUnit.Count > 0)
                    {
                        SegnsDic[unit.We7daName] = SegnPerUnit;
                    }
                }
                if (SegnsDic.Count > 0)
                {
                    zoneUnitsTmam[zone.Taba3eyaName] = SegnsDic;
                }
            }
            return zoneUnitsTmam;
        }
        public Dictionary<String, Dictionary<String, List<Gheyab>>> GetGheyabsTmam()
        {
            List<Taba3eya> zones = zoneService.GetZones();
            var zoneUnitsTmam = new Dictionary<String, Dictionary<String, List<Gheyab>>>();

            foreach (Taba3eya zone in zones)
            {
                var GheyabsDic = new Dictionary<String, List<Gheyab>>();
                foreach (We7daRa2eeseya unit in unitService.GetByZone(zone.ID))
                {
                    long tmamId = tmamService.GetTmamIDToday(unit.ID);
                    var GheyabPerUnit = appDBContext.Gheyab
                                .Include("GheyabDetails.FardDetails.Rotba")
                                .Include("GheyabDetails.CommandItem")
                                .Include("Tmam")
                                .Where(row => row.TmamID == tmamId &&
                                            row.Tmam.Submitted &&
                                            row.Tmam.Recieved)
                                .OrderBy(row => row.GheyabDetails.FardDetails.RotbaID)
                                .ToList<Gheyab>();
                    if (GheyabPerUnit.Count > 0)
                    {
                        GheyabsDic[unit.We7daName] = GheyabPerUnit;
                    }
                }
                if (GheyabsDic.Count > 0)
                {
                    zoneUnitsTmam[zone.Taba3eyaName] = GheyabsDic;
                }
            }
            return zoneUnitsTmam;
        }
        public Dictionary<String, Dictionary<String, List<Mostashfa>>> GetMostashfasTmam()
        {
            List<Taba3eya> zones = zoneService.GetZones();
            var zoneUnitsTmam = new Dictionary<String, Dictionary<String, List<Mostashfa>>>();

            foreach (Taba3eya zone in zones)
            {
                var MostashfasDic = new Dictionary<String, List<Mostashfa>>();
                foreach (We7daRa2eeseya unit in unitService.GetByZone(zone.ID))
                {
                    long tmamId = tmamService.GetTmamIDToday(unit.ID);
                    var MostashfaPerUnit = appDBContext.Mostashfa
                                .Include("MostashfaDetails.FardDetails.Rotba")
                                .Include("Tmam")
                                .Where(row => row.TmamID == tmamId &&
                                            row.Tmam.Submitted &&
                                            row.Tmam.Recieved)
                                .OrderBy(row => row.MostashfaDetails.FardDetails.RotbaID)
                                .ToList<Mostashfa>();
                    if (MostashfaPerUnit.Count > 0)
                    {
                        MostashfasDic[unit.We7daName] = MostashfaPerUnit;
                    }
                }
                if (MostashfasDic.Count > 0)
                {
                    zoneUnitsTmam[zone.Taba3eyaName] = MostashfasDic;
                }
            }
            return zoneUnitsTmam;
        }
        public Dictionary<String, Dictionary<String, List<Mo3askr>>> GetMo3askrsTmam()
        {
            List<Taba3eya> zones = zoneService.GetZones();
            var zoneUnitsTmam = new Dictionary<String, Dictionary<String, List<Mo3askr>>>();

            foreach (Taba3eya zone in zones)
            {
                var Mo3askrsDic = new Dictionary<String, List<Mo3askr>>();
                foreach (We7daRa2eeseya unit in unitService.GetByZone(zone.ID))
                {
                    long tmamId = tmamService.GetTmamIDToday(unit.ID);
                    var Mo3askrPerUnit = appDBContext.Mo3askr
                                .Include("Mo3askrDetails.FardDetails.Rotba")
                                .Include("Tmam")
                                .Where(row => row.TmamID == tmamId &&
                                            row.Tmam.Submitted &&
                                            row.Tmam.Recieved)
                                .OrderBy(row => row.Mo3askrDetails.FardDetails.RotbaID)
                                .ToList<Mo3askr>();
                    if (Mo3askrPerUnit.Count > 0)
                    {
                        Mo3askrsDic[unit.We7daName] = Mo3askrPerUnit;
                    }
                }
                if (Mo3askrsDic.Count > 0)
                {
                    zoneUnitsTmam[zone.Taba3eyaName] = Mo3askrsDic;
                }
            }
            return zoneUnitsTmam;
        }
        public Dictionary<String, Dictionary<String, List<Fer2a>>> GetFer2asTmam()
        {
            List<Taba3eya> zones = zoneService.GetZones();
            var zoneUnitsTmam = new Dictionary<String, Dictionary<String, List<Fer2a>>>();

            foreach (Taba3eya zone in zones)
            {
                var Fer2asDic = new Dictionary<String, List<Fer2a>>();
                foreach (We7daRa2eeseya unit in unitService.GetByZone(zone.ID))
                {
                    long tmamId = tmamService.GetTmamIDToday(unit.ID);
                    var Fer2aPerUnit = appDBContext.Fer2a
                                .Include("Fer2aDetails.FardDetails.Rotba")
                                .Include("Tmam")
                                .Include("Fer2aDetails.CommandItem")
                                .Where(row => row.TmamID == tmamId &&
                                            row.Tmam.Submitted &&
                                            row.Tmam.Recieved)
                                .OrderBy(row => row.Fer2aDetails.FardDetails.RotbaID)
                                .ToList<Fer2a>();
                    if (Fer2aPerUnit.Count > 0)
                    {
                        Fer2asDic[unit.We7daName] = Fer2aPerUnit;
                    }
                }
                if (Fer2asDic.Count > 0)
                {
                    zoneUnitsTmam[zone.Taba3eyaName] = Fer2asDic;
                }
            }
            return zoneUnitsTmam;
        }
    }
}