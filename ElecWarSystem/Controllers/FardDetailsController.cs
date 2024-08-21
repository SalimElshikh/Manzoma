using ElecWarSystem.Models;
using ElecWarSystem.Serivces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ElecWarSystem.Controllers
{
    public class FardDetailsController : Controller
    {
        private FardDetailsService FardDetailsRepo;
        public FardDetailsController()
        {
            FardDetailsRepo = new FardDetailsService();
        }
        // GET: FardDetails of specific unit and specific title
        [HttpGet]
        public JsonResult GetFardDetailss(int type)
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            List<FardDetails> FardDetailss = FardDetailsRepo.GetFardDetailss(userId, type);
            return Json(FardDetailss, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetFardDetailssOfRotba(int RotbaID)
        {
            int userId = int.Parse(Request.Cookies["userID"].Value);
            List<FardDetails> FardDetailss = FardDetailsRepo.GetFardDetailsOfRotba(userId, RotbaID);
            return Json(FardDetailss, JsonRequestBehavior.AllowGet);
        }

        // GET: FardDetails by id
        [HttpGet]
        public JsonResult GetFardDetails(int id)
        {
            FardDetails FardDetails = FardDetailsRepo.Find(id);
            return Json(FardDetails, JsonRequestBehavior.AllowGet);
        }
        // Post: Create New FardDetails
        [HttpPost]
        public void Create(FardDetails FardDetails)
        {
            FardDetailsRepo.Add(FardDetails);
        }
        // Put: Edit exist FardDetails
        [HttpPost]
        public void Edit(int id, FardDetails FardDetails)
        {
            FardDetailsRepo.Update(id, FardDetails);
        }
        // Delete: Delete FardDetails
        [HttpPost]
        public bool Delete(long? id)
        {
            return FardDetailsRepo.Delete(id);
        }
    }
}