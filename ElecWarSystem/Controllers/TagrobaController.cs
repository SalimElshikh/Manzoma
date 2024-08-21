using ElecWarSystem.Data;
using ElecWarSystem.Models;
using ElecWarSystem.Models.OutDoor.OutDoorNew;
using ElecWarSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElecWarSystem.Controllers
{
    public class TagrobaController : Controller
    {
        private AppDBContext db = new AppDBContext();

        public ActionResult Index()
        {
            var viewModel = new TagarobViewModel
            {
                Asl7as = db.Asl7as.ToList(),
                Za5iras = db.Za5iras.ToList(),
                Mo3edats = db.Mo3edats.ToList(),
                Markbats = db.Markbats.ToList(),
                TgarobMydanya = new TgarobMydanyas(),
                listAsl7aNames = db.Asl7asNames.Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Name
                }).ToList(),
                listMo3edatNames = db.Mo3edatNames.Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Name
                }).ToList(),
                listMarkbatNames = db.MarkbatNames.Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Name
                }).ToList(),
                listZa5iraNames = db.Za5iraNames.Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Name
                }).ToList(),
                A8radTagarobNames = db.A8radTgarob.Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.a8radTgarob
                }).ToList(),
                FullNames = db.FardDetails.Select(f => new SelectListItem
                {
                    Value = f.ID.ToString(),
                    Text = f.FullName
                }).ToList(),
                Rotbas = db.Rotba.Select(r => new SelectListItem
                {
                    Value = r.ID.ToString(),
                    Text = r.RotbaName // Assuming RotbaName is a property in Rotba
                }).ToList(),
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetData(TagarobViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var TgarobMydanya = new TgarobMydanyas
                        {
                            Qa2edTaghroba = viewModel.TgarobMydanya.Qa2edTaghroba,
                            RotbaQa2edTaghroba = viewModel.TgarobMydanya.RotbaQa2edTaghroba,
                            MkanEltaghroba = viewModel.TgarobMydanya.MkanEltaghroba,
                            DobatNum = viewModel.TgarobMydanya.DobatNum,
                            DargatNum = viewModel.TgarobMydanya.DargatNum,
                            DateFrom = viewModel.TgarobMydanya.DateFrom,
                            DateTo = viewModel.TgarobMydanya.DateTo,
                        };

                        db.TgarobMydanyas.Add(TgarobMydanya);
                        db.SaveChanges();

                        SaveRelatedEntities(viewModel, (int)TgarobMydanya.ID);

                        transaction.Commit();
                        return Json(new { success = true, message = "Data saved successfully." });
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        var errorMessage = ex.InnerException?.Message ?? ex.Message;
                        return Json(new { success = false, message = $"Error occurred while saving data: {errorMessage}" });
                    }
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return Json(new { success = false, message = "Invalid data.", errors });
            }
        }

        private void SaveRelatedEntities(TagarobViewModel viewModel, int TgarobMydanyaID)
        {
            if (viewModel.Mo3edats != null && viewModel.Mo3edats.Any())
            {
                foreach (var mo3edat in viewModel.Mo3edats)
                {
                    mo3edat.ID = TgarobMydanyaID;
                    db.Mo3edats.Add(mo3edat);
                }
            }

            if (viewModel.Asl7as != null && viewModel.Asl7as.Any())
            {
                foreach (var asl7a in viewModel.Asl7as)
                {
                    asl7a.ID = TgarobMydanyaID;
                    db.Asl7as.Add(asl7a);
                }
            }

            if (viewModel.Za5iras != null && viewModel.Za5iras.Any())
            {
                foreach (var za5ira in viewModel.Za5iras)
                {
                    za5ira.ID = TgarobMydanyaID;
                    db.Za5iras.Add(za5ira);
                }
            }

            if (viewModel.Markbats != null && viewModel.Markbats.Any())
            {
                foreach (var markbat in viewModel.Markbats)
                {
                    markbat.ID = TgarobMydanyaID;
                    db.Markbats.Add(markbat);
                }
            }

            db.SaveChanges();
        }
        [HttpPost]
        public JsonResult GetFardDetailsByRotbaAndWe7da(int rotbaID, int we7daID)
        {
            var fardDetails = db.FardDetails
                .Where(f => f.RotbaID == rotbaID && f.We7daID == we7daID && !f.Status)
                .Select(f => new { f.ID, f.FullName })
                .ToList();

            return Json(fardDetails);
        }

    }
}

