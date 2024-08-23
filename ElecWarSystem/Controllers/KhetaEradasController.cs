using ElecWarSystem.Data;
using ElecWarSystem.Models.OutDoor.OutDoorNew;
using ElecWarSystem.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ElecWarSystem.Controllers
{
    public class KhetaEradasController : Controller
    {
        private AppDBContext db = new AppDBContext();

        // GET: KhetaEradas
        public ActionResult Index()
        {
            var viewModel = new KhetaEradaViewModel
            {
                Asl7as = db.Asl7as.ToList(),
                Za5iras = db.Za5iras.ToList(),
                Mo3edats = db.Mo3edats.ToList(),
                Markbats = db.Markbats.ToList(),
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
        [Route("KhetaEradas/GetData")]
        [ValidateAntiForgeryToken]
        public JsonResult GetData(KhetaEradaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var KhetaErada = new KhetaErada
                        {
                            Qa2ed = viewModel.KhetaErada.Qa2ed,
                            RotbaQa2ed = viewModel.KhetaErada.RotbaQa2ed,
                            DobatNum = viewModel.KhetaErada.DobatNum,
                            DargatNum = viewModel.KhetaErada.DargatNum,
                            DateFrom = viewModel.KhetaErada.DateFrom,
                            DateTo = viewModel.KhetaErada.DateTo,
                        };

                        db.KhetaEradas.Add(KhetaErada);
                        db.SaveChanges();

                        // Save related entities with the newly created KhetaErada ID
                        SaveRelatedEntities(viewModel, (int)KhetaErada.ID);

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

        private void SaveRelatedEntities(KhetaEradaViewModel viewModel, int KhetaEradaId)
        {
            if (viewModel.Mo3edats != null && viewModel.Mo3edats.Any())
            {
                foreach (var mo3edat in viewModel.Mo3edats)
                {
                    mo3edat.ID = KhetaEradaId; // Ensure correct mapping
                    db.Mo3edats.Add(mo3edat);
                }
            }

            if (viewModel.Asl7as != null && viewModel.Asl7as.Any())
            {
                foreach (var asl7a in viewModel.Asl7as)
                {
                    asl7a.ID = KhetaEradaId; // Ensure correct mapping
                    db.Asl7as.Add(asl7a);
                }
            }

            if (viewModel.Za5iras != null && viewModel.Za5iras.Any())
            {
                foreach (var za5ira in viewModel.Za5iras)
                {
                    za5ira.ID = KhetaEradaId; // Ensure correct mapping
                    db.Za5iras.Add(za5ira);
                }
            }

            if (viewModel.Markbats != null && viewModel.Markbats.Any())
            {
                foreach (var markbat in viewModel.Markbats)
                {
                    markbat.ID = KhetaEradaId; // Ensure correct mapping
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
